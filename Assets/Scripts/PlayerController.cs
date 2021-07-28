using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7.0f;
    private CharacterController myCharacterController;
    public GameObject tile;

    public GameObject vampire, bat, playerUmbrella;

    private bool isVampireForm = true;

    private int tapCount = 0;           //double tap variables
    private float doubleTapTimer = 0f, doubleTapWaitTime = 0.2f;

    private float maxHP = 100, HP = 100;
    public HealthBar healthBar;

    public float turnSpeed = 0.15f;  //0.15f is the proper speed, could be scaled differently later
    public float rotationSpeed = 0.01f;

    public bool IsVampireForm { get => isVampireForm; set => isVampireForm = value; }

    private bool isUmbrellaAvailable = false;
    public bool IsUmbrellaAvailable { get => isUmbrellaAvailable; set => isUmbrellaAvailable = value; }

    public float turnAngleLimit = 20;

    private int debugDeathCount = 0;    //to be deleted

    public bool isDeathDisabled = false;

    private GameController gameController;
    void Awake()        //was Start()
    {
        HP = maxHP = 100;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        myCharacterController = GetComponent<CharacterController>();
        vampire.gameObject.SetActive(true);
        bat.gameObject.SetActive(false);
        playerUmbrella.SetActive(false);

        healthBar.SetMaxHealth(maxHP);  //healthbar configuration       
    }



    private void switchCharacter()
    {
        if (isVampireForm)
        {
            vampire.gameObject.SetActive(false);    //switching to bat form
            bat.gameObject.SetActive(true);
            isVampireForm = false;
            deepCopyCharacterController(bat);
            bat.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play(); //ParticleSystem is the second child of Bat game object
        }
        else
        {


            vampire.gameObject.SetActive(true);
            bat.gameObject.SetActive(false);        //switching to vampire form
            isVampireForm = true;
            deepCopyCharacterController(vampire);
            vampire.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();

            bat.gameObject.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 0, 0);    //return bat model position to 0, otherwise the bat slowly ascends over time, because of the animation interruption
        }
    }

    private void deepCopyCharacterController(GameObject o)
    {
        CharacterController cc = o.GetComponent<CharacterController>();
        myCharacterController.slopeLimit = cc.slopeLimit;
        myCharacterController.height = cc.height;
        myCharacterController.stepOffset = cc.stepOffset;
        myCharacterController.skinWidth = cc.skinWidth;
        myCharacterController.minMoveDistance = cc.minMoveDistance;
        myCharacterController.center = cc.center + new Vector3(0, o.transform.localPosition.y, 0);
        myCharacterController.radius = cc.radius;

    }

    private void detectDoubleTap()
    {
        if (Time.timeScale == 0) return;    //dont detect double tap if the game is paused (debug screen)
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            tapCount++;
        }

        if (tapCount == 1)
        {
            doubleTapTimer += Time.deltaTime;
        }
        if (tapCount >= 2)
        {
            switchCharacter();
            doubleTapTimer = 0.0f;
            tapCount = 0;
        }
        if (doubleTapTimer > doubleTapWaitTime)
        {
            doubleTapTimer = 0.0f;
            tapCount = 0;
        }
    }

    private void unRotatePlayer()
    {

        float unRotateMultiplier = 50;  //touch.deltaPosition values are between 0 to 150, so this multiplier was chosen accordingly
        float rotationComplete = rotationSpeed * Time.deltaTime * unRotateMultiplier;
        if (transform.localEulerAngles.y != 0)
        {



            if (transform.localEulerAngles.y < 180)
            {
                if ((transform.localEulerAngles.y - rotationComplete) < 0)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);

                }
                else transform.Rotate(0, -rotationComplete, 0);
            }
            else
            {

                if ((transform.localEulerAngles.y + rotationComplete) > 360)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);

                }
                else transform.Rotate(0, rotationComplete, 0);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

        detectDoubleTap();

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {


                myCharacterController.Move(Vector3.right * (touch.deltaPosition.x * turnSpeed) * Time.deltaTime);   //moves the player left and right

                float rotationComplete = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;    //beginning of rotating the player

                if ((touch.deltaPosition.x < 0) && ((transform.localEulerAngles.y - rotationComplete) < (360 - turnAngleLimit)) && (transform.localEulerAngles.y - rotationComplete > 180)) transform.localEulerAngles = new Vector3(0, 360 - turnAngleLimit, 0);
                else if ((touch.deltaPosition.x > 0) && ((rotationComplete + transform.localEulerAngles.y) > turnAngleLimit) && (transform.localEulerAngles.y + rotationComplete < 180)) transform.localEulerAngles = new Vector3(0, turnAngleLimit, 0);
                else
                {
                    transform.Rotate(0, rotationComplete, 0);   //rotates the player if not exceeding turnAngleLimit, otherwise snaps to turnAngleLimit
                }
            }
            else
            {
                unRotatePlayer();   //unrotate player if the finger is not moving
            }
        }
        else
        {
            unRotatePlayer();   //unrotate player if the person is not touching the screen
        }

        if (isVampireForm) myCharacterController.SimpleMove(new Vector3(0f, 0f, 0f));   //applies gravity to the vampire and no the bat
        myCharacterController.Move(Vector3.forward * speed * Time.deltaTime);

    }

    public void pickUpUmbrella()
    {
        isUmbrellaAvailable = true;
        playerUmbrella.GetComponent<UmbrellaHP>().addHP();
    }

    public GameObject getPlayerUmbrella()
    {
        return playerUmbrella;
    }


    public void doDamage(float dmg)
    {
        if (Time.timeScale == 0) return;
        HP = HP < 0 ? 0 : HP - dmg;
        healthBar.SetHealth(HP);

        if (HP <= 0)
        {
            if (!isDeathDisabled) gameController.GameOver(); //for debug menu
            else
            {
                debugDeathCount++;
                Debug.Log("Death by sun frying " + debugDeathCount);
            }
        }
    }

    public void addHP(float hp)
    {
        if (Time.timeScale == 0) return;
        HP = HP > maxHP ? maxHP : HP + hp;
        healthBar.SetHealth(HP);
    }

    public void setMaxHP(float hp)      //for debugging purposes
    {
        maxHP = hp;
        healthBar.SetMaxHealth(maxHP);      
    }

    public float getMaxHP()      //for debugging purposes
    {
        return maxHP;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            if (!isDeathDisabled) gameController.GameOver(); //for debug menu
            else
            {
                debugDeathCount++;
                Debug.Log("Death by collision " + debugDeathCount);
            }

        }
    }

}

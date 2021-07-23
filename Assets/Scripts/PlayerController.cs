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

    void Start()
    {
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
        }
        else
        {
            vampire.gameObject.SetActive(true);
            bat.gameObject.SetActive(false);        //switching to vampire form
            isVampireForm = true;
            deepCopyCharacterController(vampire);
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
        HP -= dmg;
        healthBar.SetHealth(HP);
    }
}

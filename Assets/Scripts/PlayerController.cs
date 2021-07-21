using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 touchPosition;
    private Vector3 direction;
    public float speed = 7.0f;
    private CharacterController myCharacterController;
    public GameObject tile;

    public GameObject vampire, bat;

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

    private GameObject playerUmbrella;

    public float turnAngleLimit = 20;

    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        vampire.gameObject.SetActive(true);
        bat.gameObject.SetActive(false);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("UmbrellaPlayer"))
            {
                playerUmbrella = transform.GetChild(i).gameObject;      //finding the umbrella in children
                break;
            }
        }

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
            //  myCharacterController = bat.GetComponent<CharacterController>();
        }
        else
        {
            vampire.gameObject.SetActive(true);
            bat.gameObject.SetActive(false);        //switching to vampire form
            isVampireForm = true;
            deepCopyCharacterController(vampire);
            // myCharacterController = vampire.GetComponent<CharacterController>();
        }
    }

    private void deepCopyCharacterController(GameObject o)
    {
        CharacterController cc = o.GetComponent<CharacterController>();
        myCharacterController.slopeLimit = cc.slopeLimit;
        myCharacterController.stepOffset = cc.stepOffset;
        myCharacterController.skinWidth = cc.skinWidth;
        myCharacterController.minMoveDistance = cc.minMoveDistance;
        myCharacterController.center = cc.center + new Vector3(0, o.transform.localPosition.y, 0);
        myCharacterController.radius = cc.radius;
        myCharacterController.height = cc.height;
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

        float constantMultiplier = 50;  //touch.deltaPosition values are between 0 to 150, so this multiplier was chosen accordingly
        float rotationComplete = rotationSpeed * Time.deltaTime * constantMultiplier;
        if (transform.localEulerAngles.y != 0)
        {



            if (transform.localEulerAngles.y < 180)
            {
                // Debug.Log(transform.localEulerAngles.y - rotationSpeed * Time.deltaTime * constantMultiplier);
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
        //  if (Input.GetMouseButtonDown(0))
        //  {
        //   //  switchCharacter();
        //  }
        detectDoubleTap();

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {


                myCharacterController.Move(Vector3.right * (touch.deltaPosition.x * turnSpeed) * Time.deltaTime);

                //  if (transform.localEulerAngles.y < turnAngleLimit || transform.localEulerAngles.y > 360 - turnAngleLimit)   //rotating the player
                //  {
                float rotationComplete = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;

                if ((touch.deltaPosition.x < 0) && ((transform.localEulerAngles.y - rotationComplete) < (360 - turnAngleLimit)) && (transform.localEulerAngles.y - rotationComplete > 180)) transform.localEulerAngles = new Vector3(0, 360 - turnAngleLimit, 0);
                else if ((touch.deltaPosition.x > 0) && ((rotationComplete + transform.localEulerAngles.y) > turnAngleLimit) && (transform.localEulerAngles.y + rotationComplete < 180)) transform.localEulerAngles = new Vector3(0, turnAngleLimit, 0);
                else
                {
                    transform.Rotate(0, rotationComplete, 0);

                }
                //    }
                //   else
                //   {
                //       if(transform.localEulerAngles.y<180 && touch.deltaPosition.x  < 0) transform.Rotate(0, Mathf.Sign(touch.deltaPosition.x) * rotationSpeed , 0);
                //       if (transform.localEulerAngles.y > 180 && touch.deltaPosition.x > 0) transform.Rotate(0, Mathf.Sign(touch.deltaPosition.x) * rotationSpeed , 0);
                //  }



                //  myCharacterController.enabled = false;
                //  transform.position = new Vector3(transform.position.x + (touch.deltaPosition.x* turnSpeed)  * Time.deltaTime, transform.position.y, transform.position.z);
                // myCharacterController.enabled = true;
            }
            else
            {
                unRotatePlayer();
            }
        }
        else
        {
            unRotatePlayer();
        }

        Vector3 moveVector = Vector3.forward * speed * Time.deltaTime;
        if (isVampireForm) myCharacterController.SimpleMove(new Vector3(0f, 0f, 0f));// else moveVector += new Vector3(0, -0.0001f, 0);  //gravity move
        myCharacterController.Move(moveVector);
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+speed*Time.deltaTime);    //empty game object "Player" moves




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

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

    public GameObject vampire,bat;

    private bool isVampireForm = true;

    private int tapCount=0;           //double tap variables
    private float doubleTapTimer = 0f, doubleTapWaitTime=0.2f;

    private float maxHP = 100, HP = 100;
    public HealthBar healthBar;

    public float turnSpeed = 0.15f;  //0.15f is the proper speed, could be scaled differently later

    public bool IsVampireForm { get => isVampireForm; set => isVampireForm = value; }

    private bool isUmbrellaAvailable = false;
    public bool IsUmbrellaAvailable { get => isUmbrellaAvailable; set => isUmbrellaAvailable = value; }

    private GameObject playerUmbrella;

    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        vampire.gameObject.SetActive(true);
        bat.gameObject.SetActive(false);
        
        for(int i = 0; i < transform.childCount; i++)
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
        myCharacterController.slopeLimit= cc.slopeLimit;
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
        if (tapCount >= 2 )
        {
            switchCharacter();
            doubleTapTimer = 0.0f;
            tapCount = 0;
        }
        if(doubleTapTimer > doubleTapWaitTime)
        {
            doubleTapTimer = 0.0f;
            tapCount = 0;
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
            if(touch.phase == TouchPhase.Moved)
            {
                
                
                  myCharacterController.Move(transform.right*(touch.deltaPosition.x * turnSpeed) * Time.deltaTime);
               
              //  myCharacterController.enabled = false;
              //  transform.position = new Vector3(transform.position.x + (touch.deltaPosition.x* turnSpeed)  * Time.deltaTime, transform.position.y, transform.position.z);
               // myCharacterController.enabled = true;
            }
        }
        if(isVampireForm) myCharacterController.SimpleMove(new Vector3(0f, 0f, 0f));  //gravity move
        myCharacterController.Move(transform.forward * speed * Time.deltaTime);
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+speed*Time.deltaTime);    //empty game object "Player" moves




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

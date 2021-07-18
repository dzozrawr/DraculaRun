using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 touchPosition;
    private Vector3 direction;
    public float speed = 7.0f;
    private CharacterController myCharacterController;
    private CharacterController vampireCC,batCC;    //CC stands for character controller
    public GameObject tile;
    private float movementWidthRange;

    public GameObject vampire,bat;

    private bool isVampireForm = true;

    private int tapCount=0;           //double tap variables
    private float doubleTapTimer = 0f, doubleTapWaitTime=0.5f;

    private float maxHP = 100, HP = 100;
    public HealthBar healthBar;
    void Start()
    {
        vampire.gameObject.SetActive(true);
        bat.gameObject.SetActive(false);
        vampireCC = vampire.GetComponent<CharacterController>();
        batCC= bat.GetComponent<CharacterController>();
        movementWidthRange = tile.transform.localScale.x*0.8f;   //movement width range depends on tile width

        healthBar.SetMaxHealth(maxHP);  //healthbar configuration
    }

    private void switchCharacter()
    {
        if (isVampireForm)
        {
            vampire.gameObject.SetActive(false);    //switching to bat form
            bat.gameObject.SetActive(true);
            isVampireForm = false;
            myCharacterController = bat.GetComponent<CharacterController>();
        }
        else
        {
            vampire.gameObject.SetActive(true);
            bat.gameObject.SetActive(false);        //switching to vampire form
            isVampireForm = true;
            myCharacterController = vampire.GetComponent<CharacterController>();
        }
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
            touchPosition = touch.position;

            touchPosition.z=Mathf.Sqrt(Mathf.Pow(movementWidthRange / 2,2)- Mathf.Pow(movementWidthRange / 4, 2));

            //touchPosition.z = 2;    //maybe change this value later (experiment)
            touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            touchPosition.z = 0;

            // myCharacterController.enabled = false;
            // myCharacterController.transform.position= new Vector3(touchPosition.x, myCharacterController.transform.position.y, myCharacterController.transform.position.z);
            // myCharacterController.enabled = true;
            transform.position = new Vector3(touchPosition.x, transform.position.y, transform.position.z);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+speed*Time.deltaTime);    //empty game object "Player" moves
     //   vampireCC.SimpleMove(new Vector3(0f, 0f, 0f));
     //   vampireCC.Move(transform.forward * speed * Time.deltaTime);

     //   batCC.SimpleMove(new Vector3(0f, 0f, 0f));
     //   batCC.Move(transform.forward * speed * Time.deltaTime);


        // myCharacterController.SimpleMove(new Vector3(0f, 0f, 0f));
        // myCharacterController.Move(transform.forward*speed*Time.deltaTime);
    }

    public void doDamage(float dmg)
    {
        HP -= dmg;
        healthBar.SetHealth(HP);
    }
}

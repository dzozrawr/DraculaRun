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

    // private int debugDeathCount = 0;   

    public bool isDeathDisabled = false;

    private GameController gameController;

    private CharacterController cc; //temp variable
    private float rotationComplete;  //temp variable

    private AudioSource runningOrFlyingAudioSrc;


    private float manualUnrotateMultiplier;

    [SerializeField] private Animator draculaAnimator;
    [SerializeField] private Animator batAnimator;


    [SerializeField] private GameObject draculaAsh, batAsh; //ash models for when the characters burn in the sun
    [SerializeField] private ParticleSystem draculaDeathSmoke, batDeathSmoke;
    // [SerializeField] private GameObject batAsh;

    private bool isGameOver = false;
    private void Awake()
    {
        HP = maxHP = 100;
        myCharacterController = GetComponent<CharacterController>();
        vampire.gameObject.SetActive(true);
        bat.gameObject.SetActive(false);
        playerUmbrella.SetActive(false);
    }
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        healthBar.SetMaxHealth(maxHP);  //healthbar configuration      
        runningOrFlyingAudioSrc = vampire.gameObject.GetComponent<AudioSource>();

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
            SFXManager.PlaySound("transformation");

            if (runningOrFlyingAudioSrc.isPlaying) runningOrFlyingAudioSrc.Stop();
            runningOrFlyingAudioSrc = bat.gameObject.GetComponent<AudioSource>();
        }
        else
        {


            vampire.gameObject.SetActive(true);
            bat.gameObject.SetActive(false);        //switching to vampire form
            isVampireForm = true;
            deepCopyCharacterController(vampire);
            vampire.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            SFXManager.PlaySound("transformation");

            if (runningOrFlyingAudioSrc.isPlaying) runningOrFlyingAudioSrc.Stop();
            runningOrFlyingAudioSrc = vampire.gameObject.GetComponent<AudioSource>();

            bat.gameObject.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 0, 0);    //return bat model position to 0, otherwise the bat slowly ascends over time, because of the animation interruption
        }
    }

    private void deepCopyCharacterController(GameObject o)  //copies values of another character controller to the main character controller (bat to vampire and vice versa)
    {
        cc = o.GetComponent<CharacterController>();
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
        rotationComplete = rotationSpeed * Time.deltaTime * unRotateMultiplier;
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

    public void playSFX()
    {
        if (isGameOver) return;
        if (Time.timeScale == 0) return;

        if (!runningOrFlyingAudioSrc.isPlaying) runningOrFlyingAudioSrc.Play(); //for looping running sound or flying sound
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;

        detectDoubleTap();

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {


                myCharacterController.Move(Vector3.right * (touch.deltaPosition.x * turnSpeed) * Time.deltaTime);   //moves the player left and right

                rotationComplete = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;    //beginning of rotating the player

                manualUnrotateMultiplier = 3;

                if ((touch.deltaPosition.x < 0) && ((transform.localEulerAngles.y - rotationComplete) < (360 - turnAngleLimit)) && (transform.localEulerAngles.y - rotationComplete > 180)) transform.localEulerAngles = new Vector3(0, 360 - turnAngleLimit, 0); //the left turn angle limit
                else if ((touch.deltaPosition.x > 0) && ((rotationComplete + transform.localEulerAngles.y) > turnAngleLimit) && (transform.localEulerAngles.y + rotationComplete < 180)) transform.localEulerAngles = new Vector3(0, turnAngleLimit, 0); //the right turn angle limit
                else
                {
                    if (transform.localEulerAngles.y > 180 && touch.deltaPosition.x > 0)    //speed up the manual unrotation from left to right
                    {
                        rotationComplete *= manualUnrotateMultiplier;                       
                    }

                    if (transform.localEulerAngles.y != 0 && transform.localEulerAngles.y < 180 && touch.deltaPosition.x < 0) //speed up the manual unrotation from right to left
                    {
                        rotationComplete *= manualUnrotateMultiplier;
                    }

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

        if (isVampireForm) myCharacterController.SimpleMove(new Vector3(0f, 0f, 0f));   //applies gravity to the vampire and not the bat
        myCharacterController.Move(Vector3.forward * speed * Time.deltaTime);

        playSFX();
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
        if (isGameOver) return;
        if (Time.timeScale == 0) return;
        HP = (HP - dmg) < 0 ? 0 : HP - dmg;
        healthBar.SetHealth(HP);


        if (HP <= 0)
        {
            if (!isDeathDisabled) 
            {
                isGameOver = true;

                if (isVampireForm)
                {
                    vampire.gameObject.SetActive(false);    //disable vampire
                    draculaDeathSmoke.Play();   //play smoke particles
                    draculaAsh.SetActive(true); //enable ash and animation starts automatically

                }
                else
                {
                    bat.gameObject.SetActive(false);
                    batDeathSmoke.Play();
                    batAsh.SetActive(true);
                }                                
                runningOrFlyingAudioSrc.Stop();
                gameController.timePassed.stopTime();                
            }
            else//for debug menu
            {
                //   debugDeathCount++;
                //   Debug.Log("Death by sun frying " + debugDeathCount);
            }
        }
    }

    public void addHP(float hp)
    {
        if (isGameOver) return;
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
            if (!isDeathDisabled)
            {
                if (!isGameOver)    //isGameOver flag is needed now for animations, because the game is over before the GAME OVER screen and at the beginning of the animation
                {
                    //draculaAnimator.Get
                    isGameOver = true;

                    if (isVampireForm)
                    {
                        draculaAnimator.SetTrigger("Death");
                    }
                    else
                    {
                        batAnimator.SetTrigger("Collision");
                    }
                    HP = 0;
                    healthBar.SetHealth(HP);
                    runningOrFlyingAudioSrc.Stop();
                    gameController.timePassed.stopTime();
                }
            }//for debug menu
            else
            {
                //   debugDeathCount++;
                //   Debug.Log("Death by collision " + debugDeathCount);
            }

        }
    }

    public bool is_gameOver()   //maybe should be in GameController, but the variable isGameOver is used frequently in PlayerController
    {
        return isGameOver;
    }

}
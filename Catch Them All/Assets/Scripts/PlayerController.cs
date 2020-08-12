using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    protected Joystick joystick;

    private float moveInput;
    private float jumparrangement = 0f;
    public Text winner;
    public Text gameover;
    public Text timer;
    public static float timeleft = 10.5f;
    private float temptimer = 4f;
    public static int timeleftcount = 0;
    public static float runspeed = 4f;
    public static float jumpForce = 5f;

    public static Rigidbody character;
    private Animator anim;

    private GameObject Catcher;
    private GameObject Catcher2;
    private GameObject Catcher3;
    public GameObject YellowCharacter;
    public GameObject BlueCharacter;

    public bool paused = false;

    public static bool chosenone = true;
    public static bool yellowchosen = false;
    public static bool bluechosen = false;

    public static bool yellowpaused = false;
    public static bool bluepaused = false;
    public static float pausedtimer = 1.5f;


    public bool letJump = false;

    public bool letClickRed = false;
    public bool letClickYellow = false;
    public bool letClickBlue = false;

    private Vector3 range;


    void Awake()
    {
        character = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Catcher = GameObject.Find("Catcher");
        Catcher2 = GameObject.Find("Catcher2");
        Catcher3 = GameObject.Find("Catcher3");
    }

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        Catcher2.gameObject.SetActive(false);
        Catcher3.gameObject.SetActive(false);
    }

    void Update()
    {
        SpecialCases();
        if (!paused)
        {
            timeleft -= Time.deltaTime;
            timer.text = "Time Left To Say Goodbye: " + (int)timeleft;
            CheckCollisionForJump();
            pausedtimer -= Time.deltaTime;
            if (pausedtimer < 0)
            {
                yellowpaused = false;
                bluepaused = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (!paused)
        {
            Hareket();
        }
        Catching();
        
    }

    void Hareket()
    { 
        character.position += new Vector3(joystick.Horizontal * Time.deltaTime * runspeed, 0 , joystick.Vertical * Time.deltaTime * runspeed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0) anim.SetBool("Walk", true);
        else if (Math.Abs(character.velocity.x) < 1f && Math.Abs(character.velocity.z) < 1f) anim.SetBool("Walk", false);
        else anim.SetTrigger("Stop");
        float h1 = joystick.Horizontal;
        float v1 = joystick.Vertical;
        if (h1 == 0f && v1 == 0f)
        { 
            Vector3 curRot = character.transform.localEulerAngles;   
            character.transform.localEulerAngles = Vector3.Slerp(curRot, curRot, Time.deltaTime * 2);
        }
        else
        {
            character.transform.localEulerAngles = new Vector3(0f, Mathf.Atan2(h1, v1) * 180 / Mathf.PI, 0f); 
        }

    }

    void OnCollisionStay(Collision col)
    {
        if (col.transform.tag == "Ground") letJump = true; 
        else letJump = false;
        if (col.transform.tag == "CatchYellow") letClickYellow = true;
        else letClickYellow = false;
        if (col.transform.tag == "CatchBlue") letClickBlue = true;
        else letClickBlue = false;
        if (col.transform.tag == "Coin")
        {
            MenuController.money++;
            PlayerPrefs.SetInt("Money", MenuController.money);
            Destroy(col.gameObject);
        }
    }

    void Catching()
    {
        if (letClickYellow && chosenone)
        {
            if ((Input.GetKeyDown(KeyCode.P)) || CrossPlatformInputManager.GetButtonDown(("Catch")))
            {
                pausedtimer = 1.5f;
                letClickYellow = false;
                chosenone = false;
                yellowchosen = true;
                yellowpaused = true;  
            }
        }
        if (letClickBlue && chosenone)
        {
            if ((Input.GetKeyDown(KeyCode.P)) || CrossPlatformInputManager.GetButtonDown(("Catch")))
            {
                pausedtimer = 1.5f;
                letClickBlue = false;
                chosenone = false;
                bluechosen = true;
                bluepaused = true;
            }
        }
        if (chosenone)
        {
            Catcher.gameObject.SetActive(true);
            Catcher2.gameObject.SetActive(false);
            Catcher3.gameObject.SetActive(false);
            if (timeleft < 0f)
            {
                paused = true;
                gameover.gameObject.SetActive(true);
                temptimer -= Time.deltaTime;
                if (temptimer < 0f)
                {
                    AdManager.Display_InterstitialAD();
                    SceneManager.LoadScene("GameMenu");
                }
            }
        }
        else if (timeleftcount == 2)
        {
            timeleft = 0;
            winner.gameObject.SetActive(true);
            winner.color = Color.red;
            winner.text = "The Winner is RED!";
            temptimer -= Time.deltaTime;
            if (temptimer < 0f)
            {
                LevelController.checklevel();
                AdManager.Display_InterstitialAD();
                SceneManager.LoadScene("GameMenu");
            }
        }
        if (yellowchosen)
        {
            Catcher.gameObject.SetActive(false);
            Catcher2.gameObject.SetActive(true);
            Catcher3.gameObject.SetActive(false);
            if (timeleft < 0f)
            {
                if (timeleftcount == 0)
                {
                    YellowCharacter.gameObject.SetActive(false);
                    chosenone = true;
                    yellowchosen = false;
                    Catcher.gameObject.SetActive(true);
                    Catcher2.gameObject.SetActive(false);
                    Catcher3.gameObject.SetActive(false);
                    timeleft = 10.5f;
                    timeleftcount++;
                }
                else if(timeleftcount == 1)
                {
                    YellowCharacter.gameObject.SetActive(false);
                    timeleftcount++;
                    Catching();
                }
            }
        }
        if (bluechosen)
        {
            Catcher.gameObject.SetActive(false);
            Catcher2.gameObject.SetActive(false);
            Catcher3.gameObject.SetActive(true);
            if (timeleft < 0f)
            {
                if (timeleftcount == 0)
                {
                    BlueCharacter.gameObject.SetActive(false);
                    chosenone = true;
                    bluechosen = false;
                    timeleft = 10.5f;
                    timeleftcount++;
                }
                else if (timeleftcount == 1)
                {
                    BlueCharacter.gameObject.SetActive(false);
                    timeleftcount++;
                    Catching();
                }
            }
        }
    }

    void CheckCollisionForJump()
    {
        if (letJump)
        {
            jumparrangement = 0f;
            if ((Input.GetKeyDown(KeyCode.Space)) || CrossPlatformInputManager.GetButtonDown(("Jump")))
            {
                character.velocity = new Vector3(character.velocity.x, jumpForce, character.velocity.z);
                anim.SetBool("Jump", true);
                letJump = false;
            }
            else
            {
                anim.SetBool("Jump", false);
            }
        }
        else
        {
            jumparrangement += Time.deltaTime;
            if (jumparrangement <= 0.065f)
            {
                if ((Input.GetKeyDown(KeyCode.Space)) || CrossPlatformInputManager.GetButtonDown(("Jump")))
                {
                    character.velocity = new Vector3(character.velocity.x, jumpForce, character.velocity.z);
                    anim.SetBool("Jump", true);
                }
                else
                {
                    anim.SetBool("Jump", false);
                }
            }
        }
    }

    private void SpecialCases()
    {
        if((SceneManager.GetActiveScene().name == "Fall Scene" || SceneManager.GetActiveScene().name == "Ramp Scene") && character.position.y < -7f)
        {
            paused = true;
            gameover.gameObject.SetActive(true);
            temptimer -= Time.deltaTime;
            if (temptimer < 0f)
            {
                SceneManager.LoadScene("GameMenu");
            }
        }
    }

    public void durdur()
    {
        paused = true;
        Time.timeScale = 0;
    }
    public void devam()
    {
        paused = false;
        Time.timeScale = 1;
    }
}

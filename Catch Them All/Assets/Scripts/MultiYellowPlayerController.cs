using System;
using System.Threading;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class MultiYellowPlayerController : MonoBehaviour
{
    private float moveInput;
    private float jumparrangement = 0f;
    private float runspeed = 4f;
    private float jumpForce = 5f;

    private PhotonView photonView;

    public static Rigidbody character;
    private Animator anim;

    protected Joystick joystick;

    public static bool redchosen = true;
    public static bool yellowchosen = false;
    public static bool bluechosen = false;

    public bool letJump = false;
    public bool letCatchRed = false;
    public bool letCatchBlue = false;

    private Vector3 range;

    void Awake()
    {
        GameSetupController.taken++;
        photonView = GetComponent<PhotonView>();
        character = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
       
        joystick = FindObjectOfType<Joystick>();

    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            if (GameSetupController.yellowpaused && GameSetupController.pausedtimer > 0f)
            {

            }
            else
            {
                GameSetupController.yellowpaused = false;
                CheckCollisionForJump();
                Hareket();
                Catching();
            }
        }
    }

    void Hareket()
    {
        character.position += new Vector3(joystick.Horizontal * Time.deltaTime * runspeed, 0, joystick.Vertical * Time.deltaTime * runspeed);
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
        if (photonView.IsMine)
        {
            if (col.transform.tag == "Ground") letJump = true;
            else letJump = false;
            if (col.transform.tag == "CatchRed") letCatchRed = true;
            else letCatchRed = false;
            if (col.transform.tag == "CatchBlue") letCatchBlue = true;
            else letCatchBlue = false;
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

    void Catching()
    {
        if (yellowchosen)
        {
            if (letCatchRed && ((Input.GetKeyDown(KeyCode.P)) || CrossPlatformInputManager.GetButtonDown(("Catch"))))
            {
                yellowchosen = false;
                redchosen = true;
                GameSetupController.redchosen = true;
                GameSetupController.yellowchosen = false;
                GameSetupController.redpaused = true;
                GameSetupController.pausedtimer = 1.5f;
                photonView.RPC("RPC_CatchRed", RpcTarget.All, redchosen, yellowchosen, true, 1.5f);
            }
            if (letCatchBlue && ((Input.GetKeyDown(KeyCode.P)) || CrossPlatformInputManager.GetButtonDown(("Catch"))))
            {
                yellowchosen = false;
                bluechosen = true;
                GameSetupController.bluechosen = true;
                GameSetupController.yellowchosen = false;
                GameSetupController.bluepaused = true;
                GameSetupController.pausedtimer = 1.5f;
                photonView.RPC("RPC_CatchBlue", RpcTarget.All, bluechosen, yellowchosen, true, 1.5f);
            }
        }
    }

    [PunRPC]
    private void RPC_CatchRed(bool redchosen, bool yellowchosen, bool yellowpaused, float pausedtimer)
    {
        GameSetupController.yellowchosen = yellowchosen;
        GameSetupController.redchosen = redchosen;
        GameSetupController.redpaused = yellowpaused;
        GameSetupController.pausedtimer = pausedtimer;
    }
    [PunRPC]
    private void RPC_CatchBlue(bool bluechosen, bool yellowchosen, bool bluepaused, float pausedtimer)
    {
        GameSetupController.bluechosen = bluechosen;
        GameSetupController.yellowchosen = yellowchosen;
        GameSetupController.bluepaused = bluepaused;
        GameSetupController.pausedtimer = pausedtimer;
    }
}

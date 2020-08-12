using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSetupController : MonoBehaviourPun
{
    public Text timer;
    public Text winner;

    private PhotonView photonView;

    public static int multilevel = 0;

    private float timeleft = 12.5f;
    private float temptimer = 3.2f;

    private GameObject RedCharacter;
    private GameObject YellowCharacter;
    private GameObject BlueCharacter;

    public Vector3 yellowspawn;
    public Vector3 redspawn;
    public Vector3 bluespawn;

    private GameObject RedCatcher;
    private GameObject YellowCatcher;
    private GameObject BlueCatcher;

    public static bool redchosen = true;
    public static bool yellowchosen = false;
    public static bool bluechosen = false;

    public static bool redpaused = false;
    public static bool yellowpaused = false;
    public static bool bluepaused = false;
    public static float pausedtimer = 1.5f;

    public int randomNumber;
    public static int taken = 0;

    private Player p1, p2, p3;
    private bool disconnectyellow = false;
    private bool disconnectblue = false;

    private void Start()
    {
        taken = 0;
        redchosen = true;
        yellowchosen = false;
        bluechosen = false;
        photonView = GetComponent<PhotonView>();
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        if (PhotonNetwork.NickName == PhotonNetwork.PlayerList[0].NickName) PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Red Character"), redspawn, Quaternion.identity);
        else if (PhotonNetwork.NickName == PhotonNetwork.PlayerList[1].NickName) PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Yellow Character"), yellowspawn, Quaternion.identity);
        else if (PhotonNetwork.NickName == PhotonNetwork.PlayerList[2].NickName) PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Blue Character"), bluespawn, Quaternion.identity);
    }

    void Update()
    {
        if(PhotonNetwork.IsMasterClient) photonView.RPC("RPC_Timer", RpcTarget.Others, pausedtimer, timeleft);
        pausedtimer -= Time.deltaTime;
        timeleft -= Time.deltaTime;
        timer.text = "Time Left To Say Goodbye: " + (int)timeleft;
        if (taken == 2)
        {
            RedCharacter = GameObject.Find("Red Character(Clone)");
            YellowCharacter = GameObject.Find("Yellow Character(Clone)");
            BlueCharacter = GameObject.Find("Blue Character(Clone)");
            RedCatcher = RedCharacter.transform.Find("Catcher").gameObject;
            YellowCatcher = YellowCharacter.transform.Find("Catcher2").gameObject;
            BlueCatcher = BlueCharacter.transform.Find("Catcher3").gameObject;
            if (PhotonNetwork.IsMasterClient)
            {
                p1 = PhotonNetwork.PlayerList[0];
                p2 = PhotonNetwork.PlayerList[1];
                p3 = PhotonNetwork.PlayerList[2];
            }
            taken = 3;
        }
        else if (taken == 3 && PhotonNetwork.IsMasterClient) Catching();
        else NotMasterCatching();
    }

    [PunRPC]
    private void RPC_Taken(int taken1) => taken = taken1;

    [PunRPC]
    private void RPC_ObjectsBool(bool redcharacter, bool yellowcharacter, bool bluecharacter)
    {
        RedCharacter.SetActive(redcharacter);
        YellowCharacter.SetActive(yellowcharacter);
        BlueCharacter.SetActive(bluecharacter);
    }

    [PunRPC]
    private void RPC_Catching(bool redchosen1, bool yellowchosen1, bool bluechosen1, bool redpaused1, bool yellowpaused1, bool bluepaused1, float timeleft1)
    {
        redchosen = redchosen1;
        yellowchosen = yellowchosen1;
        bluechosen = bluechosen1;
        redpaused = redpaused1;
        yellowpaused = yellowpaused1;
        bluepaused = bluepaused1;
        timeleft = timeleft1; 
    }

    [PunRPC]
    private void RPC_Disconnect(bool yellow, bool blue)
    {
        disconnectblue = blue;
        disconnectyellow = yellow;
    }

    [PunRPC]
    private void RPC_Timer(float pausedtimer1, float timeleft1)
    {
        pausedtimer = pausedtimer1;
        timeleft = timeleft1;
    }

    private void RedWon()
    {
        winner.gameObject.SetActive(true);
        winner.color = Color.red;
        winner.text = "The Winner is RED!";
        temptimer -= Time.deltaTime;
        if (temptimer < 0f)
        {
            if (PhotonNetwork.NickName == PhotonNetwork.PlayerList[0].NickName)
            {
                if (multilevel == 1) MenuController.money += 2000;
                else if (multilevel == 2) MenuController.money += 10000;
                else if (multilevel == 3) MenuController.money += 60000;
                else if (multilevel == 4) MenuController.money += 500000;
            }
            PlayerPrefs.SetInt("Money", MenuController.money);
            AdManager.Display_InterstitialAD();
            SceneManager.LoadScene("GameMenu");
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
    }
    private void YellowWon()
    {
        winner.gameObject.SetActive(true);
        winner.color = Color.yellow;
        winner.text = "The Winner is YELLOW!";
        temptimer -= Time.deltaTime;
        if (temptimer < 0f)
        {
            if (PhotonNetwork.NickName == PhotonNetwork.PlayerList[1].NickName && RedCharacter != null)
            {
                if (multilevel == 1) MenuController.money += 2000;
                else if (multilevel == 2) MenuController.money += 10000;
                else if (multilevel == 3) MenuController.money += 60000;
                else if (multilevel == 4) MenuController.money += 500000;
            }
            PlayerPrefs.SetInt("Money", MenuController.money);
            AdManager.Display_InterstitialAD();
            SceneManager.LoadScene("GameMenu");
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
    }
    private void BlueWon()
    {
        winner.gameObject.SetActive(true);
        winner.color = Color.blue;
        winner.text = "The Winner is BLUE!";
        temptimer -= Time.deltaTime;
        if (temptimer < 0f)
        {
            if (PhotonNetwork.NickName == PhotonNetwork.PlayerList[2].NickName && RedCharacter != null && YellowCharacter != null)
            {
                if (multilevel == 1) MenuController.money += 2000;
                else if (multilevel == 2) MenuController.money += 10000;
                else if (multilevel == 3) MenuController.money += 60000;
                else if (multilevel == 4) MenuController.money += 500000;
            }
            if (PhotonNetwork.NickName == PhotonNetwork.PlayerList[1].NickName && RedCharacter == null && YellowCharacter != null)
            {
                if (multilevel == 1) MenuController.money += 2000;
                else if (multilevel == 2) MenuController.money += 10000;
                else if (multilevel == 3) MenuController.money += 60000;
                else if (multilevel == 4) MenuController.money += 500000;
            }
            if (PhotonNetwork.NickName == PhotonNetwork.PlayerList[1].NickName && RedCharacter != null && YellowCharacter == null)
            {
                if (multilevel == 1) MenuController.money += 2000;
                else if (multilevel == 2) MenuController.money += 10000;
                else if (multilevel == 3) MenuController.money += 60000;
                else if (multilevel == 4) MenuController.money += 500000;
            }
            PlayerPrefs.SetInt("Money", MenuController.money);
            AdManager.Display_InterstitialAD();
            SceneManager.LoadScene("GameMenu");
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
    }
    private void WinConditions()
    {
        if (RedCharacter != null && YellowCharacter == null && BlueCharacter == null) RedWon();
        else if (RedCharacter == null && YellowCharacter != null && BlueCharacter == null) YellowWon();
        else if (RedCharacter == null && YellowCharacter == null && BlueCharacter != null) BlueWon();
        else if (RedCharacter != null && YellowCharacter != null && BlueCharacter != null)
        {
            if (RedCharacter.activeSelf && !YellowCharacter.activeSelf && !BlueCharacter.activeSelf) RedWon();
            else if (!RedCharacter.activeSelf && YellowCharacter.activeSelf && !BlueCharacter.activeSelf) YellowWon();
            else if (!RedCharacter.activeSelf && !YellowCharacter.activeSelf && BlueCharacter.activeSelf) BlueWon();
        }
        else if (RedCharacter != null && YellowCharacter != null && BlueCharacter == null)
        {
            if (RedCharacter.activeSelf && !YellowCharacter.activeSelf) RedWon();
            else if (!RedCharacter.activeSelf && YellowCharacter.activeSelf) YellowWon();
        }
        else if (RedCharacter != null && YellowCharacter == null && BlueCharacter != null)
        {
            if (RedCharacter.activeSelf && !BlueCharacter.activeSelf) RedWon();
            else if (!RedCharacter.activeSelf && BlueCharacter.activeSelf) BlueWon();
        }
        else if (RedCharacter == null && YellowCharacter != null && BlueCharacter != null)
        {
            if (!BlueCharacter.activeSelf && YellowCharacter.activeSelf) YellowWon();
            else if (!YellowCharacter.activeSelf && BlueCharacter.activeSelf) BlueWon();
        }
    }

    private void NotMasterCatching()
    {
        MultiYellowPlayerController.redchosen = redchosen;
        MultiYellowPlayerController.yellowchosen = yellowchosen;
        MultiYellowPlayerController.bluechosen = redchosen;
        MultiBluePlayerController.redchosen = redchosen;
        MultiBluePlayerController.yellowchosen = yellowchosen;
        MultiBluePlayerController.bluechosen = bluechosen;

        if (YellowCharacter != null && BlueCharacter != null)
        {
            if (disconnectblue && PhotonNetwork.NickName == PhotonNetwork.PlayerList[2].NickName)
            {
                disconnectblue = false;
                SceneManager.LoadScene("GameMenu");
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();
                photonView.RPC("RPC_Disconnect", RpcTarget.All, false, false);
            }
            else if (disconnectyellow && PhotonNetwork.NickName == PhotonNetwork.PlayerList[1].NickName)
            {
                disconnectyellow = false;
                SceneManager.LoadScene("GameMenu");
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();
                photonView.RPC("RPC_Disconnect", RpcTarget.All, false, false);
            }  
        }
        else
        {
            if (disconnectblue && timeleft < 2f)
            {
                disconnectblue = false;
                SceneManager.LoadScene("GameMenu");
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();
            }
            else if (disconnectyellow && timeleft < 2f)
            {
                disconnectyellow = false;
                SceneManager.LoadScene("GameMenu");
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();
            }
        }

        if (redchosen)
        {
            if (RedCharacter != null) RedCatcher.SetActive(true);
            if (YellowCharacter != null) YellowCatcher.SetActive(false);
            if (BlueCharacter != null) BlueCatcher.SetActive(false);
        }
        else if (yellowchosen)
        {
            if (RedCharacter != null) RedCatcher.SetActive(false);
            if (YellowCharacter != null) YellowCatcher.SetActive(true);
            if (BlueCharacter != null) BlueCatcher.SetActive(false);
        }
        else if (bluechosen)
        {
            if (RedCharacter != null) RedCatcher.SetActive(false);
            if (YellowCharacter != null) YellowCatcher.SetActive(false);
            if (BlueCharacter != null) BlueCatcher.SetActive(true);
        }
        WinConditions();
    }

    private void Catching()
    {
        MultiRedPlayerController.redchosen = redchosen;
        MultiRedPlayerController.yellowchosen = yellowchosen;
        MultiRedPlayerController.bluechosen = redchosen;

        Debug.Log("Yellow " + YellowCharacter);
        Debug.Log("Red " + RedCharacter);
        Debug.Log("Blue " + BlueCharacter);

        photonView.RPC("RPC_Catching", RpcTarget.Others, redchosen, yellowchosen, bluechosen, redpaused, yellowpaused, bluepaused, timeleft);
        photonView.RPC("RPC_Disconnect", RpcTarget.All, disconnectyellow, disconnectblue);

        if (RedCharacter == null || YellowCharacter == null || BlueCharacter == null)
        {
            if (RedCharacter == null)
            {
                if(redchosen)
                {
                    if (YellowCharacter != null) yellowchosen = true;
                    else if (BlueCharacter != null) bluechosen = true;
                }
                redchosen = false;
            }
            if (YellowCharacter == null)
            {
                if (yellowchosen)
                {
                    if (RedCharacter != null) redchosen = true;
                    else if (BlueCharacter != null) bluechosen = true;
                }
                yellowchosen = false;
            }
            if (BlueCharacter == null)
            {
                if (bluechosen)
                {
                    if (YellowCharacter != null) yellowchosen = true;
                    else if (RedCharacter != null) redchosen = true;
                }
                bluechosen = false;
            }
        }
        else photonView.RPC("RPC_ObjectsBool", RpcTarget.Others, RedCharacter.activeSelf, YellowCharacter.activeSelf, BlueCharacter.activeSelf);

        if (redchosen)
        {
            if (RedCharacter != null) RedCatcher.SetActive(true);
            if (YellowCharacter != null) YellowCatcher.SetActive(false);
            if (BlueCharacter != null) BlueCatcher.SetActive(false);
            if (timeleft < 0f)
            {
                timeleft = 10.5f;
                RedCharacter.SetActive(false);
                redchosen = false;
                if (YellowCharacter != null) yellowchosen = true;
                else if (BlueCharacter != null) bluechosen = true;
            }
        }
        else if (yellowchosen)
        {
            if (RedCharacter != null) RedCatcher.SetActive(false);
            if (YellowCharacter != null) YellowCatcher.SetActive(true);
            if (BlueCharacter != null) BlueCatcher.SetActive(false);
            if (timeleft < 0f)
            {
                YellowCharacter.SetActive(false);
                disconnectyellow = true;
                if (RedCharacter == null) PhotonNetwork.CloseConnection(p1);
                yellowchosen = false;
                if (RedCharacter != null) redchosen = true;
                else if (BlueCharacter != null) bluechosen = true;
                timeleft = 10.5f;
            }
        }    
        else if (bluechosen)
        {
            if (RedCharacter != null) RedCatcher.SetActive(false);
            if (YellowCharacter != null) YellowCatcher.SetActive(false);
            if (BlueCharacter != null) BlueCatcher.SetActive(true);
            if (timeleft <= 0f)
            { 
                BlueCharacter.SetActive(false);
                disconnectblue = true;
                if (RedCharacter == null) PhotonNetwork.CloseConnection(p1);
                bluechosen = false;
                if (RedCharacter != null) redchosen = true;
                else if (YellowCharacter != null) yellowchosen = true;
                timeleft = 10.5f;
            }
        }
        if (timeleft > 2f)
        {
            disconnectyellow = false;
            disconnectblue = false;
        }
        WinConditions();
    }
}

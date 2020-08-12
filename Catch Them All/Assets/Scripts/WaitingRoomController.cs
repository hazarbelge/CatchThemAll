using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaitingRoomController : MonoBehaviour
{
    private PhotonView photonView;

    private int playerCount;
    private int roomSize;

    public Text PlayersCount;
    public Text MaxPlayersCount;
    public Text Timer;

    private bool readyToCountDown;
    private bool readyToStart;

    private float timerToStartGame;
    private float notFullGameTimer;
    private float fullGameTimer;

    [SerializeField]
    private float maxWaitTime;
    [SerializeField]
    private float maxFullGameTime;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullGameTime;
        notFullGameTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;

        PlayerCountUpdate();
    }

    void PlayerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        PlayersCount.text = "Players In The Room: " + playerCount;
        MaxPlayersCount.text = "Waiting More Players: " + (roomSize - playerCount);

        if(playerCount == roomSize)
        {
            readyToStart = true;
            readyToCountDown = true;
        }
        else
        {
            readyToCountDown = false;
            readyToStart = false;
        }
    }

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();
        if (PhotonNetwork.IsMasterClient) photonView.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        timerToStartGame = timeIn;
        notFullGameTimer = timeIn;
        if(timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }

    public void PlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate();
    }

    void WaitingForMorePlayer()
    {
        if (playerCount <= 2) ResetTimer();
        if (readyToStart)
        {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;
        }
        string tempTimer = string.Format("TimeToStart: {0:0}", timerToStartGame);
        Timer.text = tempTimer;

        if(timerToStartGame <= 0f)
        {
            StartGame();
        }
    }
    
    void ResetTimer()
    {
        timerToStartGame = maxWaitTime;
        notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGameTime;
    }

    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting Game");
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(5);
            SceneManager.LoadScene("Space Catch - Multiplayer");
        }
        else return;
    }

    void Update()
    {
        WaitingForMorePlayer();
        PlayerCountUpdate();
    }

    public void delaycancel()
    {
        if (GameSetupController.multilevel == 1) MenuController.money += 1000;
        if (GameSetupController.multilevel == 2) MenuController.money += 5000;
        if (GameSetupController.multilevel == 3) MenuController.money += 30000;
        if (GameSetupController.multilevel == 4) MenuController.money += 250000;
        PlayerPrefs.SetInt("Money", MenuController.money);
        PlayerPrefs.SetInt("XP", MenuController.xp);
        PlayerPrefs.SetInt("Diamond", MenuController.diamond);
        PlayerPrefs.SetInt("XPLevel", MenuController.xplevel);
        PlayerPrefs.SetInt("LevelPassed", MenuController.levelpassed);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("GameMenu");
    }
}

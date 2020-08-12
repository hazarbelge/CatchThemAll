using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiplayerMenuController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    public Text loading;
    [SerializeField]
    public GameObject quickstartbutton;
    [SerializeField]
    public GameObject backbutton;
    [SerializeField]
    private int RoomSize;

    public InputField playerNameInput;

    public void quickstart()
    {
        quickstartbutton.SetActive(false);
        PhotonNetwork.JoinRandomRoom();
    }

    public void disconnect()
    {
        quickstartbutton.SetActive(false);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("GameMenu");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        quickstartbutton.SetActive(true);
        backbutton.SetActive(true);
        loading.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("NickName") == null) PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
        else PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
        playerNameInput.text = PhotonNetwork.NickName;
    }

    public void PlayerNameUpdate(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Creating a room");
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, PublishUserId = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create a room... Try again");
        CreateRoom();
    }





}

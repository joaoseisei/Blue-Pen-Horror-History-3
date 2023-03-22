using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks{

    public static Launcher Instance;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject playerListItemPrefab;

    void Awake() {
        Instance = this;
    }

    void Start(){
        Debug.Log("Conectando no Master");
        PhotonNetwork.ConnectUsingSettings();
        
    }
    public override void OnConnectedToMaster(){
        Debug.Log("Conectado no Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby(){
        MenuController.Instance.OpenMenu("MenuInicial");
        Debug.Log("Entrando no Lobby");
        PhotonNetwork.NickName = "Caneta" + Random.Range(0,1000).ToString("0000");

    }
    
    public void CreateRoom(){
        if(string.IsNullOrEmpty(roomNameInputField.text)){
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuController.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom(){
        MenuController.Instance.OpenMenu("Sala");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Count(); i++){
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message){
        errorText.text = "Falha na criação de sala:" + message;
        MenuController.Instance.OpenMenu("erro");
    }
    public void LeaveRoom(){
        PhotonNetwork.LeaveRoom();
        MenuController.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info){
        PhotonNetwork.JoinRoom(info.Name);
        MenuController.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom(){
        MenuController.Instance.OpenMenu("MenuInicial");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        foreach(Transform trans in roomListContent){
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++){
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}

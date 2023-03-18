using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks{

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform roomListContent;

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
    }

    public override void OnCreateRoomFailed(short returnCode, string message){
        errorText.text = "Falha na criação de sala:" + message;
        MenuController.Instance.OpenMenu("erro");
    }
    public void LeaveRoom(){
        PhotonNetwork.LeaveRoom();
        MenuController.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom(){
        MenuController.Instance.OpenMenu("MenuInicial");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks{

    void Start(){
        Debug.Log("Conectando no Master");
        PhotonNetwork.ConnectUsingSettings();
        
    }
    public override void OnConnectedToMaster(){
        Debug.Log("Conectado no Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby(){
        Debug.Log("Entrando no Lobby");

    }

    void Update()
    {
        
    }
}

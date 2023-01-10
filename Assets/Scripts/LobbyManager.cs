using System.Runtime.CompilerServices;
using System.Threading;
using System.Collections.ObjectModel;
using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    public Text connectionInfoText;
    public Button joinButton;

    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable = false;
        connectionInfoText.text = "마스터 서버에 접속중";
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;

        connectionInfoText.text = "온라인 : 마스터 서버와 연결됨";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;

        connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";

        //연결
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {

        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            //룸 접속 실행
            connectionInfoText.text = "룸에 접속...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {

            connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";

            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        connectionInfoText.text = "빈 방이 없음, 새로운 방 생성...";
        RoomOptions ros = new RoomOptions();
        ros.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(null, ros);

    }

    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "방 참가 성공";

        PhotonNetwork.LoadLevel("Main");
    }
}

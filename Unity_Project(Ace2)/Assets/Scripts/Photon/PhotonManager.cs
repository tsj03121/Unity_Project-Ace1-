using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon;
using Photon.Pun;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    

    public void OnGameStart()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.DestroyAll();
        }
        Invoke("ExitRoom", 5f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitRoom();
        }
    }

    public override void OnLeftRoom()
    {
        //로비로 돌아가자
        //SceneManager.LoadScene("Lobby");
    }

    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

}

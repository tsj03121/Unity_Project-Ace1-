using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class CameraPlayer : MonoBehaviourPun
{
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        cam = GameObject.Find("Player Cam");
        CinemachineVirtualCamera UserCamera = cam.GetComponent<CinemachineVirtualCamera>();

        UserCamera.Follow = transform;
        UserCamera.LookAt = transform;
    }
}

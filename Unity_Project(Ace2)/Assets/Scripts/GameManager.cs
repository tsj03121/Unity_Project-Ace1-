using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PhotonManager photonManager_;

    public Action GameStart;

    void Awake()
    {
        photonManager_ = gameObject.AddComponent<PhotonManager>();
    }

    void Start()
    {
        BindEvents();
        GameStart?.Invoke();
    }

    void BindEvents()
    {
        GameStart += photonManager_.OnGameStart;
    }

    void UnBindEvents()
    {
        GameStart -= photonManager_.OnGameStart;
    }
    
}

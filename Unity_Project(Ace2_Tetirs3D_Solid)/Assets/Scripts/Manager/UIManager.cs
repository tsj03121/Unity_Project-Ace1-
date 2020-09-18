using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text lineClear_;

    [SerializeField]
    GameObject resultPanel_;

    [SerializeField]
    Text inGameLineClear_;

    public Action ReStartAction;

    public void ReStartButton()
    {    
        resultPanel_.SetActive(false);
        inGameLineClear_.text = "LineClear : 0";
        lineClear_.text = "0";
        ReStartAction?.Invoke();
    }

    public void OnGameOver()
    {
        resultPanel_.SetActive(true);
    }

    public void OnScoreUpdate(int lineClear)
    {
        inGameLineClear_.text = "LineClear : " + lineClear;
        lineClear_.text = lineClear.ToString();
    }
}

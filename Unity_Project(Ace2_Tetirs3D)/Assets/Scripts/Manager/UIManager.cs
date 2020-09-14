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

    public Action ReStart;

    public void ReStartButton()
    {
        UnityEditor.EditorApplication.Step();    
        resultPanel_.SetActive(false);
        ReStart?.Invoke();
    }

    public void OnGameOver()
    {
        resultPanel_.SetActive(true);
    }
}

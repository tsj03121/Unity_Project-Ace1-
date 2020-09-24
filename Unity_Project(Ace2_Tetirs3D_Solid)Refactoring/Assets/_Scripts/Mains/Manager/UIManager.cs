using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text _lineClear;

    [SerializeField]
    GameObject _resultPanel;

    [SerializeField]
    Text _inGameLineClear;

    public Action CallbackReStart;

    public void OnClickReStart()
    {    
        _resultPanel.SetActive(false);
        _inGameLineClear.text = "LineClear : 0";
        _lineClear.text = "0";
        CallbackReStart?.Invoke();
    }

    public void OnGameOver()
    {
        _resultPanel.SetActive(true);
    }

    public void OnScoreUpdate(int lineClear)
    {
        _inGameLineClear.text = "LineClear : " + lineClear;
        _lineClear.text = lineClear.ToString();
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}

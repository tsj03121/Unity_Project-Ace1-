using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyGameStart : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Main");
    }
}

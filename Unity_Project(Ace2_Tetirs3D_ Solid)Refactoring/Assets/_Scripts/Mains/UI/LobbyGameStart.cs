using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyGameStart : MonoBehaviour
{
    public void OnClickGameStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickShapeEditorStart()
    {
        SceneManager.LoadScene("ShapeEditor");
    }
}

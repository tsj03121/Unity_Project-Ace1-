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

#if UNITY_EDITOR
    public void OnClickShapeEditorStart()
    {
        SceneManager.LoadScene("ShapeEditor");
    }
#endif
}

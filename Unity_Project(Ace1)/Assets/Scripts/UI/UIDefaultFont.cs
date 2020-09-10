using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDefaultFont : MonoBehaviour
{
    [SerializeField]
    Text text_;

    public void OnStageLevelDefaultSize()
    {
        text_.fontSize = 40;
        text_.fontStyle = FontStyle.Normal;
    }
}

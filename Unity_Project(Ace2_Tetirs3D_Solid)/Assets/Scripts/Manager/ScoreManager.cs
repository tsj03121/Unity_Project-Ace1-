using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    int LineClear = 0;

    public Action<int> ScoreUpdateAction;

    public void OnLineClear()
    {
        LineClear += 1;
        ScoreUpdateAction?.Invoke(LineClear);
    }

    public void OnReStart()
    {
        LineClear = 0;
    }

    public void OnGameOver()
    {

    }
}

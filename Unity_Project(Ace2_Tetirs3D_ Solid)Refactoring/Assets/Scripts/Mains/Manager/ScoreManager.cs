using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public Action<int> CallbackScoreUpdate;

    int _lineClear = 0;

    public void OnLineClear()
    {
        _lineClear += 1;
        CallbackScoreUpdate?.Invoke(_lineClear);
    }

    public void OnReStart()
    {
        _lineClear = 0;
    }
}

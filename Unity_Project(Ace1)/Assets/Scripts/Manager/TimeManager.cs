using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    bool isGameStarted_ = false;
    public Action GameStarted;

    public void StartGame(float timeToStart = 3f)
    {
        if (isGameStarted_)
            return;

        isGameStarted_ = true;
        StartCoroutine(DelayedGameStart(timeToStart));
    }

    IEnumerator DelayedGameStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        GameStarted?.Invoke();
    }
}

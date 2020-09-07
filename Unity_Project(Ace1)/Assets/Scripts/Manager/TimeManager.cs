using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    bool isGameStarted = false;
    public Action GameStarted;

    public void StartGame(float timeToStart = 3f)
    {
        if (isGameStarted)
            return;

        isGameStarted = true;
        StartCoroutine(DelayedGameStart(timeToStart));
    }

    IEnumerator DelayedGameStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        GameStarted?.Invoke();
    }
}

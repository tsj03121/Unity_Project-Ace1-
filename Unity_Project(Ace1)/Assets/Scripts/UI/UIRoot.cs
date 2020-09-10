using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIRoot : MonoBehaviour
{
    [SerializeField]
    Text scoreUI_;

    [SerializeField]
    Text resultUI_;

    [SerializeField]
    Text stageLevelUI_;

    [SerializeField]
    Button reStartUI_;

    [SerializeField]
    UIAnimation uIAnimation_;

    public Action<int> ReStart;

    public UIAnimation GetUIAnimation() { return uIAnimation_; }

    // Start is called before the first frame update
    void Start()
    {
        scoreUI_.gameObject.SetActive(false);
        resultUI_.gameObject.SetActive(false);
        stageLevelUI_.gameObject.SetActive(false);
        reStartUI_.gameObject.SetActive(false);
    }

    public void OnGameStarted()
    {
        scoreUI_.gameObject.SetActive(true);
        stageLevelUI_.gameObject.SetActive(true);
        scoreUI_.text = string.Format("Score: {0}", 0);
        stageLevelUI_.text = "Stage 1";
    }

    public void OnScoreChanged(int score)
    {
        scoreUI_.text = string.Format("Score: {0}", score);
    }

    public void OnGameEnded(bool isVictory, int buildingCount)
    {
        resultUI_.gameObject.SetActive(true);
        reStartUI_.gameObject.SetActive(true);

        if (isVictory)
        {
            resultUI_.text = "You Win!";
            stageLevelUI_.gameObject.SetActive(true);
        }
        else
        {
            resultUI_.text = "You Lose";
            stageLevelUI_.gameObject.SetActive(true);
        }
    }

    public void OnStageUp(int stageLevel)
    {
        stageLevelUI_.text = "Stage " + stageLevel.ToString();
    }

    public void ReStartButton()
    {
        resultUI_.gameObject.SetActive(false);
        reStartUI_.gameObject.SetActive(false);
        stageLevelUI_.gameObject.SetActive(true);
        ReStart?.Invoke(0);
    }
}

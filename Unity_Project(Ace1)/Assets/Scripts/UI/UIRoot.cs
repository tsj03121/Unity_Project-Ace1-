using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    [SerializeField]
    Text scoreUI;

    [SerializeField]
    Text resultUI;

    [SerializeField]
    Text stageLevelUI;

    // Start is called before the first frame update
    void Start()
    {
        scoreUI.gameObject.SetActive(false);
        resultUI.gameObject.SetActive(false);
        stageLevelUI.gameObject.SetActive(false);
    }

    public void OnGameStarted()
    {
        scoreUI.gameObject.SetActive(true);
        stageLevelUI.gameObject.SetActive(true);
        scoreUI.text = string.Format("Score: {0}", 0);
        stageLevelUI.text = "Stage 1";
    }

    public void OnScoreChanged(int score)
    {
        scoreUI.text = string.Format("Score: {0}", score);
    }

    public void OnGameEnded(bool isVictory, int buildingCount)
    {
        resultUI.gameObject.SetActive(true);
        resultUI.text = isVictory ? "You Win!" : "You Lose";
    }

    public void OnStageUp(int stageLevel)
    {
        stageLevelUI.text = "Stage " + stageLevel.ToString();
    }
}

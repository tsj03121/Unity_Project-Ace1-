using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    [SerializeField]
    Animator stageLevelAnimator_;

    [SerializeField]
    Text stageLevelText_;

    int endStageLevel_;

    public void SetEndStageLevel(int stageLevel) { endStageLevel_ = stageLevel; }

    public void OnStageLevelTextMove(int stageLevel)
    {
        if (stageLevel == 1)
            return;

        if (stageLevel >= endStageLevel_)
            return;

        stageLevelText_.fontSize = 180;
        stageLevelText_.fontStyle = FontStyle.Italic;
        stageLevelAnimator_.SetTrigger("StageLevelUp");
    }
}

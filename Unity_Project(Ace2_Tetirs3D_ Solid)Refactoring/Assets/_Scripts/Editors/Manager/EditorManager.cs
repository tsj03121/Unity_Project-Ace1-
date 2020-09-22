using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditorManager : MonoBehaviour
{
    [SerializeField]
    EditorController _editorController;

    [SerializeField]
    EditCube _cubeEdit;

    [SerializeField]
    Animator _panelAnimator;

    [SerializeField]
    RectTransform _panelArrow;

    [SerializeField]
    Text[] isChecks;

    public Action CallbackSave;
    public Action<Text> CallbackSaveNameChange;
    public Action<Slider> CallbackSliderChange;

    public Func<bool> CallbackCreateCube;
    public Func<bool> CallbackDeleteCube;

    bool _panelView = false;

    void Start()
    {
        _editorController.CallbackCreateClick += _cubeEdit.OnCreateClick;
        _editorController.CallbackDeleteClick += _cubeEdit.OnDeleteClick;
        _editorController.CallbackGetCubeLocalPosCheck += _cubeEdit.OnGetCubeLocalPosCheck;

        CallbackCreateCube += _editorController.OnCreateCube;
        CallbackDeleteCube += _editorController.OnDeleteCube;

        CallbackSliderChange += _cubeEdit.OnSliderChange;
        CallbackSaveNameChange += _cubeEdit.OnSaveNameChange;
        CallbackSave += _cubeEdit.OnSave;
    }

    void IsChecksTextReset()
    {
        for (int i = 0; i < isChecks.Length; i++)
        {
            isChecks[i].text = "False";
        }
    }

    public void OnClickPanelView()
    {
        _panelArrow.localScale *= -1;
        _panelView = !_panelView;

        if (_panelView)
        {
            _panelAnimator.SetTrigger("PanelOpen");
        }
        else
        {
            _panelAnimator.SetTrigger("PanelClose");
        }
    }

    public void SliderChange(Slider slider)
    {
        CallbackSliderChange?.Invoke(slider);
    }

    public void SaveNameChange(Text text)
    {
        CallbackSaveNameChange?.Invoke(text);
    }

    public void OnClickSave()
    {
        CallbackSave?.Invoke();
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickCubeCreate(Text text)
    {
        IsChecksTextReset();
        bool isCreateCube = (bool) CallbackCreateCube?.Invoke();
        text.text = isCreateCube.ToString();
    }

    public void OnClickCubeDelete(Text text)
    {
        IsChecksTextReset();
        bool isDeleteCube = (bool) CallbackDeleteCube?.Invoke();
        text.text = isDeleteCube.ToString();
    }
}

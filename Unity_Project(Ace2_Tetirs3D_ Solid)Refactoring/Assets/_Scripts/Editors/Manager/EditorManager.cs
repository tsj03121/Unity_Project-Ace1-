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

    [SerializeField]
    Dropdown _dropdown;

    [SerializeField]
    InputField _saveText;

    [SerializeField]
    Slider[] sliders;

    public Action CallbackSave;
    public Action CallbackReset;
    public Action<string> CallbackSaveNameChange;
    public Action<Slider> CallbackSliderChange;
    public Action<ShapePosData, Material> CallbackLoadListSelect;

    public Func<bool> CallbackCreateCube;
    public Func<bool> CallbackDeleteCube;

    bool _panelView = false;
    ShapeData _shapeData;

    void Start()
    {
        BindEvents();
        DataManager dataManager = DataManager.GetInstance();
        _cubeEdit.Init(dataManager);

        _shapeData = dataManager.GetShapeData();
        List<ShapePosData> posDatas = _shapeData.GetShapesPosDatas();
        DropdownOptionReset(posDatas);
    }

    void BindEvents()
    {
        _editorController.CallbackCreateClick += _cubeEdit.OnCreateClick;
        _editorController.CallbackDeleteClick += _cubeEdit.OnDeleteClick;
        _editorController.CallbackGetCubeLocalPosCheck += _cubeEdit.OnGetCubeLocalPosCheck;

        CallbackCreateCube += _editorController.OnCreateCube;
        CallbackDeleteCube += _editorController.OnDeleteCube;
        CallbackLoadListSelect += _cubeEdit.OnLoadListSelect;
        CallbackReset += _cubeEdit.OnReset;

        CallbackSliderChange += _cubeEdit.OnSliderChange;
        CallbackSaveNameChange += _cubeEdit.OnSaveNameChange;
        CallbackSave += _cubeEdit.OnSave;
    }

    void DropdownOptionReset(List<ShapePosData> posDatas)
    {
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();

        for (int i = 0; i < posDatas.Count; i++)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData();
            optionData.text = posDatas[i].name.ToString();
            optionDatas.Add(optionData);
        }

        _dropdown.AddOptions(optionDatas);
    }

    void LoadListSelectRGBSlider(Color color)
    {
        sliders[0].value = color.r;
        sliders[1].value = color.g;
        sliders[2].value = color.b;
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
        CallbackSaveNameChange?.Invoke(text.text);
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
        bool isCreateCube = (bool) CallbackCreateCube?.Invoke();
        text.text = isCreateCube.ToString();
    }

    public void OnClickCubeDelete(Text text)
    {
        bool isDeleteCube = (bool) CallbackDeleteCube?.Invoke();
        text.text = isDeleteCube.ToString();
    }

    public void LoadListSelect()
    {
        LoadListSelectRGBSlider(Color.white);
        CallbackReset?.Invoke();

        if (_dropdown.value == 0)
            return;

        int selectIndex = _dropdown.value - 1;

        _saveText.text = _dropdown.options[_dropdown.value].text;

        ShapePosData shapePosData = _shapeData.GetShapesPosDatas()[selectIndex];
        Material material = _shapeData.GetShapesMaterials()[selectIndex];

        LoadListSelectRGBSlider(material.color);

        CallbackSaveNameChange?.Invoke(_saveText.text);
        CallbackLoadListSelect?.Invoke(shapePosData, material);
    }
}

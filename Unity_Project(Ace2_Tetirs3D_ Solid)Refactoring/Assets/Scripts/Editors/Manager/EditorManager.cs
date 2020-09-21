using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EditorManager : MonoBehaviour
{
    [SerializeField]
    EditorController _editorController;

    [SerializeField]
    EditCube _cubeEdit;

    void Start()
    {
        _editorController.CallbackCreateClick += _cubeEdit.OnCreateClick;
        _editorController.CallbackDeleteClick += _cubeEdit.OnDeleteClick;
    }
}

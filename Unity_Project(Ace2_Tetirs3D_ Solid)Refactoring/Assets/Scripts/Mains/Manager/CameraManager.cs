using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] _shapes;

    GameObject _currNextShape;

    public void OnCreateBlock(ShapeConfirm.ShapeType shapeType)
    {
        if (ShapeConfirm.ShapeType.NULL == shapeType)
            return;

        int shapesIndex = (int)shapeType;

        _currNextShape = _shapes[shapesIndex];
        _currNextShape.gameObject.SetActive(true);
       
    }

    public void OnBlocksInput()
    {
        _currNextShape.gameObject.SetActive(false);
    }

    public void OnGameOver()
    {
        _currNextShape = null;
        for (int i = 0; i < _shapes.Length; ++i)
        {
            _shapes[i].gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{


    [SerializeField]
    GameObject _shapeListParent;

    [SerializeField]
    NextShapeRotation[] _shapes;

    GameObject _currNextShape;

    void Awake()
    {
        _shapes = _shapeListParent.GetComponentsInChildren<NextShapeRotation>();
        for(int i = 0; i < _shapes.Length; i++)
        {
            _shapes[i].transform.gameObject.SetActive(false);
        }
    }

    public void OnCreateBlock(ShapeConfirm.ShapeType shapeType)
    {
        if (ShapeConfirm.ShapeType.NULL == shapeType)
            return;

        int shapesIndex = (int)shapeType;

        _currNextShape = _shapes[shapesIndex].transform.gameObject;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] shapes;

    [SerializeField]
    float speed_ = 180f;

    [SerializeField]
    GameObject currNextShape;

    public void OnCreateBlock(ShapeType shapeType)
    {
        if (ShapeType.NULL == shapeType)
            return;

        int shapesIndex = (int)shapeType;

        currNextShape = shapes[shapesIndex];
        currNextShape.gameObject.SetActive(true);
       
    }

    public void OnBlocksInput()
    {
        currNextShape.gameObject.SetActive(false);
    }

    public void OnGameOver()
    {
        currNextShape = null;
        for (int i = 0; i < shapes.Length; ++i)
        {
            shapes[i].gameObject.SetActive(false);
        }
    }
}

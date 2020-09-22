using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{


    [SerializeField]
    GameObject _shapeListParent;

    [SerializeField]
    GameObject[] _shapes;

    [SerializeField]
    GameObject _currNextShape;

    void Awake()
    {
        int count = 0;
        NextShapeRotation[] shapes = Resources.LoadAll<NextShapeRotation>("Shapes");
        _shapes = new GameObject[shapes.Length];

        for (int i = 0; i < shapes.Length; i++)
        {
            GameObject viewShape = Instantiate(shapes[i].gameObject);
            _shapes[count] = viewShape;

            Vector3 localPos = viewShape.transform.position;
            viewShape.transform.parent = _shapeListParent.transform;
            localPos.y = 0.3f + viewShape.transform.position.y;
            viewShape.transform.localPosition = localPos;
            viewShape.transform.gameObject.SetActive(false);

            count += 1;
        }
    }

    public void OnCreateBlock(int shapeIndex)
    {
        if (shapeIndex == -1)
            return;

        _currNextShape = _shapes[shapeIndex].transform.gameObject;
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

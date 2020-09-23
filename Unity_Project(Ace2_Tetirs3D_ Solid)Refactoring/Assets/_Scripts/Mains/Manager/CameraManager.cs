using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        DataManager dataManager = DataManager.GetInstance();
        ShapeData shapeData = dataManager.GetShapeData();

        int count = 0;
        List<GameObject> shapes = shapeData.GetNextShapesView();
        _shapes = new GameObject[shapes.Count];

        for (int i = 0; i < shapes.Count; i++)
        {
            GameObject viewShape = Instantiate(shapes[i]);
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

    public void OnShapeInput()
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

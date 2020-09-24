using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

#if UNITY_EDITOR
public class EditorController : MonoBehaviour
{
    [SerializeField]
    float _maxDistance;

    [SerializeField]
    GameObject _grid;

    public Action<Vector3> CallbackCreateClick;
    public Action<GameObject> CallbackDeleteClick;
    public Func<int, bool> CallbackGetCubeLocalPosCheck;

    RaycastHit _hit;
    Vector3 _mousePos;

    bool _isCreateCube = false;
    bool _isDeleteCube = false;
    bool _isMouseDrag = false;

    int _limit_X;
    int _limit_Y;
    int _limit_Z;

    void Start()
    {
        DataManager dataManager = DataManager.GetInstance();
        int x = dataManager.GetGridMaxX();
        int z = dataManager.GetGridMaxZ();
        _limit_X = x / 2;
        _limit_Z = z / 2;

        if (x >= z)
        {
            _limit_Y = z - 1;
        }
        else
        {
            _limit_Y = x - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float cameraZoom = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 50f;
        if (cameraZoom != 0 && transform.localPosition.z <= -1)
        {
            Vector3 cameraPos = transform.localPosition;
            cameraPos.z += cameraZoom;
            if (cameraPos.z >= -1)
            {
                cameraPos.z = -1;
            }
            transform.localPosition = cameraPos;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit, _maxDistance))
            {
                if (_hit.transform.gameObject == null)
                    return;

                if (_isCreateCube)
                {
                    Vector3 vector3 = new Vector3(_hit.transform.position.x + _hit.normal.x,
                                                  _hit.transform.position.y + _hit.normal.y,
                                                  _hit.transform.position.z + _hit.normal.z);

                    //기준점을 기준으로 생성되는 새로운 블럭의 위치가 최대 길이를 넘는지 확인한다.
                    bool isCreateCheck = false;

                    if (vector3.x <= -_limit_X || vector3.x >= _limit_X)
                        return;

                    if (vector3.z <= -_limit_Z || vector3.z >= _limit_Z)
                        return;

                    if (vector3.y == 1)
                    {
                        isCreateCheck = (bool) CallbackGetCubeLocalPosCheck?.Invoke(1);
                        if (!isCreateCheck)
                            return;
                    }
                    else if (vector3.y <= -_limit_Y)
                    {
                        return;
                    }
                                        
                    CallbackCreateClick(vector3);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit, _maxDistance))
            {
                if (_hit.transform.gameObject == null)
                    return;

                if (_isDeleteCube)
                {
                    CallbackDeleteClick(_hit.transform.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            _mousePos = Input.mousePosition;
            _isMouseDrag = true;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            _isMouseDrag = false;
        }

        if (_isMouseDrag)
        {
            Vector3 dir = Input.mousePosition - _mousePos;
            float x = dir.y * Time.deltaTime + _grid.transform.eulerAngles.x;
            float y = dir.x * Time.deltaTime + _grid.transform.eulerAngles.y;

            _grid.transform.eulerAngles = new Vector3(x, y, 0);
        }
    }

    public bool OnCreateCube()
    {
        _isCreateCube = !_isCreateCube;
        return _isCreateCube;
    }

    public bool OnDeleteCube()
    {
        _isDeleteCube = !_isDeleteCube;
        return _isDeleteCube;
    }
}
#endif
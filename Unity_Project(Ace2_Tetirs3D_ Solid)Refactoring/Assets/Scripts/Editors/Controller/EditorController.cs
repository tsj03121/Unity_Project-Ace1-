using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EditorController : MonoBehaviour
{
    [SerializeField]
    float _maxDistance;

    [SerializeField]
    GameObject _grid;

    public Action<Vector3> CallbackCreateClick;
    public Action<GameObject> CallbackDeleteClick;

    RaycastHit _hit;
    Vector3 _mousePos;

    bool _isCreateCube = false;
    bool _isDeleteCube = false;
    bool _isMouseDrag = false;

    // Update is called once per frame
    void Update()
    {
        float cameraForward = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 50f;
        if (cameraForward != 0)
        {
            Vector3 cameraPos = transform.localPosition;
            cameraPos.z += cameraForward;
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

                    CallbackCreateClick(vector3);
                }
                else if (_isDeleteCube)
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

    public void OnClickCubeCreate(Text text)
    {
        _isCreateCube = !_isCreateCube;
        text.text = "CubeCreate \n " + _isCreateCube;
    }

    public void OnClickCubeDelete(Text text)
    {
        _isDeleteCube = !_isDeleteCube;
        text.text = "CubeDelete \n" + _isDeleteCube;
    }
}

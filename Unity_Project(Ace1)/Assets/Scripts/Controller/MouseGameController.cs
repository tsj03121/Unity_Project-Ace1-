using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseGameController : MonoBehaviour, IGameController
{
    public Action<Vector3> FireButtonPressed;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(FireButtonPressed != null)
            {
                FireButtonPressed(GetCurrentClickPoint(Input.mousePosition));
            }
        }
    }

    Vector3 GetCurrentClickPoint(Vector3 mousePosition)
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(mousePosition);
        point.z = 0f;
        return point;
    }
}

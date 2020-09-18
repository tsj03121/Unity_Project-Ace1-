using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextShapeRotation : MonoBehaviour
{
    [SerializeField]
    float _speed = 180;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * _speed);
    }
}

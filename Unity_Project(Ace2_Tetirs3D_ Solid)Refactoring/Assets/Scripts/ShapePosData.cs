using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShapePosData : ScriptableObject
{
    [SerializeField]
    Vector3[] _vector3s;

    public Vector3[] GetVector3s()
    {
        return _vector3s;
    }

    public void SetVector3s(Vector3[] vector3s)
    {
        _vector3s = vector3s;
    }
}

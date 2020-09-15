using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConfirm
{
    //기본 초기 위치 셋팅들
    public Vector3[] PosShapeType_I() { Vector3[] vector3s = { new Vector3(3, 19, 0), new Vector3(4, 19, 0), new Vector3(5, 19, 0), new Vector3(6, 19, 0) }; return vector3s; }
    public Vector3[] PosShapeType_O() { Vector3[] vector3s = { new Vector3(4, 19, 0), new Vector3(5, 19, 0), new Vector3(5, 18, 0), new Vector3(4, 18, 0) }; return vector3s; }
    public Vector3[] PosShapeType_Z() { Vector3[] vector3s = { new Vector3(3, 19, 0), new Vector3(4, 19, 0), new Vector3(4, 18, 0), new Vector3(5, 18, 0) }; return vector3s; }
    public Vector3[] PosShapeType_S() { Vector3[] vector3s = { new Vector3(5, 19, 0), new Vector3(4, 19, 0), new Vector3(4, 18, 0), new Vector3(3, 18, 0) }; return vector3s; }
    public Vector3[] PosShapeType_J() { Vector3[] vector3s = { new Vector3(5, 19, 0), new Vector3(5, 18, 0), new Vector3(4, 18, 0), new Vector3(3, 18, 0) }; return vector3s; }
    public Vector3[] PosShapeType_L() { Vector3[] vector3s = { new Vector3(4, 19, 0), new Vector3(4, 18, 0), new Vector3(5, 18, 0), new Vector3(6, 18, 0) }; return vector3s; }
    public Vector3[] PosShapeType_T() { Vector3[] vector3s = { new Vector3(5, 18, 0), new Vector3(4, 18, 0), new Vector3(5, 19, 0), new Vector3(6, 18, 0) }; return vector3s; }

    public Vector3[] GetShapePos_(ShapeType shapeType)
    {
        switch (shapeType)
        {
            case ShapeType.I:
                {
                    return PosShapeType_I();
                }
            case ShapeType.O:
                {
                    return PosShapeType_O();
                }
            case ShapeType.Z:
                {
                    return PosShapeType_Z();
                }
            case ShapeType.S:
                {
                    return PosShapeType_S();
                }
            case ShapeType.J:
                {
                    return PosShapeType_J();
                }
            case ShapeType.L:
                {
                    return PosShapeType_L();
                }
            case ShapeType.T:
                {
                    return PosShapeType_T();
                }
        }

        return null;
    }

    public Vector3[] GetShapeRot_(ShapeType shapeType, int rotCount)
    {
        switch(shapeType)
        {
            case ShapeType.I:
                {
                    return RotShapeType_I(rotCount);
                }
            case ShapeType.O:
                {
                    return RotShapeType_O(rotCount);
                }
            case ShapeType.Z:
                {
                    return RotShapeType_Z(rotCount);
                }
            case ShapeType.S:
                {
                    return RotShapeType_S(rotCount);
                }
            case ShapeType.J:
                {
                    return RotShapeType_J(rotCount);
                }
            case ShapeType.L:
                {
                    return RotShapeType_L(rotCount);
                }
            case ShapeType.T:
                {
                    return RotShapeType_T(rotCount);
                }
        }

        return null;
    }

    //회전에 따라 현재위치 변경
    public Vector3[] RotShapeType_I(int RotationCount)
    {
        switch (RotationCount)
        {
            case 0:
                {
                    Vector3[] vector3s = { new Vector3(-1, 2, 0), new Vector3(0, 1, 0), new Vector3(1, 0, 0), new Vector3(2, -1, 0) };
                    return vector3s;
                }
            case 1:
                {
                    Vector3[] vector3s = { new Vector3(2, 1, 0), new Vector3(1, 0, 0), new Vector3(0, -1, 0), new Vector3(-1, -2, 0) };
                    return vector3s;
                }
            case 2:
                {
                    Vector3[] vector3s = { new Vector3(1, -2, 0), new Vector3(0, -1, 0), new Vector3(-1, 0, 0), new Vector3(-2, 1, 0) };
                    return vector3s;
                }
            case 3:
                {
                    Vector3[] vector3s = { new Vector3(-2, -1, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 2, 0) };
                    return vector3s;
                }
        }
        return null;
    }

    public Vector3[] RotShapeType_O(int RotationCount)
    {
        switch (RotationCount)
        {
            case 0:
            case 2:
                {
                    Vector3[] vector3s = { new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 0, 0), new Vector3(0, -1, 0) };
                    return vector3s;
                }
            case 1:
            case 3:
                {
                    Vector3[] vector3s = { new Vector3(1, 0, 0), new Vector3(0, -1, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0) };
                    return vector3s;
                }
        }
        return null;
    }

    public Vector3[] RotShapeType_Z(int RotationCount)
    {
        switch (RotationCount)
        {
            case 1:
            case 3:
                {
                    Vector3[] vector3s = { new Vector3(2, 1, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(-1, 0, 0) };
                    return vector3s;
                }
            case 0:
            case 2:
                {
                    Vector3[] vector3s = { new Vector3(-2, -1, 0), new Vector3(-1, 0, 0), new Vector3(0, -1, 0), new Vector3(1, 0, 0) };
                    return vector3s;
                }
            
        }
        return null;
    }

    public Vector3[] RotShapeType_S(int RotationCount)
    {
        switch (RotationCount)
        {
            case 1:
            case 3:
                {
                    Vector3[] vector3s = { new Vector3(-2, 1, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 0, 0) };
                    return vector3s;
                }
            case 0:
            case 2:
                {
                    Vector3[] vector3s = { new Vector3(2, -1, 0), new Vector3(1, 0, 0), new Vector3(0, -1, 0), new Vector3(-1, 0, 0) };
                    return vector3s;
                }

        }
        return null;
    }

    public Vector3[] RotShapeType_J(int RotationCount)
    {
        switch (RotationCount)
        {
            case 0:
                {
                    Vector3[] vector3s = { new Vector3(1, -1, 0), new Vector3(0, -2, 0), new Vector3(-1, -1, 0), new Vector3(-2, 0, 0) };
                    return vector3s;
                }
            case 1:
                {
                    Vector3[] vector3s = { new Vector3(-1, -1, 0), new Vector3(-2, 0, 0), new Vector3(-1, 1, 0), new Vector3(0, 2, 0) };
                    return vector3s;
                }
            case 2:
                {
                    Vector3[] vector3s = { new Vector3(-1, 1, 0), new Vector3(0, 2, 0), new Vector3(1, 1, 0), new Vector3(2, 0, 0) };
                    return vector3s;
                }
            case 3:
                {
                    Vector3[] vector3s = { new Vector3(1, 1, 0), new Vector3(2, 0, 0), new Vector3(1, -1, 0), new Vector3(0, -2, 0) };
                    return vector3s;
                }

        }
        return null;
    }

    public Vector3[] RotShapeType_L(int RotationCount)
    {
        switch (RotationCount)
        {
            case 0:
                {
                    Vector3[] vector3s = { new Vector3(-1, 1, 0), new Vector3(-2, 0, 0), new Vector3(-1, -1, 0), new Vector3(0, -2, 0) };
                    return vector3s;
                }
            case 1:
                {
                    Vector3[] vector3s = { new Vector3(1, 1, 0), new Vector3(0, 2, 0), new Vector3(-1, 1, 0), new Vector3(-2, 0, 0) };
                    return vector3s;
                }
            case 2:
                {
                    Vector3[] vector3s = { new Vector3(1, -1, 0), new Vector3(2, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 2, 0) };
                    return vector3s;
                }
            case 3:
                {
                    Vector3[] vector3s = { new Vector3(-1, -1, 0), new Vector3(0, -2, 0), new Vector3(1, -1, 0), new Vector3(2, 0, 0) };
                    return vector3s;
                }

        }
        return null;
    }

    public Vector3[] RotShapeType_T(int RotationCount)
    {
        switch (RotationCount)
        {
            case 0:
                {
                    Vector3[] vector3s = { new Vector3(0, 0, 0), new Vector3(-1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0) };
                    return vector3s;
                }
            case 1:
                {
                    Vector3[] vector3s = { new Vector3(0, 0, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, -1, 0) };
                    return vector3s;
                }
            case 2:
                {
                    Vector3[] vector3s = { new Vector3(0, 0, 0), new Vector3(1, -1, 0), new Vector3(-1, -1, 0), new Vector3(-1, 1, 0) };
                    return vector3s;
                }
            case 3:
                {
                    Vector3[] vector3s = { new Vector3(0, 0, 0), new Vector3(-1, -1, 0), new Vector3(-1, 1, 0), new Vector3(1, 1, 0) };
                    return vector3s;
                }

        }
        return null;
    }

    public void CreatePos(GameObject[] gameObjects, ShapeType shapeType)
    {
        switch(shapeType)
        {
            case ShapeType.I:
            {
                for(int i = 0; i < gameObjects.Length; ++i)
                {
                    Vector3[] vector3s = PosShapeType_I();
                    gameObjects[i].transform.position = vector3s[i];
                }
                break;
            }
        }
    }

}

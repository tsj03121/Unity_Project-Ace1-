using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConfirm
{

//기본 초기 위치 셋팅들
    public Vector3[] PosShapeType_I()
    {
        Vector3[] vector3s =
        {
            new Vector3(3, 19, 0),
            new Vector3(4, 19, 0),
            new Vector3(5, 19, 0),
            new Vector3(6, 19, 0)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_O()
    {
        Vector3[] vector3s =
        {
            new Vector3(4, 19, 0),
            new Vector3(5, 19, 0),
            new Vector3(5, 18, 0),
            new Vector3(4, 18, 0)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_Z()
    {
        Vector3[] vector3s =
        {
            new Vector3(3, 19, 0),
            new Vector3(4, 19, 0),
            new Vector3(4, 18, 0),
            new Vector3(5, 18, 0)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_S()
    {
        Vector3[] vector3s =
        {
            new Vector3(5, 19, 0),
            new Vector3(4, 19, 0),
            new Vector3(4, 18, 0),
            new Vector3(3, 18, 0)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_J()
    {
        Vector3[] vector3s =
        {
            new Vector3(5, 19, 0),
            new Vector3(5, 18, 0),
            new Vector3(4, 18, 0),
            new Vector3(3, 18, 0)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_L()
    {
        Vector3[] vector3s =
        {
            new Vector3(4, 19, 0),
            new Vector3(4, 18, 0),
            new Vector3(5, 18, 0),
            new Vector3(6, 18, 0)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_T()
    {
        Vector3[] vector3s =
        {
            new Vector3(5, 18, 0),
            new Vector3(4, 18, 0),
            new Vector3(5, 19, 0),
            new Vector3(6, 18, 0)
        };
        return vector3s;
    }

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

    public Vector3[] GetShapeRot_(List<GameObject> currBlocks, ShapeType shapeType, int zRotCount, int yRotCount, int xRotCount, RotType rotType)
    {
        if(rotType == RotType.Z_ROT)
        {
            switch (shapeType)
            {
                case ShapeType.I:
                    {
                        return Z_RotShapeType_I(zRotCount, currBlocks);
                    }
                case ShapeType.O:
                    {
                        return RotShapeType_O(zRotCount);
                    }
                case ShapeType.Z:
                    {
                        return RotShapeType_Z(zRotCount);
                    }
                case ShapeType.S:
                    {
                        return RotShapeType_S(zRotCount);
                    }
                case ShapeType.J:
                    {
                        return RotShapeType_J(zRotCount);
                    }
                case ShapeType.L:
                    {
                        return RotShapeType_L(zRotCount);
                    }
                case ShapeType.T:
                    {
                        return RotShapeType_T(zRotCount);
                    }
            }
        }
        else if(rotType == RotType.X_ROT)
        {
            switch(shapeType)
            {
                case ShapeType.I:
                    {
                        return X_RotShapeType_I(xRotCount, currBlocks);
                    }
            }
        }
        
        return null;
    }




    //회전에 따라 현재위치 변경
    public Vector3[] Z_RotShapeType_I(int RotationCount, List<GameObject> currBlocks)
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

    public Vector3[] X_RotShapeType_I(int RotationCount, List<GameObject> currBlocks)
    {
        Vector3 basePoint = currBlocks[1].transform.position;
        Vector3 point1 = currBlocks[0].transform.position;
        Vector3 point2 = currBlocks[2].transform.position;
        Vector3 point3 = currBlocks[3].transform.position;

        Debug.Log("Dㅕ기됨?");

        switch (RotationCount)
        {
            case 0:
                {
                    Vector3[] vector3s = { new Vector3(-1, 2, 0), new Vector3(0, 1, 0), new Vector3(1, 0, 0), new Vector3(2, -1, 0) };
                    return vector3s;
                }
            case 1:
                {
                    float point1_Z = basePoint.y - point1.y + point1.z;
                    float point2_Z = basePoint.y - point2.y + point2.z;
                    float point3_Z = basePoint.y - point3.y + point3.z;

                    Vector3[] vector3s =
                    {
                        new Vector3(basePoint.x, basePoint.y, point1_Z),
                        new Vector3(0, 0, 0),
                        new Vector3(basePoint.x, basePoint.y, point2_Z),
                        new Vector3(basePoint.x, basePoint.y, point3_Z)
                    };
                    return vector3s;
                }
            case 2:
                {
                    float point1_Y = basePoint.y - point1.y + point1.z;
                    float point2_Y = basePoint.y - point2.y + point2.z;
                    float point3_Y = basePoint.y - point3.y + point3.z;

                    Vector3[] vector3s =
                    {
                        new Vector3(basePoint.x, point1_Y, basePoint.z),
                        new Vector3(0, 0, 0),
                        new Vector3(basePoint.x, point2_Y, basePoint.z),
                        new Vector3(basePoint.x, point3_Y, basePoint.z)
                    };
                    return vector3s;
                }
            case 3:
                {
                    float point1_Z = basePoint.y - point1.y + point1.z;
                    float point2_Z = basePoint.y - point2.y + point2.z;
                    float point3_Z = basePoint.y - point3.y + point3.z;

                    Vector3[] vector3s =
                    {
                        new Vector3(basePoint.x, basePoint.y, point1_Z),
                        new Vector3(0, 0, 0),
                        new Vector3(basePoint.x, basePoint.y, point2_Z),
                        new Vector3(basePoint.x, basePoint.y, point3_Z)
                    };
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

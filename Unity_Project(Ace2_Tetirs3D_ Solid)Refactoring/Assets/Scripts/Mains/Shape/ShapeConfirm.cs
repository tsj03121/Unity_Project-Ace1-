using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeConfirm
{
    public enum ShapeType
    {
        I,
        O,
        Z,
        S,
        J,
        L,
        T,

        NULL
    }

    //기본 초기 위치 셋팅들
    public Vector3[] PosShapeType_I()
    {
        Vector3[] vector3s =
        {
            new Vector3(4, 19, 5),
            new Vector3(3, 19, 5),
            new Vector3(5, 19, 5),
            new Vector3(6, 19, 5)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_O()
    {
        Vector3[] vector3s =
        {
            new Vector3(5, 19, 5),
            new Vector3(4, 19, 5),
            new Vector3(5, 18, 5),
            new Vector3(4, 18, 5)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_Z()
    {
        Vector3[] vector3s =
        {
            new Vector3(4, 19, 5),
            new Vector3(3, 19, 5),
            new Vector3(4, 18, 5),
            new Vector3(5, 18, 5)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_S()
    {
        Vector3[] vector3s =
        {
            new Vector3(4, 19, 5),
            new Vector3(5, 19, 5),
            new Vector3(4, 18, 5),
            new Vector3(3, 18, 5)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_J()
    {
        Vector3[] vector3s =
        {
            new Vector3(4, 18, 5),
            new Vector3(5, 19, 5),
            new Vector3(5, 18, 5),
            new Vector3(3, 18, 5)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_L()
    {
        Vector3[] vector3s =
        {
            new Vector3(5, 18, 5),
            new Vector3(4, 19, 5),
            new Vector3(4, 18, 5),
            new Vector3(6, 18, 5)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_T()
    {
        Vector3[] vector3s =
        {
            new Vector3(5, 18, 5),
            new Vector3(4, 18, 5),
            new Vector3(5, 19, 5),
            new Vector3(6, 18, 5)
        };
        return vector3s;
    }

    public Vector3[] GetShapePos(ShapeType shapeType)
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

    //회전 벡터 반환
    public Vector3[] GetShapeRot(List<GameObject> currBlocks, Shape.RotType rotType)
    {
        Vector3 basePoint = currBlocks[0].transform.position;

        float[] points_X = new float[currBlocks.Count - 1];
        float[] points_Y = new float[currBlocks.Count - 1];
        float[] points_Z = new float[currBlocks.Count - 1];

        if (rotType == Shape.RotType.Z_ROT)
        {
            for (int i = 1; i < currBlocks.Count; ++i)
            {
                points_X[i - 1] = (basePoint.x - currBlocks[i].transform.position.x);
                points_Y[i - 1] = (basePoint.y - currBlocks[i].transform.position.y) * -1;
                points_Z[i - 1] = (basePoint.z - currBlocks[i].transform.position.z) * -1;
            }
            return RotShapeType(basePoint, points_Y, points_X, points_Z);
        }
        else if(rotType == Shape.RotType.X_ROT)
        {
            for (int i = 1; i < currBlocks.Count; ++i)
            {
                points_X[i - 1] = (basePoint.x - currBlocks[i].transform.position.x) * -1;
                points_Y[i - 1] = (basePoint.y - currBlocks[i].transform.position.y);
                points_Z[i - 1] = (basePoint.z - currBlocks[i].transform.position.z) * -1;
            }
            return RotShapeType(basePoint, points_X, points_Z, points_Y);
        }
        else if (rotType == Shape.RotType.Y_ROT)
        {
            for (int i = 1; i < currBlocks.Count; ++i)
            {
                points_X[i - 1] = (basePoint.x - currBlocks[i].transform.position.x);
                points_Y[i - 1] = (basePoint.y - currBlocks[i].transform.position.y) * -1;
                points_Z[i - 1] = (basePoint.z - currBlocks[i].transform.position.z) * -1;
            }
            return RotShapeType(basePoint, points_Z, points_Y, points_X);
        }

        return null;
    }

    public Vector3[] GetShapeRotReverse(List<GameObject> currBlocks, Shape.RotType rotType)
    {
        Vector3 basePoint = currBlocks[0].transform.position;

        float[] points_X = new float[currBlocks.Count - 1];
        float[] points_Y = new float[currBlocks.Count - 1];
        float[] points_Z = new float[currBlocks.Count - 1];

        if (rotType == Shape.RotType.Z_ROT)
        {
            for (int i = 1; i < currBlocks.Count; i++)
            {
                points_X[i - 1] = (basePoint.x - currBlocks[i].transform.position.x) * -1;
                points_Y[i - 1] = (basePoint.y - currBlocks[i].transform.position.y);
                points_Z[i - 1] = (basePoint.z - currBlocks[i].transform.position.z) * -1;
            }
            return RotShapeType(basePoint, points_Y, points_X, points_Z);
        }
        else if (rotType == Shape.RotType.X_ROT)
        {
            for (int i = 1; i < currBlocks.Count; i++)
            {
                points_X[i - 1] = (basePoint.x - currBlocks[i].transform.position.x) * -1;
                points_Y[i - 1] = (basePoint.y - currBlocks[i].transform.position.y) * -1;
                points_Z[i - 1] = (basePoint.z - currBlocks[i].transform.position.z);
            }
            return RotShapeType(basePoint, points_X, points_Z, points_Y);
        }
        else if (rotType == Shape.RotType.Y_ROT)
        {
            for (int i = 1; i < currBlocks.Count; i++)
            {
                points_X[i - 1] = (basePoint.x - currBlocks[i].transform.position.x) * -1;
                points_Y[i - 1] = (basePoint.y - currBlocks[i].transform.position.y) * -1;
                points_Z[i - 1] = (basePoint.z - currBlocks[i].transform.position.z);

            }
            return RotShapeType(basePoint, points_Z, points_Y, points_X);
        }
        return null;
    }

    public Vector3[] RotShapeType(Vector3 basePoint, float[] points_X, float[] points_Y, float[] points_Z)
    {
        Vector3[] vector3s =
        {
            new Vector3(0, 0, 0) + basePoint,
            new Vector3(points_X[0], points_Y[0], points_Z[0]) + basePoint,
            new Vector3(points_X[1], points_Y[1], points_Z[1]) + basePoint,
            new Vector3(points_X[2], points_Y[2], points_Z[2]) + basePoint
        };
        return vector3s;
    }

    //처음 생성 위지
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

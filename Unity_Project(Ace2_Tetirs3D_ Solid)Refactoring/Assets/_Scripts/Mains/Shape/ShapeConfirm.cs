using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeConfirm
{
    ShapePosData[] _shapeTypes;

    public ShapePosData[] GetShapeTypes() { return _shapeTypes; }

    public Vector3 GetBasicCreatePos() { return new Vector3(5, 19, 5); }

    public ShapeConfirm()
    {
        _shapeTypes = Resources.LoadAll<ShapePosData>("");
    }

    public Vector3[] GetShapePos(int shapeIndex)
    {
        return _shapeTypes[shapeIndex].GetVector3s();
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
        Vector3[] vector3s = new Vector3[points_X.Length + 1];

        vector3s[0] = basePoint;
        for (int i = 0; i < points_X.Length; i++)
        {
            vector3s[i + 1] = new Vector3(points_X[i], points_Y[i], points_Z[i]) + basePoint;
        }
        return vector3s;
    }
}

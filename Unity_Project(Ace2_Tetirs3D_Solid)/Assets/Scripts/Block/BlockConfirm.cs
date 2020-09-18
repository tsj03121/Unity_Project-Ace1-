using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConfirm
{
    int zRotCount_;
    int yRotCount_;
    int xRotCount_;

    List<GameObject> currBlocks_;

//기본 초기 위치 셋팅들
    public Vector3[] PosShapeType_I()
    {
        Vector3[] vector3s =
        {
            new Vector3(3, 19, 5),
            new Vector3(4, 19, 5),
            new Vector3(5, 19, 5),
            new Vector3(6, 19, 5)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_O()
    {
        Vector3[] vector3s =
        {
            new Vector3(4, 19, 5),
            new Vector3(5, 19, 5),
            new Vector3(5, 18, 5),
            new Vector3(4, 18, 5)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_Z()
    {
        Vector3[] vector3s =
        {
            new Vector3(3, 19, 5),
            new Vector3(4, 19, 5),
            new Vector3(4, 18, 5),
            new Vector3(5, 18, 5)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_S()
    {
        Vector3[] vector3s =
        {
            new Vector3(5, 19, 5),
            new Vector3(4, 19, 5),
            new Vector3(4, 18, 5),
            new Vector3(3, 18, 5)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_J()
    {
        Vector3[] vector3s =
        {
            new Vector3(5, 19, 5),
            new Vector3(5, 18, 5),
            new Vector3(4, 18, 5),
            new Vector3(3, 18, 5)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_L()
    {
        Vector3[] vector3s =
        {
            new Vector3(4, 19, 5),
            new Vector3(4, 18, 5),
            new Vector3(5, 18, 5),
            new Vector3(6, 18, 5)
        };
        return vector3s;
    }

    public Vector3[] PosShapeType_T()
    {
        Vector3[] vector3s =
        {
            new Vector3(4, 18, 5),
            new Vector3(5, 18, 5),
            new Vector3(5, 19, 5),
            new Vector3(6, 18, 5)
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
        currBlocks_ = currBlocks;
        zRotCount_ = zRotCount;
        xRotCount_ = xRotCount;
        yRotCount_ = yRotCount;


        Vector3 basePoint = currBlocks[1].transform.position;
        Vector3[] points =
            {
                basePoint - currBlocks[0].transform.position,
                basePoint - currBlocks[2].transform.position,
                basePoint - currBlocks[3].transform.position
            };
        
        if (rotType == RotType.Z_ROT)
        {
            switch (shapeType)
            {
                case ShapeType.I:
                    {
                        return Z_RotShapeType_I(zRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.O:
                    {
                        return Z_RotShapeType_O(zRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.Z:
                    {
                        return Z_RotShapeType_Z(zRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.S:
                    {
                        return Z_RotShapeType_S(zRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.J:
                    {
                        return Z_RotShapeType_J(zRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.L:
                    {
                        return Z_RotShapeType_L(zRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.T:
                    {
                        return Z_RotShapeType_T(zRotCount, currBlocks, basePoint, points);
                    }
            }
        }
        else if(rotType == RotType.X_ROT)
        {
            switch(shapeType)
            {
                case ShapeType.I:
                    {
                        return X_RotShapeType_I(xRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.O:
                    {
                        return X_RotShapeType_O(xRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.Z:
                    {
                        return X_RotShapeType_Z(xRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.S:
                    {
                        return X_RotShapeType_S(xRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.J:
                    {
                        return X_RotShapeType_J(xRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.L:
                    {
                        return X_RotShapeType_L(xRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.T:
                    {
                        return X_RotShapeType_T(xRotCount, currBlocks, basePoint, points);
                    }
            }
        }
        else if(rotType == RotType.Y_ROT)
        {
            switch(shapeType)
            {
                case ShapeType.I:
                    {
                        return Y_RotShapeType_I(yRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.O:
                    {
                        return Y_RotShapeType_O(yRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.Z:
                    {
                        return Y_RotShapeType_Z(yRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.S:
                    {
                        return Y_RotShapeType_S(yRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.J:
                    {
                        return Y_RotShapeType_J(xRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.L:
                    {
                        return Y_RotShapeType_L(xRotCount, currBlocks, basePoint, points);
                    }
                case ShapeType.T:
                    {
                        return Y_RotShapeType_T(xRotCount, currBlocks, basePoint, points);
                    }
            }
        }
        
        return null;
    }

    //회전에 따라 현재위치 변경 I도형
    public Vector3[] Z_RotShapeType_I(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint , Vector3[] points)
    {
        switch (RotationCount)
        {
            case 0:
            case 2:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].y, points[0].y, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(points[1].y, points[1].y, 0),
                        new Vector3(points[2].y, points[2].y, 0)
                    };
                    return vector3s;
                }
            case 1:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(-points[0].x, points[0].x, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(-points[1].x, points[1].x, 0),
                        new Vector3(-points[2].x, points[2].x, 0)
                    };
                    return vector3s;
                }
        }
        return null;
    }

    public Vector3[] X_RotShapeType_I(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        switch (RotationCount)
        {
            case 0:
            case 2:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(0, points[0].y, points[0].y),
                        new Vector3(0, 0, 0),
                        new Vector3(0, points[1].y, points[1].y),
                        new Vector3(0, points[2].y, points[2].y)
                    };
                    return vector3s;
                }
            case 1:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(0, points[0].z, points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(0, points[1].z, points[1].z),
                        new Vector3(0, points[2].z, points[2].z)
                    };
                    return vector3s;
                }
        }
        return null;
    }

    public Vector3[] Y_RotShapeType_I(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        switch (RotationCount)
        {
            case 0:
            case 2:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].x, 0, points[0].x),
                        new Vector3(0, 0, 0),
                        new Vector3(points[1].x, 0, points[1].x),
                        new Vector3(points[2].x, 0, points[2].x)
                    };
                    return vector3s;
                }
            case 1:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].z, 0, points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(points[1].z, 0, points[1].z),
                        new Vector3(points[2].z, 0, points[2].z)
                    };
                    return vector3s;
                }
        }
        return null;
    }



    public Vector3[] Z_RotShapeType_O(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        if (points[0].z == 0 && points[1].z == 0 && points[2].z == 0)
        {
            Vector3[] vector3s =
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 0)
            };
            return vector3s;
        }

        switch (RotationCount)
        {
            case 0:
            case 2:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(-points[0].y + points[0].x, points[0].y + points[0].x, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(-points[1].y + points[1].x, points[1].y + points[1].x, 0),
                        new Vector3(-points[2].y + points[2].x, points[2].y + points[2].x, 0)
                    };
                    return vector3s;
                }
            case 1:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].x + points[0].y, points[0].x + points[0].y, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(points[1].x + points[1].y, points[1].x + points[1].y, 0),
                        new Vector3(points[2].x + points[2].y, points[2].x + points[2].y, 0)
                    };
                    return vector3s;
                }
        }
        return null;
    }

    public Vector3[] X_RotShapeType_O(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        if (points[0].z == 0 && points[1].z == 0 && points[2].z == 0)
        {
            Vector3[] vector3s =
            {
                            new Vector3(0, 0, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(0, 0, 0)
                        };
            return vector3s;
        }

        switch (RotationCount)
        {
            case 0:
            case 2:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(0, 0, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(0, -(points[1].y + points[1].z), points[1].y + points[1].z),
                        new Vector3(0, -(points[2].y + points[2].z), points[2].y + points[2].z)
                    };
                    return vector3s;
                }
            case 1:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(0, 0, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(0, points[1].y + points[1].z, points[1].y + points[1].z),
                        new Vector3(0, points[2].y + points[2].z, points[2].y + points[2].z)
                    };
                    return vector3s;
                }
        }
        return null;
    }

    public Vector3[] Y_RotShapeType_O(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        if (points[0].z == 0 && points[1].z == 0 && points[2].z == 0)
        {
            Vector3[] vector3s =
            {
                            new Vector3(0, 0, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(0, 0, 0)
                        };
            return vector3s;
        }

        switch (RotationCount)
        {
            case 0:
            case 2:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(-(points[0].x + points[0].z), 0, points[0].x + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(-(points[1].x + points[1].z), 0, points[1].x + points[1].z),
                        new Vector3(-(points[2].x + points[2].z), 0, points[2].x + points[2].z)
                    };
                    return vector3s; 
                }
            case 1:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].x + points[0].z, 0, points[0].x + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(points[1].x + points[1].z, 0, points[1].x + points[1].z),
                        new Vector3(points[2].x + points[2].z, 0, points[2].x + points[2].z)
                    };
                    return vector3s;
                }
        }
        return null;
    }



    public Vector3[] Z_RotShapeType_Z(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        if(points[0].z != 0 && points[1].z != 0 && points[2].z != 0)
        {
            switch (RotationCount)
            {
                case 0:
                case 2:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(-points[0].y, points[0].y, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(-points[1].y, points[1].y, 0),
                            new Vector3(-points[2].y, points[2].y, 0)
                        };
                        return vector3s;
                    }
                case 3:
                case 1:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(points[0].x, points[0].x, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(points[1].x, points[1].x, 0),
                            new Vector3(points[2].x, points[2].x, 0)
                        };
                        return vector3s;
                    }
            }
        }
        else
        {
            switch (RotationCount)
            {
                case 0:
                case 2:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(points[0].y, points[0].y, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(points[1].x, -points[1].x, 0),
                            new Vector3(points[2].x + points[2].y, 0, 0)
                        };
                        return vector3s;
                    }
                case 3:
                case 1:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(points[0].x, points[0].x, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(-points[1].y, points[1].y, 0),
                            new Vector3(points[2].x + -points[2].y, 0, 0)
                        };
                        return vector3s;
                    }
            }

        }
        return null;
    }

    public Vector3[] X_RotShapeType_Z(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        switch (RotationCount)
        {
            case 0:
            case 2:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(0, -(points[0].z + points[0].y), points[0].z + points[0].y),
                        new Vector3(0, 0, 0),
                        new Vector3(0, -(points[1].z + points[1].y), points[1].z + points[1].y),
                        new Vector3(0, -(points[2].z + points[2].y), points[2].z + points[2].y)
                    };
                    return vector3s;
                }
            case 1:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(0, points[0].z + points[0].y, points[0].z + points[0].y),
                        new Vector3(0, 0, 0),
                        new Vector3(0, points[1].z + points[1].y, points[1].z + points[1].y),
                        new Vector3(0, points[2].z + points[2].y, points[2].z + points[2].y)
                    };
                    return vector3s;
                }
        }
        return null;
    }

    public Vector3[] Y_RotShapeType_Z(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        switch (RotationCount)
        {
            case 0:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].x + -points[0].z, 0, points[0].x + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(points[1].x + points[1].z, 0, points[1].x + -points[1].z),
                        new Vector3(points[2].x + -points[2].z, 0, points[2].x + points[2].z)
                    };
                    return vector3s;
                }
            case 1:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].x + -points[0].z, 0, points[0].x + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(points[1].x + points[1].z, 0, points[1].x + -points[1].z),
                        new Vector3(points[2].x + -points[2].z, 0, points[2].x + -points[2].z)
                    };
                    return vector3s;
                }
            case 2:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].x + -points[0].z, 0, points[0].x + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(points[1].x + points[1].z, 0, points[1].x + -points[1].z),
                        new Vector3(points[2].x + -points[2].z, 0, points[2].x + points[2].z)
                    };
                    return vector3s;
                }
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].x + -points[0].z, 0, points[0].x + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(points[1].x + points[1].z, 0, points[1].x + -points[1].z),
                        new Vector3(points[2].x + -points[2].z, 0, points[2].x + points[2].z)
                    };
                    return vector3s;
                }
        }
        return null;
    }



    public Vector3[] Z_RotShapeType_S(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        if (points[0].z != 0 && points[1].z != 0 && points[2].z != 0)
        {
            switch (RotationCount)
            {
                case 0:
                case 2:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(points[0].y, points[0].y, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(points[1].y, points[1].y, 0),
                            new Vector3(points[2].y, points[2].y, 0)
                        };
                        return vector3s;
                    }
                case 1:
                case 3:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(points[0].x, points[0].x, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(points[1].x, points[1].x, 0),
                            new Vector3(points[2].x, points[2].x, 0)
                        };
                        return vector3s;
                    }
            }
        }
        else
        {
            switch (RotationCount)
            {
                case 0:
                case 2:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(points[0].y, points[0].y, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(points[1].x, -points[1].x, 0),
                            new Vector3(0, -points[2].x + points[2].y, 0)
                        };
                        return vector3s;
                    }
                case 1:
                case 3:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(points[0].x, points[0].x, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(-points[1].y, points[1].y, 0),
                            new Vector3(0, points[2].x + points[2].y, 0)
                        };
                        return vector3s;
                    }
            }

        }
        return null;
    }

    public Vector3[] X_RotShapeType_S(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        if (points[0].x == 0 && points[1].x == 0 && points[2].x == 0)
        {
            switch (RotationCount)
            {
                case 0:
                case 2:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(0, points[0].y, points[0].y),
                            new Vector3(0, 0, 0),
                            new Vector3(0, -points[1].z, -points[1].z),
                            new Vector3(0, points[2].z + points[2].y, 0)
                        };
                        return vector3s;
                    }
                case 1:
                case 3:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(0, -points[0].z, points[0].z),
                            new Vector3(0, 0, 0),
                            new Vector3(0, points[1].y, points[1].y),
                            new Vector3(0, -points[2].z + points[2].y, 0)
                        };
                        return vector3s;
                    }
            }
        }

        switch (RotationCount)
        {
            case 0:
            case 2:
                {

                    Vector3[] vector3s =
                    {
                        new Vector3(0, 0, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(0, -(points[1].z + points[1].y), points[1].z + points[1].y),
                        new Vector3(0, -(points[2].z + points[2].y), points[2].z + points[2].y)
                    };
                    return vector3s;
                }
            case 1:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(0, 0, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(0, points[1].z + points[1].y, points[1].z + points[1].y),
                        new Vector3(0, points[2].z + points[2].y, points[2].z + points[2].y)
                    };
                    return vector3s;
                }
        }
        return null;
    }

    public Vector3[] Y_RotShapeType_S(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        switch (RotationCount)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].x + -points[0].z, 0, points[0].x + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(points[2].x + -points[2].z, 0, points[2].x + points[2].z)
                    };
                    return vector3s;
                }
        }
        return null;
    }



    public Vector3[] Z_RotShapeType_J(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        switch (RotationCount)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(-points[0].y + points[0].x, points[0].y + points[0].x, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(-points[1].y + points[1].x, points[1].y + points[1].x, 0),
                        new Vector3(-points[2].y + points[2].x, points[2].y + points[2].x, 0)
                    };
                    return vector3s;
                }
        }
        return null;
    }

    public Vector3[] X_RotShapeType_J(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        switch (RotationCount)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(0, points[0].y + -points[0].z, points[0].y + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(0, points[1].y + -points[1].z, points[1].y + points[1].z),
                        new Vector3(0, points[2].y + -points[2].z, points[2].y + points[2].z)
                    };
                    return vector3s;
                }
        }
        return null;
    }

    public Vector3[] Y_RotShapeType_J(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        switch (RotationCount)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].x + -points[0].z, 0, points[0].x + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(points[1].x + -points[1].z, 0, points[1].x + points[1].z),
                        new Vector3(points[2].x + -points[2].z, 0, points[2].x + points[2].z)
                    };
                    return vector3s;
                }
        }
        return null;
    }



    public Vector3[] Z_RotShapeType_L(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        if (points[0].z != 0 && points[1].z != 0 && points[2].z != 0)
        {
            switch (RotationCount)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(points[0].x + -points[0].y, points[0].x + points[0].y, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(points[1].x + -points[1].y, points[1].x + points[1].y, 0),
                            new Vector3(points[2].x + -points[2].y, points[2].x + points[2].y, 0)
                        };
                        return vector3s;
                    }
            }
        }
        else
        {
            switch (RotationCount)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    {
                        Vector3[] vector3s =
                        {
                                new Vector3(-points[0].y + points[0].x, points[0].y + points[0].x, 0),
                                new Vector3(0, 0, 0),
                                new Vector3(-points[1].y + points[1].x, points[1].y + points[1].x, 0),
                                new Vector3(-points[2].y + points[2].x, points[2].y + points[2].x, 0)
                            };
                        return vector3s;
                    }
            }
        }
        return null;
    }

    public Vector3[] X_RotShapeType_L(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        if (points[0].x != 0 && points[1].x != 0 && points[2].x != 0)
        {
            switch (RotationCount)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(0, points[0].y + -points[0].z, points[0].y + points[0].z),
                            new Vector3(0, 0, 0),
                            new Vector3(0, points[1].y + -points[1].z, points[1].y + points[1].z),
                            new Vector3(0, points[2].y + -points[2].z, points[2].y + points[2].z)
                        };
                        return vector3s;
                    }
            }
        }

        switch (RotationCount)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(0, points[0].y + -points[0].z, points[0].y + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(0, points[1].y + -points[1].z, points[1].y + points[1].z),
                        new Vector3(0, points[2].y + -points[2].z, points[2].y + points[2].z)
                    };
                    return vector3s;
                }
        }
        return null;
    }

    public Vector3[] Y_RotShapeType_L(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        switch (RotationCount)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].x + -points[0].z, 0, points[0].x + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(points[1].x + -points[1].z, 0, points[1].x + points[1].z),
                        new Vector3(points[2].x + -points[2].z, 0, points[2].x + points[2].z)
                    };
                    return vector3s;
                }
        }
        return null;
    }



    public Vector3[] Z_RotShapeType_T(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        if (points[0].z != 0 && points[1].z != 0 && points[2].z != 0)
        {
            switch (RotationCount)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(points[0].x + -points[0].y, points[0].x + points[0].y, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(points[1].x + -points[1].y, points[1].x + points[1].y, 0),
                            new Vector3(points[2].x + -points[2].y, points[2].x + points[2].y, 0)
                        };
                        return vector3s;
                    }
            }
        }
        else
        {
            switch (RotationCount)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(-points[0].y + points[0].x, points[0].y + points[0].x, 0),
                            new Vector3(0, 0, 0),
                            new Vector3(-points[1].y + points[1].x, points[1].y + points[1].x, 0),
                            new Vector3(-points[2].y + points[2].x, points[2].y + points[2].x, 0)
                        };
                        return vector3s;
                    }
            }
        }
        return null;
    }

    public Vector3[] X_RotShapeType_T(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        if (points[0].x != 0 && points[1].x != 0 && points[2].x != 0)
        {
            switch (RotationCount)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    {
                        Vector3[] vector3s =
                        {
                            new Vector3(0, points[0].y + -points[0].z, points[0].y + points[0].z),
                            new Vector3(0, 0, 0),
                            new Vector3(0, points[1].y + -points[1].z, points[1].y + points[1].z),
                            new Vector3(0, points[2].y + -points[2].z, points[2].y + points[2].z)
                        };
                        return vector3s;
                    }
            }
        }

        switch (RotationCount)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(0, points[0].y + -points[0].z, points[0].y + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(0, points[1].y + -points[1].z, points[1].y + points[1].z),
                        new Vector3(0, points[2].y + -points[2].z, points[2].y + points[2].z)
                    };
                    return vector3s;
                }
        }
        return null;
    }

    public Vector3[] Y_RotShapeType_T(int RotationCount, List<GameObject> currBlocks, Vector3 basePoint, Vector3[] points)
    {
        switch (RotationCount)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                {
                    Vector3[] vector3s =
                    {
                        new Vector3(points[0].x + -points[0].z, 0, points[0].x + points[0].z),
                        new Vector3(0, 0, 0),
                        new Vector3(points[1].x + -points[1].z, 0, points[1].x + points[1].z),
                        new Vector3(points[2].x + -points[2].z, 0, points[2].x + points[2].z)
                    };
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    ShapeData _shapeData;

    [SerializeField]
    List<Material> _shapesMaterials = new List<Material>();

    [SerializeField]
    List<ShapePosData> _shapePosDatas = new List<ShapePosData>();

    [SerializeField]
    List<GameObject> _nextShapeView = new List<GameObject>();

    static DataManager _dataManager;

    public static DataManager GetInstance()
    {
        if (_dataManager == null)
        {
            Debug.Log("1");
            return null;
        }
        return _dataManager;
    }

    int _grid_MaxX;
    int _grid_MaxY;
    int _grid_MaxZ;
    Vector3 _basicPos;

    public int GetGridMaxX() { return _grid_MaxX; }
    public int GetGridMaxY() { return _grid_MaxY; }
    public int GetGridMaxZ() { return _grid_MaxZ; }
    public Vector3 GetBasicPos() { return _basicPos; }
    public ShapeData GetShapeData() { return _shapeData; }

    // Start is called before the first frame update
    void Awake()
    {
        if (_dataManager != null)
        {
            Destroy(gameObject);
            return;
        }

        _dataManager = this;
        _shapeData = new ShapeData();
        _shapesMaterials = _shapeData.GetShapesMaterials();
        _shapePosDatas = _shapeData.GetShapesPosDatas();
        _nextShapeView = _shapeData.GetNextShapesView();

        _grid_MaxX = 10;
        _grid_MaxY = 20;
        _grid_MaxZ = 10;
        _basicPos = new Vector3(5, 19, 5);
        DontDestroyOnLoad(gameObject);
    }

}

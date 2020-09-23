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
            return null;
        }
        return _dataManager;
    }

    public ShapeData GetShapeData() { return _shapeData; }

    // Start is called before the first frame update
    void Awake()
    {
        if (_dataManager == null)
        {
            _dataManager = this;
            _shapeData = new ShapeData();
            _shapesMaterials = _shapeData.GetShapesMaterials();
            _shapePosDatas = _shapeData.GetShapesPosDatas();
            _nextShapeView = _shapeData.GetNextShapesView();
        }
        else if (_dataManager != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeData
{
    List<Material> _shapesMaterials = new List<Material>();
    List<ShapePosData> _shapePosDatas = new List<ShapePosData>();
    List<GameObject> _nextShapeView = new List<GameObject>();

    public List<Material> GetShapesMaterials() { return _shapesMaterials; }
    public void AddShapeMaterial(Material material) { _shapesMaterials.Add(material); }
    public void UpdateShapeMaterial(int index, Material material) { _shapesMaterials[index] = material; }

    public List<ShapePosData> GetShapesPosDatas() { return _shapePosDatas; }
    public void AddShapePosData(ShapePosData shapePosData) { _shapePosDatas.Add(shapePosData); }
    public void UpdateShapePosData(int index, ShapePosData shapePosData) { _shapePosDatas[index] = shapePosData; }

    public List<GameObject> GetNextShapesView() { return _nextShapeView; }
    public void AddNextShapesView(GameObject gameObject) { _nextShapeView.Add(gameObject); }

    public ShapeData()
    {
        Material[] materials = Resources.LoadAll<Material>("ShapeMaterials");
        ShapePosData[] shapePosDatas = Resources.LoadAll<ShapePosData>("ShapePosDatas");
        NextShapeRotation[] nextShapeView = Resources.LoadAll<NextShapeRotation>("Shapes");

        for (int i = 0; i < materials.Length; i++)
        {
            _shapesMaterials.Add(materials[i]);
            _shapePosDatas.Add(shapePosDatas[i]);
            _nextShapeView.Add(nextShapeView[i].gameObject);
        }
    }
}

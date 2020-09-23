using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditCube : MonoBehaviour
{
    [SerializeField]
    Material _material;

    [SerializeField]
    GameObject _cube;

    [SerializeField]
    GameObject _center;

    [SerializeField]
    GameObject _parentCube;

    [SerializeField]
    List<GameObject> _newBlock = new List<GameObject>();

    DataManager _dataManager;

    string _cubeName = "Temp";

    void AllPosMove(Vector3 vector3)
    {
        _parentCube.transform.position += vector3;
        _center.transform.position = _parentCube.transform.position;
    }

    void ShapesMaterialSetting(Material material)
    {
        for (int i = 0; i < _newBlock.Count; i++)
        {
            _newBlock[i].GetComponent<MeshRenderer>().material = material;
        }
    }

    void ShapesScaleSetting()
    {
        int x = 0;
        int y = 0;

        for (int i = 0; i < _newBlock.Count; i++)
        {
            if (x < Mathf.Abs((int)_newBlock[i].transform.localPosition.x))
            {
                x = Mathf.Abs((int)_newBlock[i].transform.localPosition.x);
            }

            if (y < Mathf.Abs((int)_newBlock[i].transform.localPosition.y))
            {
                y = Mathf.Abs((int)_newBlock[i].transform.localPosition.y);
            }                
        }

        if (x > y)
        {
            SetScaleChange(x);
        }
        else
        {
            SetScaleChange(y);
        }
    }

    void GotoLobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }

    void SetScaleChange(int value)
    {
        float scaleSize = 0.9f;
        if (value != 0)
        {
            scaleSize = 0.6f  - (0.1f * value);
        }
        _parentCube.transform.localScale = Vector3.one * scaleSize;
    }

    public void Init(DataManager dataManager)
    {
        _dataManager = dataManager;
        _material.color = Color.white;
    }

    public void OnCreateClick(Vector3 vector3)
    {
        GameObject newCube = Instantiate(_cube);
        newCube.transform.parent = _parentCube.transform;
        newCube.transform.position = vector3;
        newCube.GetComponent<MeshRenderer>().material = _material;

        _newBlock.Add(newCube);

        if (newCube.transform.position.y > 0)
        {
            AllPosMove(Vector3.down);
        }
    }

    public void OnDeleteClick(GameObject gameObject)
    {
        if (gameObject == _parentCube)
            return;

        int index = _newBlock.IndexOf(gameObject);
        _newBlock.RemoveAt(index);
        Destroy(gameObject);

        int height = 0;
        for (int i = 0; i < _newBlock.Count; i++)
        {
            float y = _newBlock[i].transform.localPosition.y;

            if (height < y)
            {
                height = (int) y;
            }
        }

        if (_parentCube.transform.position.y + height == 0)
            return;

        height = (int)_parentCube.transform.position.y + height;

        AllPosMove(Vector3.up * (height * -1));
    }

    public void OnSliderChange(Slider slider)
    {
        Color color = _material.color;

        if (slider.name.ToString().Equals("R"))
        {
            color.r = slider.value;
        }
        else if (slider.name.ToString().Equals("G"))
        {
            color.g = slider.value;
        }
        else if (slider.name.ToString().Equals("B"))
        {
            color.b = slider.value;
        }

        _material.color = color;
    }

    public void OnSaveNameChange(string text)
    {
        _cubeName = text;
    }

    public bool OnGetCubeLocalPosCheck(int value)
    {
        for (int i = 0; i < _newBlock.Count; i++)
        {
            if (_newBlock[i].transform.localPosition.y >= 4)
                return false;
        }

        return true;
    }

    public void OnSave()
    {
        _newBlock.Add(_parentCube);

        ShapePosData shapePosData = ScriptableObject.CreateInstance<ShapePosData>();
        Vector3[] vector3s = new Vector3[_newBlock.Count];

        for (int i = 0; i < _newBlock.Count; i++)
        {
            vector3s[i] = _newBlock[i].transform.position;
        }
        shapePosData.SetVector3s(vector3s);

        Material material = new Material(Shader.Find("Standard"));
        material.color = _material.color;     

        ShapesMaterialSetting(material);

        _newBlock.Remove(_parentCube);
        ShapesScaleSetting();

        _parentCube.transform.position = new Vector3(0.64f, 0f, 0.88f);
        _parentCube.AddComponent<NextShapeRotation>();

        if (_cubeName.Equals(""))
            _cubeName = "Temp";

        AssetDatabase.CreateAsset(material, "Assets/Resources/ShapeMaterials/" + _cubeName + ".mat");
        AssetDatabase.CreateAsset(shapePosData, "Assets/Resources/ShapePosDatas/" + _cubeName + ".asset");
        PrefabUtility.SaveAsPrefabAssetAndConnect(_parentCube, "Assets/Resources/Shapes/" + _cubeName + ".prefab" , InteractionMode.UserAction);
        NextShapeRotation nextShapeView = Resources.Load<NextShapeRotation>("Shapes/" + _cubeName);

        ShapeData shapeData = _dataManager.GetShapeData();
        int checkIndex = shapeData.GetNextShapesView().IndexOf(nextShapeView.gameObject);
        if (checkIndex == -1)
        {
            shapeData.AddShapeMaterial(material);
            shapeData.AddShapePosData(shapePosData);
            shapeData.AddNextShapesView(nextShapeView.gameObject);
        }
        else
        {
            shapeData.UpdateShapeMaterial(checkIndex, material);
            shapeData.UpdateShapePosData(checkIndex, shapePosData);
        }

        Invoke("GotoLobbyScene", 0.1f);
    }

    public void OnLoadListSelect(ShapePosData shapePosData, Material material)
    {
        _material.color = material.color;

        Vector3[] vector3s = shapePosData.GetVector3s();

        _parentCube.transform.position += vector3s[vector3s.Length - 1];

        for (int i = 0; i < vector3s.Length - 1; i++)
        {
            OnCreateClick(vector3s[i]);
        }
    }

    public void OnReset()
    {
        for (int i = _newBlock.Count - 1; i >= 0; i--)
        {
            OnDeleteClick(_newBlock[i]);
        }
        _material.color = Color.white;
    }
}

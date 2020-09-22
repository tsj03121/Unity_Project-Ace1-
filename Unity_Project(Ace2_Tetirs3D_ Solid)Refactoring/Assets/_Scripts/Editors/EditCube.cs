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
    GameObject _parentCube;

    [SerializeField]
    List<GameObject> _newShapePos = new List<GameObject>();

    string _cubeName = "Temp";

    void Start()
    {
        _material.color = Color.white;
    }

    void AllPosMove(Vector3 vector3)
    {
        _parentCube.transform.position += vector3;
    }

    void ShapesMaterialSetting(Material material)
    {
        for (int i = 0; i < _newShapePos.Count; i++)
        {
            _newShapePos[i].GetComponent<MeshRenderer>().material = material;
        }
    }

    //기준점을 기준으로 크기가 얼마나 큰지 측정 후 스케일 조정
    void ShapesScaleSetting()
    {
        int x = 0;
        int y = 0;

        for (int i = 0; i < _newShapePos.Count; i++)
        {
            if (x < Mathf.Abs((int) _newShapePos[i].transform.localPosition.x))
            {
                x = Mathf.Abs((int)_newShapePos[i].transform.localPosition.x);
            }

            if (y < Mathf.Abs((int) _newShapePos[i].transform.localPosition.y))
            {
                y = Mathf.Abs((int)_newShapePos[i].transform.localPosition.y);
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

    void SetScaleChange(int value)
    {
        switch (value)
        {
            case 1:
                {
                    _parentCube.transform.localScale = Vector3.one * 0.5f;
                    break;
                }
            case 2:
                {
                    _parentCube.transform.localScale = Vector3.one * 0.3f;
                    break;
                }
            case 3:
                {
                    _parentCube.transform.localScale = Vector3.one * 0.2f;
                    break;
                }
            case 4:
                {
                    _parentCube.transform.localScale = Vector3.one * 0.15f;
                    break;
                }
            default:
                {
                    _parentCube.transform.localScale = Vector3.one * 1.5f;
                    break;
                }
        }
    }

    public void OnCreateClick(Vector3 vector3)
    {
        GameObject newCube = Instantiate(_cube);
        newCube.transform.parent = _parentCube.transform;
        newCube.transform.position = vector3;
        newCube.GetComponent<MeshRenderer>().material = _material;

        _newShapePos.Add(newCube);

        if (newCube.transform.position.y > 0)
        {
            AllPosMove(Vector3.down);
        }
    }

    public void OnDeleteClick(GameObject gameObject)
    {
        if (gameObject == _parentCube)
            return;

        int index = _newShapePos.IndexOf(gameObject);
        _newShapePos.RemoveAt(index);
        Destroy(gameObject);

        int height = 0;
        for (int i = 0; i < _newShapePos.Count; i++)
        {
            float y = _newShapePos[i].transform.localPosition.y;

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

    public void OnSaveNameChange(Text text)
    {
        _cubeName = text.text;
    }

    public bool OnGetCubeLocalPosCheck(int value)
    {
        for (int i = 0; i < _newShapePos.Count; i++)
        {
            if (_newShapePos[i].transform.localPosition.y >= 4)
                return false;
        }

        return true;
    }

    public void OnSave()
    {
        _newShapePos.Add(_parentCube);

        ShapePosData shapePosData = new ShapePosData();
        Vector3[] vector3s = new Vector3[_newShapePos.Count];

        for (int i = 0; i < _newShapePos.Count; i++)
        {
            vector3s[i] = _newShapePos[i].transform.position;
        }
        shapePosData.SetVector3s(vector3s);

        Material material = new Material(Shader.Find("Standard"));
        material.color = _material.color;     

        ShapesMaterialSetting(material);

        _newShapePos.Remove(_parentCube);
        ShapesScaleSetting();

        _parentCube.transform.position = new Vector3(0.64f, 0f, 0.88f);
        _parentCube.AddComponent<NextShapeRotation>();

        if (_cubeName.Equals(""))
            _cubeName = "Temp";

        AssetDatabase.CreateAsset(material, "Assets/Resources/ShapeMaterials/" + _cubeName + ".mat");
        AssetDatabase.CreateAsset(shapePosData, "Assets/Resources/ShapePosData/" + _cubeName + ".asset");
        PrefabUtility.SaveAsPrefabAssetAndConnect(_parentCube, "Assets/Resources/Shapes/" + _cubeName + ".prefab" , InteractionMode.UserAction);
        SceneManager.LoadScene("Lobby");
    }
}

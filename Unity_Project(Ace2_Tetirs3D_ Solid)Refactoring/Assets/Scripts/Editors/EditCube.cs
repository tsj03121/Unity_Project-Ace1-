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
    List<Vector3> newShapePos = new List<Vector3>();

    Vector3 _basicCreatePos = new Vector3(5, 19, 5);

    string _cubeName = "";

    void Start()
    {
        _material.color = Color.white;
        newShapePos.Add(_parentCube.transform.position);
    }

    void AllPosMove(Vector3 vector3, int max = 1)
    {
        for (int reset = 0; reset < max; reset++)
        {
            for (int i = 0; i < newShapePos.Count; i++)
            {
                newShapePos[i] += vector3;
            }
            _parentCube.transform.position += vector3;
        }
    }

    public void OnCreateClick(Vector3 vector3)
    {
        GameObject newCube = Instantiate(_cube);
        newCube.transform.parent = _parentCube.transform;
        newCube.transform.position = vector3;
        newCube.GetComponent<MeshRenderer>().material = _material;

        newShapePos.Add(newCube.transform.position);

        if (newCube.transform.position.y > 0)
        {
            AllPosMove(Vector3.down);
        }
    }

    public void OnDeleteClick(GameObject gameObject)
    {
        if (gameObject == _parentCube)
            return;

        int index = newShapePos.IndexOf(gameObject.transform.position);
        newShapePos.RemoveAt(index);
        Destroy(gameObject);

        bool isAllPosMoveCheck = false;
        int height = -99999;
        for (int i = 0; i < newShapePos.Count; i++)
        {
            if (height < newShapePos[i].y)
            {
                height = (int) newShapePos[i].y;
            }
                
            if (newShapePos[i].y == 0)
            {
                isAllPosMoveCheck = false;
                return;
            }
            else
            {
                isAllPosMoveCheck = true;
            }
        }

        if (isAllPosMoveCheck)
        {
            AllPosMove(Vector3.up * (height * -1));
        }
    }

    public void SliderChange(Slider slider)
    {
        Color color = _material.color;

        if (slider.name.ToString().Equals("R"))
        {
            color.r = slider.value * -1;
        }
        else if (slider.name.ToString().Equals("G"))
        {
            color.g = slider.value * -1;
        }
        else if (slider.name.ToString().Equals("B"))
        {
            color.b = slider.value * -1;
        }

        _material.color = color;
    }

    public void CubeNameChange(Text text)
    {
        _cubeName = text.text;
    }

    public void OnSaveClick()
    {
        ShapePosData shapePosData = new ShapePosData();

        Vector3[] vector3s = new Vector3[newShapePos.Count];

        for (int i = 0; i < newShapePos.Count; i++)
        {
            vector3s[i] = newShapePos[i];
        }

        shapePosData.SetVector3s(vector3s);

        //string printStr = "";
        //for (int i = 0; i < newShapePos.Count; i++)
        //{
        //    if (newShapePos.Count - 1 == i)
        //    {
        //        printStr += "_basicCreatePos + new Vector3(" + newShapePos[i].x + "f, " + newShapePos[i].y + "f, " + newShapePos[i].z + "f)" + "\n";
        //    }
        //    else
        //    {
        //        printStr += "_basicCreatePos + new Vector3(" + newShapePos[i].x + "f, " + newShapePos[i].y + "f, " + newShapePos[i].z + "f),\n";
        //    }
        //}
        //Debug.Log(printStr);
        
        if (_cubeName.Equals(""))
            _cubeName = "Temp";

        Material material = new Material(Shader.Find("Standard"));
        material.color = _material.color;
        _parentCube.transform.localScale = Vector3.one * 0.05f;
        _parentCube.transform.position = Vector3.up * 0.3f;
        _parentCube.AddComponent<NextShapeRotation>();

        AssetDatabase.CreateAsset(material, "Assets/Materials/Shape/" + _cubeName + ".mat");
        AssetDatabase.CreateAsset(shapePosData, "Assets/" + _cubeName + ".asset");
        PrefabUtility.SaveAsPrefabAssetAndConnect(_parentCube, "Assets/Prefabs/Shape/" + _cubeName + ".prefab" , InteractionMode.UserAction);
        SceneManager.LoadScene("Lobby");
    }
}

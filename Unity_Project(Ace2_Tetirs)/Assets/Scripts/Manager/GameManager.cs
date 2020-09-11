using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Action<Image[]> GameStart;
    Image[] images;
    // Start is called before the first frame update
    void Start()
    {
        GameStart?.Invoke(images);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

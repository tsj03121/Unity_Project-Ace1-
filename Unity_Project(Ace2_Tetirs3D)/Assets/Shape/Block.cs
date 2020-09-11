using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    public Action<Block, bool> ShapeTriggerSetting;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
            return;

        if (other.gameObject.CompareTag("Grid"))
            return;

        Debug.Log(other.name);
        ShapeTriggerSetting?.Invoke(this, true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
            return;

        if (other.gameObject.CompareTag("Grid"))
            return;

        ShapeTriggerSetting?.Invoke(this, false);
    }
}

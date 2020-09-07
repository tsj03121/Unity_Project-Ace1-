using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    AudioStorage soundStorage;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySound(SoundId id)
    { 
        AudioSource.PlayClipAtPoint(soundStorage.Get(id), Vector3.zero);
    }
}

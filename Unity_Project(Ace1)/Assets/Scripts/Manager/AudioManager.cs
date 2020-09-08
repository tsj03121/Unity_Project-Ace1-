using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance_;

    [SerializeField]
    AudioStorage soundStorage_;

    void Awake()
    {
        if (instance_ == null)
        {
            instance_ = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySound(SoundId id)
    { 
        AudioSource.PlayClipAtPoint(soundStorage_.Get(id), Vector3.zero);
    }
}

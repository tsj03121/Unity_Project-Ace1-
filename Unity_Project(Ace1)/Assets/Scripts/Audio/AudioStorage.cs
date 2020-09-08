using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class AudioStorage : ScriptableObject
{
    [SerializeField]
    SoundSrc[] soundSrcs_;

    Dictionary<SoundId, AudioClip> dicSounds_ = new Dictionary<SoundId, AudioClip>();

    void GenerateDictionary()
    {
        for (int i = 0; i < soundSrcs_.Length; ++i)
        {
            dicSounds_.Add(soundSrcs_[i].Id, soundSrcs_[i].SoundFile);
        }
    }

    public AudioClip Get(SoundId id)
    {
        Debug.Assert(soundSrcs_.Length > 0, "No soundSource Data!");

        if (dicSounds_.Count == 0)
        {
            GenerateDictionary();
        }

        return dicSounds_[id];
    }
}

[Serializable]
public struct SoundSrc
{
    [SerializeField]
    AudioClip soundFile_;

    [SerializeField]
    SoundId soundId_;

    public AudioClip SoundFile { get { return soundFile_; } }
    public SoundId Id { get { return soundId_; } }
}

public enum SoundId
{
    Shoot,
    BulletExplosion,
    BuildingExplosion,
    GameEnd
}

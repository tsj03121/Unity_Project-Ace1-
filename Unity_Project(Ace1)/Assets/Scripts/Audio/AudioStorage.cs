using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class AudioStorage : ScriptableObject
{
    [SerializeField]
    SoundSrc[] soundSrcs;

    Dictionary<SoundId, AudioClip> dicSounds = new Dictionary<SoundId, AudioClip>();

    void GenerateDictionary()
    {
        for (int i = 0; i < soundSrcs.Length; ++i)
        {
            dicSounds.Add(soundSrcs[i].Id, soundSrcs[i].SoundFile);
        }
    }

    public AudioClip Get(SoundId id)
    {
        Debug.Assert(soundSrcs.Length > 0, "No soundSource Data!");

        if (dicSounds.Count == 0)
        {
            GenerateDictionary();
        }

        return dicSounds[id];
    }
}

[Serializable]
public struct SoundSrc
{
    [SerializeField]
    AudioClip soundFile;

    [SerializeField]
    SoundId soundId;

    public AudioClip SoundFile { get { return soundFile; } }
    public SoundId Id { get { return soundId; } }
}

public enum SoundId
{
    Shoot,
    BulletExplosion,
    BuildingExplosion,
    GameEnd
}

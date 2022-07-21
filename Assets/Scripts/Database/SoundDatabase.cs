using System;
using UnityEngine;


[CreateAssetMenu(fileName = "New Sound Database", menuName = "Game/Database/Sound")]
public class SoundDatabase : ScriptableObject
{
    public enum SfxType
    {
        HammerHit,
    }
    public AudioClip[] bgm;
    public Sfx[] sfx;
    

    /// <summary>
    /// Get the AudioClip for the given SfxType.
    /// </summary>
    /// <param name="type">Type of the Sfx (Sfx Class Array Must Match the Enum)</param>
    /// <returns>AudioClips Array From that type of SFX</returns>
    public AudioClip[] GetSfx(SfxType type)
    {
        return sfx[(int)type].clips;
    }
}

[Serializable]
public class Sfx
{
    public string name;
    public AudioClip[] clips;
    
}

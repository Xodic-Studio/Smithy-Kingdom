using UnityEngine;


[CreateAssetMenu(fileName = "New Sound Database", menuName = "Game/Database/Sound")]
public class SoundDatabase : ScriptableObject
{
    public AudioClip[] bgm;
    public AudioClip[] sfx;
}

using UnityEngine;

[CreateAssetMenu(fileName = "SoundList", menuName = "Audio/AudioClipList")]
public class SoundList : SingletonScriptableObject<SoundList>
{
    public AudioClip titleBGM;
    public AudioClip chapter1BGM;
    public AudioClip chapter2BGM;
    public AudioClip chapter3BGM;
    public AudioClip chapter4BGM;
    public AudioClip equipClip;
    public AudioClip holyBladeClip;
    public AudioClip monsterHit;
    public AudioClip mark;
    public AudioClip playerHit;
    public AudioClip playerDead;
    public AudioClip playerJump;
}
using UnityEngine;

[CreateAssetMenu(fileName = "SoundList", menuName = "Audio/AudioClipList")]
public class SoundList : SingletonScriptableObject<SoundList>
{
    public AudioClip titleBGM;
    public AudioClip chapter1BGM;
    public AudioClip chapter2BGM;
    public AudioClip chapter3BGM;
    public AudioClip chapter4BGM;
    
    public AudioClip buttonClick;
    public AudioClip menuOpen;
    public AudioClip menuClose;
    public AudioClip altarOpen;
    public AudioClip altarClose;
    public AudioClip altarBuy;
    public AudioClip altarEquip;
    public AudioClip altarUnequip;
    
    public AudioClip monsterHit;

    public AudioClip playerHit;

    public AudioClip playerHolyBlade;
    public AudioClip playerMark;
    public AudioClip playerDash;
    public AudioClip playerDead;
    public AudioClip playerJump;
    public AudioClip playerSkill1;
    public AudioClip playerSkill2;
    public AudioClip playerRevive;

    public AudioClip timeSlow;
    public AudioClip soulGet;
}
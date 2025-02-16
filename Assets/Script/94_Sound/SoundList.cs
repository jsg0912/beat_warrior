using UnityEngine;

[CreateAssetMenu(fileName = "SoundList", menuName = "Audio/AudioClipList")]
public class SoundList : SingletonScriptableObject<SoundList>
{
    public AudioClip titleBGM;
    public AudioClip chapter1BGM;
    public AudioClip chapter2BGM;
    public AudioClip chapter3BGM;
    public AudioClip chapter4BGM;

    public AudioClip chapter2BossMapTitle;
    
    public AudioClip buttonClick;
    public AudioClip menuOpen; //
    public AudioClip menuClose; //
    public AudioClip altarOpen;
    public AudioClip altarClose;
    public AudioClip altarBuy; //
    public AudioClip altarEquip; //
    public AudioClip altarUnequip; //
    
    public AudioClip monsterHit; //
    public AudioClip defMonsterHit; //
    public AudioClip monsterIppaliAttack; //
    public AudioClip monsterIbkkugiAttack; //
    public AudioClip monsterKoppulsoCharge; //2.0s
    public AudioClip monsterKoppulsoAttack; //0.3s
    public AudioClip monsterDulduliCharge; //
    public AudioClip monsterDulduliAttack; // in attackend animation
    public AudioClip monsterGiljjugiAttack; //
    public AudioClip monsterItmomiAttack; //
    public AudioClip monsterItmomiThorn; // 1.0s up in itmomi attack animation

    public AudioClip bossSpit;
    public AudioClip bossIppaliSpawn;
    public AudioClip bossTentacleStab;
    public AudioClip bossTentacleHit;
    public AudioClip bossAttackThree;
    public AudioClip bossHalfHp;
    public AudioClip bossDie;
    public AudioClip bossBackGroundSoundEffect;

    public AudioClip playerHit; //
    public AudioClip playerHolyBlade; //
    public AudioClip playerMark; //
    public AudioClip playerDash; //
    public AudioClip playerDead; //
    public AudioClip playerJump; //
    public AudioClip playerQSkill; //
    public AudioClip playerESkill; //
    public AudioClip playerRevive0; //
    public AudioClip playerRevive1; // 6.5

    public AudioClip timeSlow;
    public AudioClip soulGet; //
    public AudioClip elevator;//
}
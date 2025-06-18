using UnityEngine;

[CreateAssetMenu(fileName = "SoundList", menuName = "Audio/AudioClipList")]
public class SoundList : SingletonScriptableObject<SoundList>
{
    // Chapter BGM
    public AudioClip titleBGM;
    public AudioClip chapter1BGM;
    public AudioClip chapter2BGM;
    public AudioClip chapter2BossBGM;
    public AudioClip chapter3BGM;
    public AudioClip chapter4BGM;

    //Chapter BGSFX
    public AudioClip bossBackGroundSoundEffect;
    public AudioClip chapter1SoundEffect;
    public AudioClip chapter2SoundEffect;

    // Stage Special BGM
    public AudioClip chapter2BossMapTitle;

    // UI SFX
    public AudioClip buttonClick;
    public AudioClip buttonHover;
    public AudioClip menuOpen; //
    public AudioClip menuClose; //
    public AudioClip altarOpen;
    public AudioClip altarClose;
    public AudioClip altarBuy; //
    public AudioClip altarEquip; //
    public AudioClip altarUnequip; //

    // Monster SFX
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

    // Boss SFX - Gurges
    public AudioClip bossSpit;
    public AudioClip bossIppaliSpawn;
    public AudioClip bossTentacleStabBoss;
    public AudioClip bossTentacleStabTentacle;
    public AudioClip bossTentacleHit;
    public AudioClip bossAttackOneTwo;
    public AudioClip bossAttackThree;
    public AudioClip bossStand;
    public AudioClip bossDie0;
    public AudioClip bossDie1;
    public AudioClip bossDie2;

    // Player SFX
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

    // Item SFX
    public AudioClip timeSlow;
    public AudioClip soulGet; //
    public AudioClip elevator;//
    public AudioClip elevatorEnd;
    public AudioClip IronMaden;
    public AudioClip FloorSpike;
    public AudioClip FloorGas;
    public AudioClip Vallista;
}
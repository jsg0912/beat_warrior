using System.Collections.Generic;
using UnityEngine;

public class BossGergus : Monster
{
    public int Phase = 1;
    [SerializeField] public List<BossTentacle> tentaclesVertical;
    [SerializeField] public List<BossTentacle> tentaclesHorizontal;
    [SerializeField] public List<BossTentacle> tentaclesCrash;

    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        monsterUnit = MonsterList.FindMonster(monsterName);
        pattern = PatternFactory.GetPatternByPatternName(monsterUnit.patternName);
        pattern.Initialize(this);

        HpUI.SetMaxHP(monsterUnit.GetCurrentHP()); // Customizing HP Code - SDH, 20250119

        foreach (var tentacle in tentaclesVertical) tentacle.Boss = this;
        foreach (var tentacle in tentaclesHorizontal) tentacle.Boss = this;
        foreach (var tentacle in tentaclesCrash) tentacle.Boss = this;
    }

    public override void SetMovingDirection(Direction dir)
    {
        return;
    }

    public override void PlayScarEffect()
    {
        Vector3 playerPos = Player.Instance.GetMiddlePos();
        Collider2D bossCollider = monsterBodyCollider.GetCollider(); // it can be boxCollider2D or polygonCollider2D

        Vector3 closetPoint = bossCollider.ClosestPoint(playerPos);

        foreach (GameObject hitEffect in hitEffects)
        {
            hitEffect.transform.position = closetPoint;
            StartCoroutine(Util.PlayInstantEffect(hitEffect, 0.3f));
        }
    }

    override public void PlayAnimation(string trigger)
    {
        if (isFixedAnimation) return;
        if (trigger == MonsterAnimTrigger.hurtAnimTrigger)
        {
            PlayScarEffect();
            // 색깔 변화
            return;
        }
        _animator.SetTrigger(trigger);
    }

    public void PlaySpitSFX()
    {
        SoundManager.Instance.SFXPlay(SoundList.Instance.bossSpit);
    }
    public void PlayStabSFX()
    {
        SoundManager.Instance.SFXPlay(SoundList.Instance.bossTentacleStabBoss);
    }
    public void PlayAtkOTSFX()
    {
        SoundManager.Instance.SFXPlay(SoundList.Instance.bossAttackOneTwo);
    }
    public void PlayAtkTSFX()
    {
        SoundManager.Instance.SFXPlay(SoundList.Instance.bossAttackThree);
    }
    public void PlayStandSFX()
    {
        SoundManager.Instance.SFXPlay(SoundList.Instance.bossStand);

    }
    public void PlayDie0SFX()
    {
        SoundManager.Instance.SFXPlay(SoundList.Instance.bossDie0);

    }
    public void PlayDie1SFX()
    {
        SoundManager.Instance.SFXPlay(SoundList.Instance.bossDie1);
        SoundManager.Instance.StopBackGroundSFX();

    }
    public void PlayDie2SFX()
    {
        SoundManager.Instance.SFXPlay(SoundList.Instance.bossDie2);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class BossGergus : Monster
{
    [SerializeField] public List<BossTentacle> tentaclesVertical;
    [SerializeField] public List<BossTentacle> tentaclesHorizontal;
    [SerializeField] public List<BossTentacle> tentaclesCrash;

    protected override void Start()
    {
        base.Start();

        foreach (var tentacle in tentaclesVertical) tentacle.Boss = this;
        foreach (var tentacle in tentaclesHorizontal) tentacle.Boss = this;
        foreach (var tentacle in tentaclesCrash) tentacle.Boss = this;
    }

    public override void SetMovingDirection(Direction dir)
    {
        return;
    }

    public override void GetDamaged(int dmg)
    {
        base.GetDamaged(dmg);
        PlayScarEffect();
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

    public void PlaySpitSFX()
    {
        SoundManager.Instance.SFXPlay("BossSpit", SoundList.Instance.bossSpit);
    }
    public void PlayStabSFX()
    {
        SoundManager.Instance.SFXPlay("TentacleStabBoss", SoundList.Instance.bossTentacleStabBoss);
    }
    public void PlayAtkOTSFX()
    {
        SoundManager.Instance.SFXPlay("BossAttackOneTwo", SoundList.Instance.bossAttackOneTwo);
    }
    public void PlayAtkTSFX()
    {
        SoundManager.Instance.SFXPlay("BossAttackThree", SoundList.Instance.bossAttackThree);
    }
    public void PlayStandSFX()
    {
        SoundManager.Instance.SFXPlay("BossStand", SoundList.Instance.bossStand);

    }
    public void PlayDie0SFX()
    {
        SoundManager.Instance.SFXPlay("BossDie0", SoundList.Instance.bossDie0);

    }
    public void PlayDie1SFX()
    {
        SoundManager.Instance.SFXPlay("BossDie1", SoundList.Instance.bossDie1);

    }
    public void PlayDie2SFX()
    {
        SoundManager.Instance.SFXPlay("BossDie2", SoundList.Instance.bossDie2);
    }





}
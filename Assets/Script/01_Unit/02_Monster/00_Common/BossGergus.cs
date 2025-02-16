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
}
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
        // Boss의 Collider 내부의 점들 중에서 Player의 현재 위치와 가장 가까운 점을 찾아 Scar Effect를 옮겨서 켠다.
        Vector3 playerPos = Player.Instance.GetMiddlePos();
        Vector3 bossPos = GetMiddlePos();
        Vector3 bossColliderPos = attackCollider.transform.position;



    }
}
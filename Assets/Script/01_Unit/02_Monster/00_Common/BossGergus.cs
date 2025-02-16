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
}
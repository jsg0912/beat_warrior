using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
    [SerializeField] List<BossTentacle> tentaclesVertical;
    [SerializeField] List<BossTentacle> tentaclesHorizontal;
    [SerializeField] List<BossTentacle> tentaclesCrash;

    protected override void Start()
    {
        base.Start();

        foreach (var tentacle in tentaclesVertical) tentacle.Boss = this;
        foreach (var tentacle in tentaclesHorizontal) tentacle.Boss = this;
        foreach (var tentacle in tentaclesCrash) tentacle.Boss = this;
    }
}
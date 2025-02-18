using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStrategyBossGergusTentacle : AttackStrategyMeleeRandomAttack
{
    protected List<BossTentacle> tentacleVertical = new();
    protected List<BossTentacle> tentacleHorizontal = new();
    protected List<BossTentacle> tentacles = new();
    protected BossGergus bossGergus;

    public AttackStrategyBossGergusTentacle(string monsterAnimTrigger = MonsterAnimTrigger.attackChargeAnimTrigger,
    List<BossTentacle> tentacleVertical = null, List<BossTentacle> tentacleHorizontal = null) : base(monsterAnimTrigger, tentacleVertical)
    {
        this.tentacleVertical = tentacleVertical;
        this.tentacleHorizontal = tentacleHorizontal;
    }

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        bossGergus = monster as BossGergus;
    }

    public override void AttackStart()
    {
        tentacles.Clear(); // 기존 리스트 초기화

        if (bossGergus.Phase == 1)
        {
            List<BossTentacle> selectedList = Random.Range(0, 2) == 0 ? tentacleVertical : tentacleHorizontal;
            AddRandomElements(selectedList, 2);
        }
        else if (bossGergus.Phase == 2)
        {
            AddRandomElements(tentacleVertical, 1);
            AddRandomElements(tentacleHorizontal, 1);
        }

        monoBehaviour.StartCoroutine(ActivateTentacles());
    }

    void AddRandomElements(List<BossTentacle> sourceList, int count)
    {
        if (sourceList.Count < count) return;

        List<BossTentacle> tempList = new List<BossTentacle>(sourceList);
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            tentacles.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
        }
    }

    IEnumerator ActivateTentacles()
    {
        foreach (BossTentacle tentacle in attackColliders)
        {
            Util.SetActive(tentacle.gameObject, true);
            yield return new WaitForSeconds(2f);
        }
    }

    public override void AttackEnd()
    {
        tentacles.RemoveAt(0);
        if (tentacles.Count == 0) bossGergus.PlayAnimation(MonsterAnimTrigger.attackEndAnimTrigger);
    }

    protected override void AttackMethod()
    {
    }
}
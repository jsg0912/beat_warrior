using UnityEngine;

public class Execution : PassiveSkill
{
    private int executionHP;

    public Execution(GameObject unit) : base(unit)
    {
        skillName = SkillName.Execution;
        executionHP = 1;
    }

    public override void GetSkill()
    {
        Player.Instance.hitMonsterFuncList += ExecutionMonster;
    }

    public void ExecutionMonster(MonsterUnit monster)
    {
        int currentHP = monster.GetCurrentHP();
        if (currentHP <= executionHP) monster.ChangeCurrentHP(-currentHP);
    }

    public override void RemoveSkill()
    {
        Player.Instance.HitMonsterFuncList -= ExecutionMonster;
    }
}

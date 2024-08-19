using UnityEngine;

public class Execution : PassiveSkill
{
    private int executionHP;

    public Execution(GameObject unit) : base(unit) { }

    public override void GetSkill()
    {
        skillName = SkillName.Execution;
        executionHP = 1;

        Player.Instance.HitMonsterFuncList += ExecutionMonster;
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

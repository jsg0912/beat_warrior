using UnityEngine;

public class Execution : PassiveSkill
{
    private int executionHP;

    public Execution(GameObject unit) : base(unit)
    {
        executionHP = 1;
    }

    protected override void SetSkillName() { skillName = SkillName.Execution; }

    public override void GetSkill()
    {
        Player.Instance.hitMonsterFuncList += ExecutionMonster;
    }

    public void ExecutionMonster(Monster monster)
    {
        int currentHP = monster.GetCurrentHP();
        if (currentHP <= executionHP) monster.AttackedByPlayer(currentHP, true);
    }

    public override void RemoveSkill()
    {
        Player.Instance.hitMonsterFuncList -= ExecutionMonster;
    }
}

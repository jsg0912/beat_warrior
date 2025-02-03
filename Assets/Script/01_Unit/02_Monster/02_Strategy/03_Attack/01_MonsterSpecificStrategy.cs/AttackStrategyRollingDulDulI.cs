using System.Collections;
using UnityEngine;

public class AttackStrategyRollingDulDulI : AttackStrategyRolling
{
    public AttackStrategyRollingDulDulI()
    {
        jumpPower = MonsterConstant.DulduliJumpPower;
        numJumps = MonsterConstant.DulduliJumpNumber;
        duration = MonsterConstant.DulduliJumpDuration;
    }

    protected override IEnumerator UseSkill()
    {
        monster.SetStatus(MonsterStatus.Attack);
        yield return new WaitForSeconds(attackStartDelay);

        monster.PlayAnimation(MonsterStatus.Attack);
        yield return new WaitForSeconds(attackActionInterval);
        SkillMethod();

        yield return new WaitForSeconds(duration + 0.3f);
        SetAfterSkill();

        monster.PlayAnimation(MonsterStatus.AttackEnd);
        monster.SetBuffMultiply(StatKind.Def, -1);
        yield return new WaitForSeconds(MonsterConstant.DulduliGroggyDuration);
        monster.ResetBuffMultiply(StatKind.Def);
    }
}
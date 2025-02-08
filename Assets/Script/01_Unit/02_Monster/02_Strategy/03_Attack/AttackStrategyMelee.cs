using System.Collections;

public class AttackStrategyMelee : AttackStrategy
{
    protected override void SkillMethod()
    {
        monster.PlayAnimation(MonsterStatus.Attack);
        monster.SetIsTackleAble(true);
        monoBehaviour.StartCoroutine(TurnOffAttack());
    }

    private IEnumerator TurnOffAttack()
    {
        if (monster.GetStatus() == MonsterStatus.Attack) yield return null;
        monster.SetIsTackleAble(false);
    }
}
using UnityEngine;

public class Revive : PassiveSkill
{
    private const int ReviveHP = 2;
    bool isUsed;

    public Revive(GameObject unit) : base(unit)
    {
        isUsed = false;
    }

    protected override void SetSkillName() { skillName = SkillName.Revive; }

    public override void GetSkill()
    {
        Player.Instance.reviveSKillFuncList += RevivePlayer;
    }

    public override void RemoveSkill()
    {
        Player.Instance.reviveSKillFuncList -= RevivePlayer;
    }


    public bool RevivePlayer()
    {
        bool isAlive = false;
        if (isUsed) return isAlive;

        Player player = Player.Instance;
        player.SetAnimTrigger(PlayerConstant.reviveAnimTrigger);
        player.ForceSetCurrentHp(ReviveHP);
        isUsed = true;
        isAlive = true;

        return isAlive;
    }
}

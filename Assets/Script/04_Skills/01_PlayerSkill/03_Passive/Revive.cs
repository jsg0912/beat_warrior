using UnityEngine;

public class Revive : PassiveSkill
{
    private const int ReviveHP = 2;
    bool isUsed;
    GameObject reviveEffect;

    public Revive(GameObject unit) : base(unit)
    {
        isUsed = false;
        reviveEffect = Resources.Load(PrefabRouter.ReviveEffectPrefab) as GameObject;
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


    public bool RevivePlayer() // return isRevive
    {
        if (isUsed) return false;

        Player player = Player.Instance;
        player.SetAnimTrigger(PlayerConstant.reviveAnimTrigger);
        player.ForceSetCurrentHp(ReviveHP);

        Vector3 position = player.transform.position + new Vector3(0, 2, 0);
        GameObject effect = GameObject.Instantiate(reviveEffect, position, Quaternion.identity);
        isUsed = true;
        GameObject.Destroy(effect, PlayerSkillConstant.reviveDuration);

        return true;
    }
}

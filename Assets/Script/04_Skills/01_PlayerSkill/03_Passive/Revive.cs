using DG.Tweening;
using UnityEngine;

public class Revive : PassiveSkill
{
    private const int ReviveHP = 2;
    bool isUsed;
    Player player => Player.Instance;
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

        player.SetAnimTrigger(PlayerConstant.reviveAnimTrigger);
        isUsed = true;

        return true;
    }

    public void ReviveFunctionBefore()
    {
        player.transform.DOMoveY(player.transform.position.y + 1.0f, 2.0f).SetEase(Ease.InSine);
        Vector3 position = player.transform.position + new Vector3(0, 2, 0);
        GameObject effect = GameObject.Instantiate(reviveEffect, position, Quaternion.identity);
        player.SetGravityScale(false);
        player.SetInvincibility(true);
        GameObject.Destroy(effect, PlayerSkillConstant.reviveEffectDuration);
    }

    public void ReviveFunctionAfter()
    {
        player.ForceSetCurrentHp(ReviveHP);
        player.SetGravityScale(true);
        player.SetInvincibility(false);
    }
}

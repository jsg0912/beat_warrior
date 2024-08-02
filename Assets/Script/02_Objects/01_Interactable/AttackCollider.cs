using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : AttackColliderController
{
    private float attackForce;

    protected override void Initiallize()
    {
        atk = PlayerSkillConstant.attackAtk;
        attackForce = PlayerSkillConstant.attackKnockbackRange;
    }

    protected override void AttackMethod(GameObject obj)
    {
        int dir = Player.Instance.transform.position.x < obj.transform.position.x ? 1 : -1;
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir * attackForce, 0.0f), ForceMode2D.Impulse);
    }
}

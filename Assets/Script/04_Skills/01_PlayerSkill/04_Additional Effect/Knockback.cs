using UnityEngine;

public class KnockBack : AdditionalEffect
{
    private float knockBackDistance;
    public KnockBack(float knockBackDistance) : base()
    {
        this.knockBackDistance = knockBackDistance;
    }

    public override void SetAdditionalEffectName() { additionalEffectName = AdditionalEffectName.KnockBack; }

    public override void work(GameObject obj)
    {
        Monster monster = obj.GetComponent<Monster>();
        if (monster != null)
        {
            if (!monster.GetIsKnockBackAble()) return;
            monster.StopAttack();
        }
        float dir = Player.Instance.GetMovingDirectionFloat();
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir * knockBackDistance, 0.0f), ForceMode2D.Impulse);
    }
}
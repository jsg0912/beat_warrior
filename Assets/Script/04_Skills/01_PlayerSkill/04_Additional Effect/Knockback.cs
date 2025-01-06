using UnityEngine;

public class KnockBack : AdditionalEffect
{
    private float knockBackDistance;
    public KnockBack(float knockBackDistance)
    {
        this.knockBackDistance = knockBackDistance;
    }
    public override void work(GameObject obj)
    {
        int dir = Player.Instance.GetDirection();
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir * knockBackDistance, 0.0f), ForceMode2D.Impulse);
    }
}

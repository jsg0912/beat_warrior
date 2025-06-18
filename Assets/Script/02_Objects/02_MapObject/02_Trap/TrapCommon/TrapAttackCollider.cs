using System.Collections;
using UnityEngine;

public class TrapAttackCollider : MonoBehaviour
{
    private bool isAttacked = false;
    private int damage;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void Initialize()
    {
        isAttacked = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.CompareTag(TagConstant.Player) && isAttacked == false)
        {
            Player.Instance.GetDamaged(damage, Player.Instance.GetRelativeDirectionToTarget(transform.position));
            isAttacked = true;
            StartCoroutine(ResetIsAttackedAfterTime(1f)); // Reset isAttacked after 1 second
        }
    }

    public IEnumerator ResetIsAttackedAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        isAttacked = false;
    }
}

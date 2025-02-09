using System.Collections;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private BoxCollider2D playerCollider;
    public AttackCollider shortBladeCollider;
    public AttackCollider BodyAttackCollider;

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
    }

    public void InitializeAttackCollider()
    {
        shortBladeCollider.ResetTargetMonster();
        BodyAttackCollider.ResetTargetMonster();
    }

    public void SetColliderTrigger(bool isTrigger)
    {
        playerCollider.isTrigger = isTrigger;
    }

    public void PassTile(BoxCollider2D tileCollider)
    {
        StartCoroutine(DisableCollision(tileCollider));
    }

    private IEnumerator DisableCollision(BoxCollider2D tileCollider)
    {
        Physics2D.IgnoreCollision(playerCollider, tileCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, tileCollider, false);
    }
}

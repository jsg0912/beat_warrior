using UnityEngine;

public class MonsterAttackColliderIbkkugi : MonsterAttackCollider
{
    void Start()
    {
        Destroy(gameObject, 10.0f);
    }

    void Update()
    {
        if (CheckBase(Vector3.down) || CheckBase(Vector3.left) || CheckBase(Vector3.right)) Destroy(gameObject, 0.03f);
    }

    private bool CheckBase(Vector3 direction)
    {
        Collider2D collider = CheckRay(direction).collider;
        if (collider != null && collider.CompareTag(TagConstant.Base)) return true;
        return false;
    }
    private RaycastHit2D CheckRay(Vector3 direction) { return Physics2D.Raycast(transform.position, direction, 0.3f, LayerMask.GetMask(LayerConstant.Tile)); }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag(TagConstant.Player))
        {
            Player.Instance.GetDamaged(damage, GetRelativeDirectionToPlayer());
            Destroy(gameObject);
        }
    }
}

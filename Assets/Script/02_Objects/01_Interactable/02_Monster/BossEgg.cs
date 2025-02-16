using UnityEngine;

public class BossEgg : MonoBehaviour
{
    [SerializeField] GameObject monster;
    [SerializeField] Animator animator;

    public void SummonMonster()
    {
        Instantiate(monster, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerConstant.Tile))
        {
            animator.SetTrigger(BossConstantCh2.EggIsGroundAnimTrigger);
        }
    }
}

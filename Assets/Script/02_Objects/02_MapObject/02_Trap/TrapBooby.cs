using System.Collections;
using UnityEngine;

public class TrapBooby : Trap
{
    [SerializeField] private Animator animator;
    protected override void TrapAction()
    {
        base.TrapAction();
        animator.SetBool("Trap", true);
        isTriggered = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.CompareTag(TagConstant.Player) && isTriggered == false)
        {
            TrapAction();
            StartCoroutine(SetTrapCoolTime());
        }
    }

    protected IEnumerator SetTrapCoolTime()
    {
        yield return new WaitForSeconds(duration);
        animator.SetBool("Trap", false);

        yield return new WaitForSeconds(coolTime);
        Initialize();
    }
}
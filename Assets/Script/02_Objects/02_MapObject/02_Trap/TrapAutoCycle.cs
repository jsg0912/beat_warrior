using System.Collections;
using UnityEngine;

public class TrapAutoCycle : Trap
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        StartCoroutine(StartTrapCoolTime());
    }

    protected override void TrapAction()
    {
        base.TrapAction();
        animator.SetBool("Trap", true);
    }

    protected IEnumerator StartTrapCoolTime()
    {
        animator.SetBool("Trap", false);
        yield return new WaitForSeconds(coolTime);
        Initialize();
        TrapAction();
        yield return new WaitForSeconds(duration);
        yield return StartCoroutine(StartTrapCoolTime());
    }
}

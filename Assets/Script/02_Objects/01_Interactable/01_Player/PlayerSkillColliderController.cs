using System.Collections;
using UnityEngine;

public class PlayerSkillColliderController : MonoBehaviour
{
    [SerializeField] AttackCollider attackCollider;

    void OnEnable()
    {
        // GameObject가 활성화될 때 코루틴 시작
        StartCoroutine(DisableAfterDelay());
    }

    private IEnumerator DisableAfterDelay()
    {
        // 0.3초 대기
        yield return new WaitForSeconds(0.3f);

        // GameObject 비활성화
        gameObject.SetActive(false);
        attackCollider.ResetTargetMonster();
    }

    public void SetAdditionalEffect(AdditionalEffect additionalEffect)
    {
        attackCollider.SetAdditionalEffect(additionalEffect);
    }

    public void SetAtk(int atk)
    {
        attackCollider.SetAtk(atk);
    }
}
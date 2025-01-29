using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    protected int atk;
    protected List<GameObject> TargetMonster = new();
    public List<AdditionalEffect> additionalEffects = new();

    public void InitializeBeforeAttack()
    {
        additionalEffects.Clear();
        TargetMonster.Clear();
    }

    public void SetAtk(int atk)
    {
        this.atk = atk;
    }

    public void SetAdditionalEffect(AdditionalEffect additionalEffect)
    {
        if (additionalEffects == null)
        {
            additionalEffects = new List<AdditionalEffect>();
        }
        additionalEffects.Add(additionalEffect);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = Util.GetMonsterGameObject(collision);

        if (CheckAttackAbleCollision(obj))
        {
            TargetMonster.Add(obj);
            obj.GetComponent<Monster>().AttackedByPlayer(atk);
            additionalEffects?.ForEach(additionalEffect => additionalEffect.work(obj));
        }
    }

    private bool CheckAttackAbleCollision(GameObject gameObject)
    {
        // Alive Monster Check
        if (gameObject == null) return false;
        if (gameObject.CompareTag(TagConstant.Monster) == false) return false;
        if (gameObject.GetComponent<Monster>().GetIsAlive() == false) return false;

        // Check duplication
        if (TargetMonster.Contains(gameObject)) return false;
        return true;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    protected int atk;
    protected List<GameObject> TargetMonster;
    public List<AdditionalEffect> additionalEffects;

    void Start()
    {
        TargetMonster = new List<GameObject>();
        Destroy(gameObject, 0.1f);
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
        GameObject obj = collision.gameObject;

        if (CheckAttackAbleCollision(obj))
        {
            obj.GetComponent<Monster>().GetDamaged(atk);
            additionalEffects?.ForEach(additionalEffect => additionalEffect.work(obj));
            TargetMonster.Add(obj);
        }
    }

    private bool CheckAttackAbleCollision(GameObject gameObject)
    {
        // Alive Monster Check
        if (!gameObject.CompareTag(TagConstant.Monster)) return false;
        if (gameObject.GetComponent<Monster>().GetIsAlive() == false) return false;

        // Check duplication
        if (TargetMonster.Contains(gameObject)) return false;
        return true;
    }
}

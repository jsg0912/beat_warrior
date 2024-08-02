using System.Collections.Generic;
using UnityEngine;

public abstract class AttackColliderController : MonoBehaviour
{
    protected int atk;
    protected List<GameObject> TargetMonster;

    void Start()
    {
        Initiallize();

        TargetMonster = new List<GameObject>();
        Destroy(gameObject, 0.1f);
    }

    protected abstract void Initiallize();

    protected abstract void AttackMethod(GameObject obj);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (!obj.CompareTag("Monster")) return;

        if (TargetMonster.Contains(obj)) return;

        Debug.Log("wef");

        AttackMethod(obj);

        obj.GetComponent<Monster>().GetDamaged(atk);
        TargetMonster.Add(obj);
    }
}

using System.Collections;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [SerializeField] protected float duration = 3f;
    [SerializeField] protected float coolTime = 10f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected bool isTriggered = false;
    [SerializeField] protected TrapAttackCollider trapAttackCollider;

    protected void Initialize()
    {
        isTriggered = false;
        trapAttackCollider.Initialize();
        trapAttackCollider.SetDamage(damage);
    }

    private void Awake()
    {
        Initialize();
    }

    protected abstract void TrapAction();
}
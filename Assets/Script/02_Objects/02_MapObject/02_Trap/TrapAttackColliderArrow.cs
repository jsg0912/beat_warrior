using UnityEngine;

public class TrapAttackColliderArrow : TrapAttackCollider
{
    [SerializeField] private Rigidbody2D arrowRb;
    [SerializeField] private float projectileSpeed = 10f;

    public void Start()
    {
        arrowRb.gravityScale = 0; // No gravity
        arrowRb.drag = 0f; // No air resistance
    }

    public void OnEnable()
    {
        SetSpeed();
    }

    private void SetSpeed()
    {
        float fireAngle = transform.eulerAngles.z - 180f;
        float angleInRadians = fireAngle * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        arrowRb.velocity = direction * projectileSpeed;
    }
}
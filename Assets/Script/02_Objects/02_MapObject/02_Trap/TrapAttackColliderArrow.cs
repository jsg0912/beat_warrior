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
        // Calculate direction based on current rotation
        // Since default is facing left and we added 180 degrees to aim at player,
        // we need to subtract 180 to get the actual firing direction
        float fireAngle = transform.eulerAngles.z - 180f;
        float angleInRadians = fireAngle * Mathf.Deg2Rad;

        // Calculate velocity components
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        // Set arrow velocity
        arrowRb.velocity = direction * projectileSpeed;

        // Debug log to check if launch is working
        Debug.Log($"Arrow launched with velocity: {arrowRb.velocity}, angle: {fireAngle}");
    }
}
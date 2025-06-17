using System.Collections;
using UnityEngine;

public class TrapVallista : Trap
{
    [SerializeField] private Animator animator;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private GameObject arrow; // VallistaArrow GameObject

    private bool canShoot = true;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D arrowRb;

    protected override void TrapAction()
    {
        if (!canShoot) return;

        StartCoroutine(ShootSequence());
    }

    private void Start()
    {
        Initialize();
        player = Player.Instance.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get arrow's Rigidbody2D
        if (arrow != null)
        {
            arrowRb = arrow.GetComponent<Rigidbody2D>();
            if (arrowRb != null)
            {
                arrowRb.gravityScale = 0; // No gravity
                arrowRb.drag = 0f; // No air resistance
            }
        }
    }

    private void Update()
    {
        if (canShoot && player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= detectionRange && !isTriggered)
            {
                isTriggered = true;
                TrapAction();
            }
        }
    }

    private IEnumerator ShootSequence()
    {
        canShoot = false;

        // Aim at player for duration
        float aimTime = 0f;
        while (aimTime < duration)
        {
            AimAtPlayer();
            aimTime += Time.deltaTime;
            yield return null;
        }

        // Fire animation
        animator.SetBool("Trap", true);

        // Launch arrow (assuming animation event or delay needed)
        yield return new WaitForSeconds(0.5f); // Adjust timing based on animation
        LaunchArrow();

        // Cooldown
        yield return new WaitForSeconds(coolTime);

        // Reload
        animator.SetBool("Trap", false);
        isTriggered = false;
        canShoot = true;
    }

    private void AimAtPlayer()
    {
        if (player == null) return;

        // Calculate direction to player
        Vector2 direction = (player.position - transform.position).normalized;

        // Calculate angle in degrees (add 180 because default is facing left)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;

        // Smoothly rotate towards target
        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.LerpAngle(currentAngle, angle, Time.deltaTime * 2f); // Adjust speed with multiplier

        // Apply rotation
        transform.rotation = Quaternion.Euler(0, 0, newAngle);

        // Flip sprite based on adjusted angle
        if (spriteRenderer != null)
        {
            float normalizedAngle = newAngle % 360f;
            if (normalizedAngle > 90 && normalizedAngle < 270)
            {
                spriteRenderer.flipY = true;
            }
            else
            {
                spriteRenderer.flipY = false;
            }
        }
    }

    private void LaunchArrow()
    {
        if (arrow == null) return;

        // Ensure arrow is active
        arrow.SetActive(true);

        // Get or add Rigidbody2D component
        if (arrowRb == null)
        {
            arrowRb = arrow.AddComponent<Rigidbody2D>();
        }

        // Calculate direction based on current rotation
        // Since default is facing left and we added 180 degrees to aim at player,
        // we need to subtract 180 to get the actual firing direction
        float fireAngle = transform.eulerAngles.z - 180f;
        float angleInRadians = fireAngle * Mathf.Deg2Rad;

        // Calculate velocity components
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        // Set arrow velocity
        arrowRb.velocity = direction * projectileSpeed;

        // Set arrow rotation to match launch direction
        arrow.transform.rotation = transform.rotation;

        // Debug log to check if launch is working
        Debug.Log($"Arrow launched with velocity: {arrowRb.velocity}, angle: {fireAngle}");
    }

    private void OnDrawGizmosSelected()
    {
        // Draw detection range in editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
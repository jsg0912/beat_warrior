using System.Collections;
using UnityEngine;

public class TrapVallista : Trap
{
    [SerializeField] private Animator animator;
    [SerializeField] private float detectionRange = 10f;

    private bool canShoot = true;
    private Transform player;
    [SerializeField] private Transform arrowStartPoint;
    private SpriteRenderer spriteRenderer;

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

    private void ArrowInitialize()
    {
        trapAttackCollider.Initialize();
        trapAttackCollider.SetDamage(damage);
        trapAttackCollider.gameObject.transform.position = arrowStartPoint.position;
    }

    private IEnumerator ShootSequence()
    {
        canShoot = false;

        float aimTime = 0f;
        while (aimTime < duration)
        {
            AimAtPlayer();
            aimTime += Time.deltaTime;
            yield return null;
        }
        ArrowInitialize();
        animator.SetBool("Trap", true);

        yield return new WaitForSeconds(coolTime);

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
}
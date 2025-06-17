using System.Collections;
using System.Collections.Generic;
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
        
        // Calculate angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Apply rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        // Flip sprite if rotation is more than 180 degrees
        if (spriteRenderer != null)
        {
            if (angle > 90 || angle < -90)
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
        if (arrow == null || arrowRb == null) return;
        
        // Get current rotation direction
        Vector2 direction = transform.right; // Right direction when rotation is 0
        
        // Set arrow velocity
        arrowRb.velocity = direction * projectileSpeed;
        
        // Optional: Set arrow rotation to match launch direction
        arrow.transform.rotation = transform.rotation;
    }
    
    private void OnDrawGizmosSelected()
    {
        // Draw detection range in editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
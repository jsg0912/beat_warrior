using UnityEngine;

public static class PhysicsCalculator
{
    public static void StopRigidBody(Rigidbody2D rb) { rb.velocity = Vector2.zero; }
    public static void StopRigidBodyX(Rigidbody2D rb) { rb.velocity = new Vector2(0, rb.velocity.y); }
    public static void StopRigidBodyY(Rigidbody2D rb) { rb.velocity = new Vector2(rb.velocity.x, 0); }

    public static bool IsStoppedSpeed(Rigidbody2D rb) { return IsStoppedSpeed(rb.velocity.x) && IsStoppedSpeed(rb.velocity.y); }

    public static bool IsStoppedSpeedX(Rigidbody2D rb) { return IsStoppedSpeed(rb.velocity.x); }

    public static bool IsStoppedSpeedY(Rigidbody2D rb) { return IsStoppedSpeed(rb.velocity.y); }

    // Speed can be not exact zero, just check it is close to zero, - SDH, 20250208
    public static bool IsStoppedSpeed(float speed) { return -1e-4f <= speed && speed <= 1e4f; }

    public static Vector2 GetGroundPositionBelow(Vector2 vector2) { return new Vector2(vector2.x, vector2.y); }

    public static Vector2 PushPosition(Vector2 position, Vector2 direction, float distance) { return position + direction.normalized * distance; }
}

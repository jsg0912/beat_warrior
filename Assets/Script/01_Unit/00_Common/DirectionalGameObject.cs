using UnityEngine;

public abstract class DirectionalGameObject : MonoBehaviour
{
    // Graphic draws every sprite that is right direction; - SDH, 20250119;
    protected Direction movingDirection = Direction.Right;
    protected Direction objectDirection = Direction.Right;

    public Direction GetMovingDirection() { return movingDirection; }
    public float GetMovingDirectionFloat() { return (float)movingDirection; }

    public void SetMovingDirection(Direction dir)
    {
        movingDirection = dir;
        if (objectDirection != movingDirection)
        {
            FlipDirection();
            objectDirection = dir;
        }
    }

    public void FlipDirection()
    {
        Util.FlipLocalScaleX(gameObject);
    }
}
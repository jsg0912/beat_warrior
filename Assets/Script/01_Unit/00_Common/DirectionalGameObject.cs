using UnityEngine;

public abstract class DirectionalGameObject : MonoBehaviour
{
    // Graphic draws every sprite that is right direction; - SDH, 20250119;
    [SerializeField] protected Direction movingDirection = Direction.Right;
    [SerializeField] protected Direction objectDirection = Direction.Right;

    public Direction GetMovingDirection() { return movingDirection; }
    public float GetMovingDirectionFloat() { return (float)movingDirection; }

    public void SetMovingDirection(Direction dir)
    {
        movingDirection = dir;
        if (objectDirection != movingDirection)
        {
            FlipObjectScaleX();
            objectDirection = dir;
        }
    }

    public void FlipDirection()
    {
        if (movingDirection == Direction.Right) SetMovingDirection(Direction.Left);
        else SetMovingDirection(Direction.Right);
    }

    private void FlipObjectScaleX()
    {
        Util.FlipLocalScaleX(gameObject);
    }
}
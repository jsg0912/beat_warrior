using System.Collections.Generic;
using UnityEngine;

public abstract class DirectionalGameObject : MonoBehaviour
{
    // Graphic draws every sprite that is right direction; - SDH, 20250119;
    [SerializeField] protected Direction movingDirection = Direction.Right;
    [SerializeField] protected Direction objectDirection = Direction.Right;
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] private List<GameObject> childDirectionalObjects = new List<GameObject>();

    public Direction GetMovingDirection() { return movingDirection; }
    public float GetMovingDirectionFloat() { return (float)movingDirection; }

    public void FlipDirection()
    {
        if (movingDirection == Direction.Right) SetMovingDirection(Direction.Left);
        else SetMovingDirection(Direction.Right);
    }

    public virtual void SetMovingDirection(Direction dir)
    {
        movingDirection = dir;
        if (objectDirection != movingDirection)
        {
            FlipObjectSprite();
            FlipAdditionalScaleChangeObjects();
            objectDirection = dir;
        }
    }

    private void FlipObjectSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    protected virtual void FlipAdditionalScaleChangeObjects()
    {
        // TODO: childDirectionalObjects의 Type에 따라 최적화된 Flip을 하도록 수정해야 함
        foreach (GameObject obj in childDirectionalObjects)
        {
            Util.FlipDirectionX(obj);
        }
    }

    public Direction GetRelativeDirectionToTarget(Vector3 targetPosition)
    {
        return gameObject.transform.position.x > targetPosition.x ? Direction.Right : Direction.Left;
    }
}
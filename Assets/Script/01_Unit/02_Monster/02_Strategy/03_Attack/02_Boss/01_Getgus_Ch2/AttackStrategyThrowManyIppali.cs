using UnityEngine;

public class AttackStrategyThrowManyIppali : AttackStrategyThrowMany
{
    public AttackStrategyThrowManyIppali(float throwSpeed, float maxHeight, int throwCountMax, float throwInterval, PoolTag poolTag, string animTrigger) : base(throwSpeed, maxHeight, throwCountMax, throwInterval, poolTag, animTrigger)
    {
        this.throwCountMax = throwCountMax;
        this.throwInterval = throwInterval;
        this.poolTag = poolTag;
    }
    protected override void SetTargetPosition()
    {
        Vector3 startPos = monster.GetMiddlePos();

        if (throwCountMax == 1) startPos.x += (Random.value > 0.5f) ? 5f : -5f;

        RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.down, Mathf.Infinity, LayerMask.GetMask(LayerConstant.Tile));

        targetPosition = hit.point;
    }
}
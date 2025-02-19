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
        float offsetX = 5f + RandomSystem.RandomFloat(2f, -2f);
        Vector3 startPos = monster.attackCollider.transform.position + new Vector3(offsetX, 0f, 0f); // TODO: 임시 Offset
        if (RandomSystem.RandomBool(66.7f)) startPos.x += RandomSystem.RandomBool(50) ? 7f : -7f;
        RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.down, Mathf.Infinity, LayerMask.GetMask(LayerConstant.Tile));

        targetPosition = hit.point;
    }
}
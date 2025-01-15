using System.Collections;
using UnityEngine;

public class AttackStrategyRush : AttackStrategy
{
    protected float rushSpeed;
    protected Direction RushDirection;
    protected float dashDuration;
    protected Coroutine rushCoroutine;
    protected LayerMask GroundLayer;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        SetRushDirection();
        GroundLayer = LayerMask.GetMask(LayerConstant.Tile);
    }
    protected override void SkillMethod()
    {
        Rush();
        base.monoBehaviour.StartCoroutine(RushCoroutine());
    }

    protected IEnumerator RushCoroutine()
    {
    float elapsedTime = 0f;
    while (elapsedTime < dashDuration)
    {
        elapsedTime += Time.deltaTime;
        
        Rush();
        yield return null;
    }
    }

    protected virtual void SetRushDirection()
    {
        if(Player.Instance.transform.position.x > monster.transform.position.x) RushDirection = Direction.Right;
        else RushDirection = Direction.Left;
    }
    protected virtual void Rush()
    {
        CheckWall();
        CheckGround();
        monster.gameObject.transform.position += new Vector3((int)RushDirection * rushSpeed * Time.deltaTime, 0, 0);
    }
    protected virtual void CheckWall()
    {
        Vector3 start = GetMonsterMiddlePos() + new Vector3(GetMonsterSize().x / 2, 0, 0) * (int)RushDirection;
        Vector3 dir = Vector3.right * (int)RushDirection;

        RaycastHit2D rayHit = Physics2D.Raycast(start, dir, 0.1f, LayerMask.GetMask(LayerConstant.Tile));
        //Debug.DrawLine(start, start + dir * 0.1f, Color.red);
        if (rayHit.collider != null && rayHit.collider.CompareTag("Base")) 
        {
            ChangeDir();
        }
    }

    protected Vector3 GetRayStartPoint()
    {
        Vector3 offset = new Vector3((int)RushDirection, 0, 0);
        return GetMonsterPos() + offset;
    }

    protected void CheckGround()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(GetRayStartPoint(), Vector3.down, 0.1f, GroundLayer);

        if (rayHit.collider == null) 
        {
            ChangeDir();
        }
    }

    protected void ChangeDir()
    {
        RushDirection = (Direction)(-1 * (int)RushDirection);
    }
}

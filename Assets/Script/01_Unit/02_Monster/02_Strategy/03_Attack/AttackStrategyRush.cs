using System.Collections;
using UnityEngine;

public class AttackStrategyRush : AttackStrategy
{
    protected float rushSpeed;
    protected Direction RushDirection;
    protected float dashDuration;
    protected Coroutine rushCoroutine;
    protected LayerMask GroundLayer;
    protected GameObject prefab;
    protected GameObject obj;
    protected MonsterAttackCollider monsterAttackCollider;
    protected bool isStop = false;

    public AttackStrategyRush(float rushSpeed, float dashDuration)
    {
        this.rushSpeed = rushSpeed;
        this.dashDuration = dashDuration;
    }

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        GroundLayer = LayerMask.GetMask(LayerConstant.Tile);
        prefab = Resources.Load(PrefabRouter.AttackPrefab[monster.monsterName]) as GameObject;
    }

    protected override void SkillMethod()
    {
        obj = GameObject.Instantiate(prefab);
        monoBehaviour.StartCoroutine(RushCoroutine());
    }

    protected IEnumerator RushCoroutine()
    {
        float elapsedTime = 0f;
        SetRushDirection();

        while (elapsedTime < dashDuration)
        {
            elapsedTime += Time.deltaTime;

            Rush();
            yield return null;
        }

        monster.PlayAnimation("attackEnd");
        Object.Destroy(obj);
    }

    protected virtual void SetRushDirection()
    {
        if (Player.Instance.transform.position.x > monster.transform.position.x) RushDirection = Direction.Right;
        else RushDirection = Direction.Left;
    }

    protected virtual void Rush()
    {
        if (isStop) return;

        monster.SetMovingDirection(RushDirection);

        CheckWall();
        CheckGround();

        monster.gameObject.transform.position += new Vector3((int)RushDirection * rushSpeed * Time.deltaTime, 0, 0);
        obj.transform.position = monster.gameObject.transform.position + new Vector3(0, 2.3f, 0); ;
    }

    protected virtual void CheckWall()
    {
        Vector3 start = GetMonsterMiddlePos() + new Vector3(GetMonsterSize().x / 2, 0, 0) * (int)RushDirection;
        Vector3 dir = Vector3.right * (int)RushDirection;

        RaycastHit2D rayHit = Physics2D.Raycast(start, dir, 0.1f, LayerMask.GetMask(LayerConstant.Tile));
        //Debug.DrawLine(start, start + dir * 0.1f, Color.red);
        if (rayHit.collider != null && rayHit.collider.CompareTag("Base"))
        {
            monoBehaviour.StartCoroutine(ChangeDir());
        }
    }

    protected Vector3 GetRayStartPoint()
    {
        return GetMonsterBottomPos() + new Vector3((int)RushDirection * GetMonsterSize().x / 2, 0, 0);
    }

    protected void CheckGround()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(GetRayStartPoint(), Vector3.down, 0.1f, GroundLayer);
        //Debug.DrawLine(GetRayStartPoint(), GetMonsterPos() + offset + Vector3.down * 0.1f, Color.red);

        if (rayHit.collider == null) monoBehaviour.StartCoroutine(ChangeDir());
    }

    protected IEnumerator ChangeDir()
    {
        RushDirection = (Direction)(-1 * (int)RushDirection);
        monster.PlayAnimation("turn");
        isStop = true;
        yield return new WaitForSeconds(0.33f);
        isStop = false;
    }
}

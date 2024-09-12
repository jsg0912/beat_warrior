using System.Collections;
using UnityEngine;

public class RangedMonster : Pattern
{
    public float shootCoolTime;
    private float shootCoolMaxTime;
    private float arrowSpeed;
    private float FindRange;
    private LayerMask ObjectLayer;
    public bool alert;
    public bool isMoveable;
    public bool shootAble; //TODO: shoot안쏘고 움직이기만 하는 Monster 재현을 위해 빠르게 추가한 방식으로 임시적인 것 - 신동환, 20240904

    private GameObject ArrowPrefab;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        arrowSpeed = 5f;
        FindRange = 5f;
        ArrowPrefab = Resources.Load("Prefab/Arrow") as GameObject;
        ObjectLayer = LayerMask.GetMask("Player");
        alert = false;
        isMoveable = true;
        shootAble = false;
    }
    public override void PlayPattern()
    {
        CheckCoolTime();
        CheckCollision();
        // Move();
    }

    protected bool GetIsMoveable()
    {
        return isMoveable;
    }

    private void CheckCoolTime()
    {
        if (shootCoolTime >= 0) shootCoolTime -= Time.deltaTime;

    }

    private void CheckCollision()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(monster.gameObject.transform.position, FindRange, ObjectLayer);

        foreach (Collider2D collider in collider2Ds)
        {
            monster.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(Shoot(collider));
            alert = true;

            if (isMoveable == true)
            {
                if (collider.transform.position.x > monster.gameObject.transform.position.x) monster.SetDirection(Direction.Right);
                else monster.SetDirection(Direction.Left);
            }

        }
        if (alert == true)
        {
            if (Physics2D.OverlapCircle(monster.gameObject.transform.position, FindRange, ObjectLayer) == false)
            {
                alert = false;
                isMoveable = true;
            }
        }
        RaycastHit2D rayHit = Physics2D.Raycast(monster.gameObject.transform.position + new Vector3(monster.GetDirection(), 0, 0), Vector3.down, 1, LayerMask.GetMask("Tile"));
        if (rayHit.collider == null)
        {
            if (alert == true) isMoveable = false;
            else monster.ChangeDirection();
        }
    }

    private IEnumerator Shoot(Collider2D player)
    {
        if (shootCoolTime > 0 || shootAble == false) yield break;
        isMoveable = false;
        shootCoolTime = shootCoolMaxTime;

        monster.SetAnimation("Attack");

        yield return new WaitForSeconds(0.55f);

        Vector3 start = monster.gameObject.transform.position + new Vector3(0, 0.8f, 0);
        Vector3 end = player.transform.position;
        Vector3 direction = end - start;


        GameObject Arrow = GameObject.Instantiate(ArrowPrefab, start, Quaternion.identity);

        Arrow.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector3.up, direction.normalized).eulerAngles.z);
        Arrow.GetComponent<Rigidbody2D>().velocity = direction.normalized * arrowSpeed;

        isMoveable = true;
    }

    public RangedMonster Copy()
    {
        return new RangedMonster();
    }
}

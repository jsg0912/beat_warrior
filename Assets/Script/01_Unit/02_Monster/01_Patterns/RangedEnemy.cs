using System.Collections;
using UnityEngine;

public class RangedEnemy : Pattern
{
    public float shootCoolTime;
    private float shootCoolMaxTime;
    private float arrowSpeed;
    private float FindRange;
    private LayerMask ObjectLayer;
    public bool alert;
    public bool canMove;
    public bool shootAble; //TODO: shoot안쏘고 움직이기만 하는 Monster 재현을 위해 빠르게 추가한 방식으로 임시적인 것 - 신동환, 20240904
    private float tempDir;

    private GameObject ArrowPrefab;

    public override void Initialize(GameObject gameObject)
    {
        base.Initialize(gameObject);
        shootCoolMaxTime = 3f;
        shootCoolTime = 3f;
        arrowSpeed = 5f;
        FindRange = 5f;
        ArrowPrefab = Resources.Load("Prefab/Arrow") as GameObject;
        ObjectLayer = LayerMask.GetMask("Player");
        anim = gameObject.GetComponent<Animator>();
        direction = Direction.Right;
        moveSpeed = 0.5f;
        alert = false;
        canMove = false;
        shootAble = false;
        tempDir = 1;
    }
    public override void PlayPattern()
    {
        MoveAnim();
        CheckCoolTime();
        CheckCollision();
        Move();
    }

    private void MoveAnim()
    {
        if (direction == 0) anim.SetBool("isWalk", false);
        else anim.SetBool("isWalk", true);
    }

    protected override bool IsMoveable()
    {
        return canMove;
    }

    private void CheckCoolTime()
    {
        if (shootCoolTime >= 0) shootCoolTime -= Time.deltaTime;

    }

    private void CheckCollision()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(gameObject.transform.position, FindRange, ObjectLayer);

        foreach (Collider2D collider in collider2Ds)
        {
            gameObject.GetComponent<MonoBehaviour>().StartCoroutine(Shoot(collider));
            alert = true;

            if (canMove == true)
            {
                if (collider.transform.position.x > gameObject.transform.position.x) direction = Direction.Right;
                else direction = Direction.Left;
            }

        }
        if (alert == true)
        {
            if (Physics2D.OverlapCircle(gameObject.transform.position, FindRange, ObjectLayer) == false)
            {
                alert = false;
                canMove = true;
            }
        }
        RaycastHit2D rayHit = Physics2D.Raycast(gameObject.transform.position + new Vector3((int)direction, 0, 0), Vector3.down, 1, LayerMask.GetMask("Tile"));
        if (rayHit.collider == null)
        {
            if (alert == true) canMove = false;
            else direction = (Direction)(-1 * (int)direction);
        }
    }

    private IEnumerator Shoot(Collider2D player)
    {
        if (shootCoolTime > 0 || shootAble == false) yield break;
        canMove = false;
        shootCoolTime = shootCoolMaxTime;

        anim.SetTrigger("attack");

        yield return new WaitForSeconds(0.55f);

        Vector3 start = gameObject.transform.position + new Vector3(0, 0.8f, 0);
        Vector3 end = player.transform.position;
        Vector3 direction = end - start;


        GameObject Arrow = GameObject.Instantiate(ArrowPrefab, start, Quaternion.identity);

        Arrow.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector3.up, direction.normalized).eulerAngles.z);
        Arrow.GetComponent<Rigidbody2D>().velocity = direction.normalized * arrowSpeed;

        canMove = true;
    }

    public override Pattern Copy()
    {
        return new RangedEnemy();
    }
}

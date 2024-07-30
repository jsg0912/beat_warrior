using System.Collections;
using UnityEngine;

public class RangedEnemy : Pattern
{
    public float shootCoolTime;
    private float shootCoolMaxTime;
    private float moveSpeed;
    private float arrowSpeed;
    private float FindRange;
    private LayerMask ObjectLayer;
    public float direction;
    public bool alert;
    public bool stop;
    private float tempdir;

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
        direction = 1;
        moveSpeed = 0.5f;
        alert = false;
        stop = false;
        tempdir = 1;
    }
    public override void PlayPattern()
    {
        MoveAnim();
        CheckCoolTime();
        CheckCollision();
        Move();
    }

    private void Move()
    {


        if (direction != 0)
        {
            gameObject.transform.localScale = new Vector3(-1 * direction, 1, 1);
        }
        gameObject.transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
    }

    private void MoveAnim()
    {
        if (direction == 0) anim.SetBool("isWalk", false);
        else anim.SetBool("isWalk", true);
    }

    private void SetStop()
    {
        tempdir = direction;
        direction = 0;
        stop = true;
    }

    private void SetMove()
    {
        direction = tempdir;
        stop = false;
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

            if (stop == false)
            {
                if (collider.transform.position.x > gameObject.transform.position.x) direction = 1;
                else direction = -1;
            }

        }
        if (alert == true)
        {
            if (Physics2D.OverlapCircle(gameObject.transform.position, FindRange, ObjectLayer) == false)
            {
                alert = false;
                SetMove();
            }
        }
        RaycastHit2D rayHit = Physics2D.Raycast(gameObject.transform.position + new Vector3(direction, 0, 0), Vector3.down, 1, LayerMask.GetMask("Tile"));
        if (rayHit.collider == null)
        {
            if (alert == true) SetStop();
            else direction *= -1;
        }
    }

    private IEnumerator Shoot(Collider2D player)
    {
        if (shootCoolTime > 0) yield break;
        SetStop();
        shootCoolTime = shootCoolMaxTime;

        anim.SetTrigger("attack");

        yield return new WaitForSeconds(0.55f);

        Vector3 start = gameObject.transform.position + new Vector3(0, 0.8f, 0);
        Vector3 end = player.transform.position;
        Vector3 direction = end - start;


        GameObject Arrow = GameObject.Instantiate(ArrowPrefab, start, Quaternion.identity);

        Arrow.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector3.up, direction.normalized).eulerAngles.z);
        Arrow.GetComponent<Rigidbody2D>().velocity = direction.normalized * arrowSpeed;

        SetMove();

    }

    public override Pattern Copy()
    {
        return new RangedEnemy();
    }
}

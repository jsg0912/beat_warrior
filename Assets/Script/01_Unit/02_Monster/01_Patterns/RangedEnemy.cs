using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float shootCoolTime;
    private float shootCoolMaxTime;
    private float moveSpeed;
    private float arrowSpeed;
    private float FindRange;
    private LayerMask ObjectLayer;
    private Animator anim;
    public float direction;
    public bool alert;
    public bool stop;
    private float hp;
    private bool alive;
    private float tempdir;




    private GameObject ArrowPrefab;

    // Start is called before the first frame update
    void Start()
    {
        shootCoolMaxTime = 2f;
        shootCoolTime = 2f;
        arrowSpeed = 5f;
        FindRange = 5f;
        ArrowPrefab = Resources.Load("Prefab/Arrow") as GameObject;
        ObjectLayer = LayerMask.GetMask("Player");
        anim = GetComponent<Animator>();
        direction = 1;
        moveSpeed = 1f;
        alert = false;
        alive = true;
        hp = 3f;
        stop = false;
        tempdir = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if(alive)
        {
            MoveAnim();
            CheckCoolTime();
            CheckCollision();
            Move();
        }
    }

    private void Move()
    {
        
        
        if (direction != 0)
        {
            transform.localScale = new Vector3(-1 * direction, 1, 1);
        }
        transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
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
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, FindRange, ObjectLayer);

        foreach (Collider2D collider in collider2Ds)
        {

            StartCoroutine(Shoot(collider));
            alert = true;

            if(stop == false)
            {
                if (collider.transform.position.x > transform.position.x) direction = 1;
                else direction = -1;
            }

        }
        if(alert == true)
        {
            if (Physics2D.OverlapCircle(transform.position, FindRange, ObjectLayer) == false)
            {
                alert = false;
                SetMove();
            }
        }
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector3.down, 1, LayerMask.GetMask("Tile"));
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

        Vector3 start = transform.position + new Vector3(0,0.8f,0);
        Vector3 end = player.transform.position;
        Vector3 direction = end - start;
        

        GameObject Arrow = Instantiate(ArrowPrefab, start, Quaternion.identity);

        Arrow.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector3.up, direction.normalized).eulerAngles.z);
        Arrow.GetComponent<Rigidbody2D>().velocity = direction.normalized * arrowSpeed;

        SetMove();

    }


    public void RangedGetDamage()
    {
        if(hp <= 0)
        {
            anim.SetTrigger("die");
            alive = false;
            return;
        }
        anim.SetTrigger("hurt");
        hp -= 1;
    }

}

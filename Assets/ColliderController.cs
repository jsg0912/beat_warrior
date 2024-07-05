using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private CapsuleCollider2D playerCollider;
    private GameObject tile;
    private GameObject monster;

    private void Start()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();

    }


    public void PassTile()
    {
        if (tile != null)
        {
            StartCoroutine(DisableCollision());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            tile = collision.gameObject;
        }

        if (collision.gameObject.CompareTag("Monster"))
        {
            monster = collision.gameObject;
            BoxCollider2D monsterCollider = monster.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(playerCollider, monsterCollider);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            tile = null;
        }


    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D tileCollider = tile.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, tileCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, tileCollider, false);
    }
}

using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{

    private bool damaged;

    private void Start()
    {
        damaged = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.CompareTag("Player") && damaged == true)
        {
            damaged = false;
            Player.Instance.GetDamaged(1);
            StartCoroutine(InvincibleTime(1));
        }
    }

    IEnumerator InvincibleTime(int time)
    {
        yield return new WaitForSeconds(time);
        damaged = true;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 2.0f);
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * 500);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.CompareTag("Monster"))
        {
            Player.Instance.SetTarget(obj);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Remove()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.CompareTag("Player"))
        {
            Player.Instance.GetDamaged(1);
            Destroy(this.gameObject);
        }
    }
}

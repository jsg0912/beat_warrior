using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockObject : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Marker(Clone)")
        {
            Marker.Destroy(collision.gameObject);
        }
        else if(collision.gameObject.name == "Arrow(Clone)")
        {
            Arrow.Destroy(collision.gameObject);
        }
    }
}

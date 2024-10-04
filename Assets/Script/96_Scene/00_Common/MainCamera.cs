using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Camera main;
    void Start()
    {
        main = GetComponent<Camera>();
        main.cullingMask = ~(1 << LayerMask.NameToLayer("MiniMap"));
    }

}

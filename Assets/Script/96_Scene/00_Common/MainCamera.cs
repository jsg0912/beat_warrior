using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // public static MainCamera _instance;
    // public static MainCamera Instance
    // {
    //     get
    //     {
    //         if (_instance == null)
    //         {
    //             _instance = FindFirstObjectByType();
    //         }
    //         return _instance;
    //     }

    // }


    Camera main;
    void Start()
    {
        main = GetComponent<Camera>();
        main.cullingMask = ~(1 << LayerMask.NameToLayer("MiniMap"));
    }

}
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera Instance;
    public Camera main;
    void Awake()
    {
        Instance = this;
        main = GetComponent<Camera>();
    }
}
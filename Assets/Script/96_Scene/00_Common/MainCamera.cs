using UnityEngine;

public class MainCamera : SingletonObject<MainCamera>
{
    public Camera main;
    protected override void Awake()
    {
        main = GetComponent<Camera>();
        base.Awake();
    }
}
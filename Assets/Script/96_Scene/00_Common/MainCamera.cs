using UnityEngine;

public class MainCamera : SingletonObject<MainCamera>
{
    public Camera main;
    protected override void Awake()
    {
        base.Awake();
        main = GetComponent<Camera>();
    }

}
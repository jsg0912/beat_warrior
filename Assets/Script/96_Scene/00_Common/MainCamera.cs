// TODO: 어떤 Scene을 만들든 Main Camera에 MainCamera Script  자동으로 들어가도록 방식 개선 - 전체회의, 20241010
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
using UnityEngine;

public class CursorClickChangeController : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D clickCursor;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭
        {
            Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0)) // 마우스 클릭 해제
        {
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}

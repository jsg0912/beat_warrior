using UnityEngine;

public class CursorHoverController : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D hoverCursor;
    private bool isHovering = false;

    void Update()
    {
        if (isHovering)
            Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseEnter()
    {
        isHovering = true;
    }

    void OnMouseExit()
    {
        isHovering = false;
    }
}

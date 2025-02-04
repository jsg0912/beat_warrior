using System;
using UnityEngine;

[Serializable]
public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D TitleCursor;
    [SerializeField] private Texture2D inGameCursor;
    [SerializeField] private Texture2D zoomInCursor;

    public void SetTitleCursor()
    {
        if (TitleCursor != null)
        {
            Cursor.SetCursor(TitleCursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void SetInGameCursor()
    {
        Cursor.SetCursor(inGameCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetZoomInCursor()
    {
        Cursor.SetCursor(zoomInCursor, Vector2.zero, CursorMode.Auto);
    }
}
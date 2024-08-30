using UnityEngine;

public static class DebugConsole
{
    public static void Log(string message)
    {
        Debug.Log(message);
    }

    public static void Log(Vector2 vector)
    {
        Debug.Log(vector);
    }
}
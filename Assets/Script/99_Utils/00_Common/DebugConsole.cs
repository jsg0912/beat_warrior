using System.Collections.Generic;
using UnityEngine;

public static class DebugConsole
{
    public static void Log(string message)
    {
        Debug.Log(message);
    }

    public static void Log(float message)
    {
        Debug.Log(message);
    }

    public static void Log(Vector2 vector)
    {
        Debug.Log(vector);
    }

    public static void Log(List<Skill> skillList)
    {
        foreach (Skill skill in skillList)
        {
            Log(skill.skillName.ToString());
        }
    }
}
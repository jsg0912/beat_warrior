using System;

public static class Util
{
    public static T ParseEnumFromString<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value);
    }

}
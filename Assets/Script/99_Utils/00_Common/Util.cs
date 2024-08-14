using System.Collections.Generic;

public static class Util
{
    public static bool isHasKey<K, V>(Dictionary<K, V> dict, K key)
    {
        foreach (KeyValuePair<K, V> kvp in dict)
        {
            if (kvp.Key.Equals(key))
            {
                return true;
            }
        }
        return false;
    }
}
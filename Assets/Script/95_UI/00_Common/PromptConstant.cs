public enum PromptType
{
    Sign,
    Altar
}

public static class PromptMessageGenerator
{
    public static string GeneratePromptMessage(PlayerAction action)
    {
        return $"Press [{KeySetting.keys[action]}]";
    }
}
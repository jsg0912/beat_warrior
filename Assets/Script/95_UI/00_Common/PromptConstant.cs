public enum PromptType
{
    Sign,
    Altar
}

public static class PromptMessageGenerator
{
    public static string GeneratePromptMessage()
    {
        return $"Press [{KeySetting.keys[PlayerAction.Interaction]}]";
    }
}
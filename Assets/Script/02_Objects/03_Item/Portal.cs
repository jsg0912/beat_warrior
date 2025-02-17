public class Portal : ObjectWithInteractionPrompt
{
    // Portal에 여러번 상호작용 하면 큰일 남.
    bool isUsed = false; // TODO: 더 좋은 방식이 있을지 고민 필요. - SDH, 20250218

    public override bool StartInteraction()
    {
        if (isUsed) return false;

        bool success = ChapterManager.Instance.MoveToNextStage();
        if (success) isUsed = true;
        else SystemMessageUIManager.Instance.TurnOnSystemMassageUI(SystemMessageType.EnemyRemaining);
        return success;
    }
}
public class Portal : ObjectWithInteractionPrompt
{
    public override bool StartInteraction()
    {
        bool success = ChapterManager.Instance.MoveToNextStage();
        if (success == false)
        {
            SystemMessageUIManager.Instance.TurnOnSystemMassageUI(SystemMessageType.EnemyRemaining);
        }
        return success;
    }
}
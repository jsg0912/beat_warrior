public class Portal : ObjectWithInteractionPrompt
{
    public override void StartInteraction()
    {
        bool success = ChapterManager.Instance.MoveToNextStage();
        if(success == false)
        {
            SystemMassageUIManager.Instance.TurnOnSystemMassageUI(SyetemMassageType.EnemyRemaining);
        }
    }
}
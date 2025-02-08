public class Portal : ObjectWithInteractionPrompt
{
    public override void StartInteraction()
    {
        bool success = ChapterManager.Instance.MoveToNextStage();
    }
}
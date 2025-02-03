public class Portal : ObjectWithInteractionPrompt
{
    public override void StartInteraction()
    {
        ChapterManager.Instance.MoveToNextStage();
    }
}
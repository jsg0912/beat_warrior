public class Altar : ObjectWithInteractionPrompt
{
    public override void StartInteraction()
    {
        UIManager.Instance.TurnOnAltarPopup();
    }
}
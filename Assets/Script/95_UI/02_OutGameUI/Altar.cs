public class Altar : ObjectWithInteractionPrompt
{
    public override bool StartInteraction()
    {
        UIManager.Instance.TurnOnAltarPopup();
        return true;
    }
}
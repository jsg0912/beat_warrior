public class Monster2Pattern : Pattern
{
    public Monster2Pattern()
    {
        Recognize = new RecognizeRanged();
        MoveNormal = new MoveNormal();
        MoveChase = new MoveChase();
    }
}

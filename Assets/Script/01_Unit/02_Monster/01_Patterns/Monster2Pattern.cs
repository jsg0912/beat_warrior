public class Monster2Pattern : Pattern
{
    public Monster2Pattern()
    {
        Recognize = new RecognizeRangedMonster();
        MoveNormal = new MoveNormal();
        MoveChase = new MoveChase();
    }
}

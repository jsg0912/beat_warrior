public class Monster2Pattern : Pattern
{
    public Monster2Pattern()
    {
        Recognize = new RecognizeRangedMonster();
        Move = new MoveStrategy();
        //Attack.Initialize(monster);
    }
}

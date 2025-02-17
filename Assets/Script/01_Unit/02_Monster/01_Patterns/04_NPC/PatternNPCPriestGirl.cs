public class PatternNPCPriestGirl : PatternNPC
{
    public PatternNPCPriestGirl()
    {
        MoveBasic = new MoveStrategyFix();
        MoveChase = new MoveStrategyChase();
        Recognize = new RecognizeStrategyRanged();
    }
}

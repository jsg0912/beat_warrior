public class IppaliPattern : Pattern
{
    public IppaliPattern()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyMelee(); // TODO: 인식범위와 공격범위는 다른 건데, 지금 인식범위로 공격범위를 정하고 있음 수정 필요 - SDH, 2025.01.06
    }
}
using UnityEngine;

public class RecognizeStrategyMelee : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        recognizeRange = MonsterConstant.MeleeRecognizeRange;
    }

    protected override void CheckTarget()
    {
        Vector3 offset = new Vector3(0, 1.0f, 0);
        RaycastHit2D rayHit = Physics2D.Raycast(GetMonsterPos() + offset, Vector3.right * GetDirection(), 1.5f, TargetLayer);
    }
}

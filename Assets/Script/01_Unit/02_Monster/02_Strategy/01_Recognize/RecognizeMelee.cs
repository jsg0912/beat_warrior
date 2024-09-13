using UnityEngine;

public class RecognizeMelee : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        recognizeRange = MonsterConstant.MeleeRecognizeRange;
    }

    protected override void CheckTarget()
    {

    }
}

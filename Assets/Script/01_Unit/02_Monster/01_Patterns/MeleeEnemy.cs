using UnityEngine;

public class MeleeEnemy : Pattern
{

    public override void Initialize(GameObject gameObject)
    {
        base.Initialize(gameObject);
    }
    public override void PlayPattern()
    {

    }
    public override Pattern Copy()
    {
        return new MeleeEnemy();
    }
}

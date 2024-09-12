using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStrategyPattern
{
    protected GameObject gameObject;
    protected Monster monster;

    protected RecognizePattern Recognize;
    protected MovePattern Move;
    protected AttackPattern Attack;

    public void Initialize()
    {
        Recognize.Initialize(gameObject);
        Move.Initialize(gameObject);
        Attack.Initialize(gameObject);
    }

    public void Play()
    {
        Recognize.PlayPattern();
        Move.PlayPattern();
        Attack.PlayPattern();
    }
}

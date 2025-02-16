using UnityEngine;

public class PortalWithBench : Portal
{
    [SerializeField] private GameObject bench;
    [SerializeField] private GameObject rest;
    [SerializeField] private GameObject fire;

    public override bool StartInteraction()
    {
        bool success = base.StartInteraction();
        if (success)
        {
            if (RandomSystem.RandomBool(50.0f))
            {
                Player.Instance.transform.position = bench.transform.position;
                Player.Instance.SetAnimTrigger(PlayerConstant.Rest1AnimTrigger);
            }
            else
            {
                Player.Instance.transform.position = rest.transform.position;
                Player.Instance.SetAnimTrigger(PlayerConstant.Rest2AnimTrigger);
                Player.Instance.SetMovingDirection(Direction.Left);
            }
        }
        return success;
    }
}
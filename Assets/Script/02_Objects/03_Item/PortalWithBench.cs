using UnityEngine;

public class PortalWithBench : Portal
{
    [SerializeField] private GameObject bench;

    public override bool StartInteraction()
    {
        bool success = base.StartInteraction();
        if (success)
        {
            Player.Instance.transform.position = bench.transform.position;
            Player.Instance.SetAnimTrigger(PlayerConstant.Rest1AnimTrigger);
        }
        return success;
    }
}
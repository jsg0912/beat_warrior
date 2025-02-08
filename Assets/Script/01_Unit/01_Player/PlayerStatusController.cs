using UnityEngine;

public class PlayerStatusController : StateMachineBehaviour
{
    private Player player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null) player = Player.Instance;

        if (stateInfo.IsName("DashCharge") || stateInfo.IsName("Dash")) player.SetStatus(PlayerStatus.Dash);
        if (stateInfo.IsName("Idle")) player.SetStatus(PlayerStatus.Normal);
    }
}

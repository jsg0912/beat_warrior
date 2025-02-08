using UnityEngine;

public class PlayerStatusController : StateMachineBehaviour
{
    private Player _player;

    private Player player
    {
        get
        {
            if (_player == null) _player = Player.Instance;
            return _player;
        }
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName(PlayerAnimation.Dash) || stateInfo.IsName(PlayerAnimation.DashCharge)) player.SetStatus(PlayerStatus.Dash);
        if (stateInfo.IsName(PlayerAnimation.Idle)) player.SetStatus(PlayerStatus.Normal);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}

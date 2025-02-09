using UnityEngine;

public class PlayerGhostController
{
    private Timer timer = new Timer();
    private GameObject GhostPrefab;

    public PlayerGhostController()
    {
        timer.Initialize(PlayerSkillConstant.ghostDelayTimeMax);
        GhostPrefab = Resources.Load(PrefabRouter.GhostPrefab) as GameObject;
    }

    public void TryMakeGhost(Direction direction)
    {
        if (timer.Tick()) return;

        GameObject ghostObject = GameObject.Instantiate(GhostPrefab, Player.Instance.transform.position, Quaternion.identity);
        Ghost ghost = ghostObject.GetComponent<Ghost>();
        if (direction == Direction.Left)
        {
            ghost.FlipSprite();
        }
        ghost.ChangeSprite(Player.Instance.sprite);
        timer.Initialize(PlayerSkillConstant.ghostDelayTimeMax);
        GameObject.Destroy(ghostObject, 1.0f);
    }
}
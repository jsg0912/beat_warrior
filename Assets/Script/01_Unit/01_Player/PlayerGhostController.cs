using UnityEngine;
using System.Collections;

public class PlayerGhostController : SingletonObject<PlayerGhostController>
{
    private Timer timer = new Timer();

    public void Start()
    {
        timer.Initialize(PlayerSkillConstant.ghostDelayTimeMax);
    }

    public void TryMakeGhost(Direction direction)
    {
        if (timer.Tick()) return;

        GameObject ghostObject = MyPooler.ObjectPooler.Instance.GetFromPool(PoolTag.Ghost, Player.Instance.transform.position, Quaternion.identity);
        Ghost ghost = ghostObject.GetComponent<Ghost>();
        if (direction == Direction.Left)
        {
            ghost.FlipSprite();
        }
        ghost.ChangeSprite(Player.Instance.sprite);
        timer.Initialize(PlayerSkillConstant.ghostDelayTimeMax);
        StartCoroutine(DestroyGhost(ghostObject, 1.0f));
    }

    public IEnumerator DestroyGhost(GameObject obj, float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        MyPooler.ObjectPooler.Instance.ReturnToPool(PoolTag.Ghost, obj);
    }
}
using UnityEngine;

public class TracePlayer : MonoBehaviour
{
    private Transform target;

    private void LateUpdate()
    {
        if (target == null)
        {
            target = Player.Instance.transform;
        }
        TraceToPlayer();
    }

    private void TraceToPlayer()
    {
        transform.position = new Vector3(target.position.x, target.position.y + 0.5f, 0);
    }
}
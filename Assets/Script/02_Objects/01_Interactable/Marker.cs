using UnityEngine;

public class Marker : MonoBehaviour
{
    private void Start()
    {
        DestroyMarker(PlayerSkillConstant.markerDuration);
    }

    public void SetVelocity(Vector2 start, Vector2 end)
    {
        Vector2 direction = (end - start).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * PlayerSkillConstant.markerSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = Util.GetMonsterGameObject(collision);

        if (obj != null && obj.CompareTag(TagConstant.Monster) && obj.GetComponent<Monster>().GetIsAlive())
        {
            Player.Instance.SetTarget(obj);
            obj.GetComponent<Monster>().SetTarget();
            PlayerUIManager.Instance.SwapMarkAndDash(false);
            Destroy(gameObject);
        }
    }

    public void DestroyMarker(float timer = 0.0f)
    {
        Destroy(gameObject, timer);
    }
}

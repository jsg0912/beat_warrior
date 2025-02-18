using UnityEngine;

public class MonsterBodyCollider : MonoBehaviour
{
    [SerializeField] public Monster monster;
    [SerializeField] private Collider2D colliderObj;
    private BoxCollider2D boxCollider;
    private PolygonCollider2D polygonCollider;
    private Vector2 size;

    public void Start()
    {
        boxCollider = colliderObj.GetComponent<BoxCollider2D>();
        polygonCollider = colliderObj.GetComponent<PolygonCollider2D>();

        if (boxCollider != null) size = ColliderCalculator.GetSizeBoxCollider2D(boxCollider);
        else if (polygonCollider != null) size = ColliderCalculator.GetSizePolygonCollider2D(polygonCollider);
        else size = Vector2.zero;
    }

    public void TryFlipPolygonCollider()
    {
        Util.FlipLocalScaleX(polygonCollider);
    }

    public Vector2 GetSize()
    {
        return size;
    }

    public Vector3 GetMiddlePos()
    {
        if (boxCollider != null) return ColliderCalculator.GetMiddlePosBoxCollider2D(boxCollider);
        else if (polygonCollider != null) return ColliderCalculator.GetMiddlePosPolygonCollider2D(polygonCollider);
        else return Vector2.zero;
    }

    public Vector3 GetBottomPos()
    {
        if (boxCollider != null) return ColliderCalculator.GetBottomPosBoxCollider2D(boxCollider);
        else if (polygonCollider != null) return ColliderCalculator.GetBottomPosPolygonCollider2D(polygonCollider);
        else return Vector3.zero;
    }

    public Collider2D GetCollider()
    {
        if (boxCollider != null) return boxCollider;
        else if (polygonCollider != null) return polygonCollider;
        else return null;
    }
}
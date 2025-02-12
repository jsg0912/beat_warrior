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

        if (boxCollider != null) size = Util.GetSizeBoxCollider2D(boxCollider);
        else if (polygonCollider != null) size = Util.GetSizePolygonCollider2D(polygonCollider);
        else size = Vector2.zero;
    }

    public void TryFlipPolygonCollider()
    {
        Util.FlipLocalScaleX(polygonCollider);
    }

    public Vector2 GetSize()
    {
        if (boxCollider != null) return Util.GetSizeBoxCollider2D(boxCollider);
        else if (polygonCollider != null) return Util.GetSizePolygonCollider2D(polygonCollider);
        else return Vector2.zero;
    }

    public Vector3 GetMiddlePos()
    {
        return size;
    }

    public Vector3 GetBottomPos()
    {
        if (boxCollider != null) return Util.GetBottomPosBoxCollider2D(boxCollider);
        else if (polygonCollider != null) return Util.GetBottomPosPolygonCollider2D(polygonCollider);
        else return Vector3.zero;
    }
}
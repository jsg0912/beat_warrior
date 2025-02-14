using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Util
{
    // Vector 활용에 있어서 오차범위용
    public static float OffsetX = 0.05f;
    public static float OffsetY = 0.05f;

    public static T ParseEnumFromString<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value);
    }

    public static bool SetActive(Button button, bool isOn)
    {
        return SetActive(button.gameObject, isOn);
    }

    public static bool SetActive(GameObject gameObject, bool isOn)
    {
        if (gameObject != null && gameObject.activeSelf != isOn)
        {
            gameObject.SetActive(isOn);
            return true;
        }
        return false;
    }

    public static Vector2 GetSizeBoxCollider2D(BoxCollider2D boxCollider2D)
    {
        return boxCollider2D.size;
    }

    public static Vector3 GetMiddlePosBoxCollider2D(BoxCollider2D boxCollider2D)
    {
        return boxCollider2D.bounds.center;
    }

    public static Vector3 GetBottomPosBoxCollider2D(BoxCollider2D boxCollider2D)
    {
        return boxCollider2D.bounds.center - new Vector3(0, boxCollider2D.size.y / 2, 0);
    }

    public static Vector2 GetSizePolygonCollider2D(PolygonCollider2D polygonCollider2D)
    {
        // 결과 반환
        return new Vector2(polygonCollider2D.bounds.extents.x * 2, polygonCollider2D.bounds.extents.y * 2);
    }

    public static Vector3 GetMiddlePosPolygonCollider2D(PolygonCollider2D polygonCollider2D)
    {
        return polygonCollider2D.bounds.center;
    }

    public static Vector3 GetBottomPosPolygonCollider2D(PolygonCollider2D polygonCollider2D)
    {
        return polygonCollider2D.bounds.center - new Vector3(0, polygonCollider2D.bounds.extents.y, 0);
    }

    public static void FlipDirectionX(GameObject gameObject)
    {
        if (gameObject.GetComponent<PolygonCollider2D>() != null)
            FlipLocalScaleX(gameObject.GetComponent<PolygonCollider2D>());
        else
        {
            FlipDirectionX(gameObject.transform);
            FlipLocalScaleX(gameObject.transform);
        }
    }

    public static void FlipDirectionX(Transform transform)
    {
        Vector3 pos = transform.localPosition;
        transform.localPosition = new Vector3(-pos.x, pos.y, pos.z);
    }

    public static void FlipLocalScaleX(GameObject gameObject)
    {
        if (gameObject.GetComponent<PolygonCollider2D>() != null)
            FlipLocalScaleX(gameObject.GetComponent<PolygonCollider2D>());
        else
            FlipLocalScaleX(gameObject.transform);
    }

    public static void FlipLocalScaleX(PolygonCollider2D polygonCollider)
    {
        if (polygonCollider != null)
        {
            Vector2[] points = polygonCollider.points;
            for (int i = 0; i < points.Length; i++)
            {
                points[i].x = -points[i].x;
            }
            polygonCollider.points = points;
        }
    }

    public static void FlipLocalScaleX(Transform transform)
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x; // x 값의 부호 반전
        transform.localScale = scale;
    }

    public static T FindComponentInHierarchy<T>(GameObject root) where T : Component
    {
        // 현재 GameObject에서 컴포넌트 찾기
        T component = root.GetComponent<T>();
        if (component != null)
        {
            return component;
        }

        // 자식 오브젝트에서 재귀적으로 탐색
        foreach (Transform child in root.transform)
        {
            component = FindComponentInHierarchy<T>(child.gameObject);
            if (component != null)
            {
                return component;
            }
        }

        return null; // 컴포넌트를 찾지 못한 경우
    }

    public static Vector2 GetLocalSize(SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            Vector2 pixelSize = spriteRenderer.sprite.rect.size; // 픽셀 크기
            float pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit; // PPU 값

            return pixelSize / pixelsPerUnit; // 로컬 크기 반환
        }

        Debug.LogError("SpriteRenderer or Sprite is null!");
        return Vector2.zero;
    }

    public static GameObject GetMonsterGameObject(Collider2D collision)
    {
        return collision.gameObject.transform.parent?.gameObject;
    }

    public static int Round(int p, int q)
    {
        return (p + q - 1) / q;
    }

    public static Vector3 GetMousePointWithPerspectiveCamera()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(mousePosition); ;
    }

    public static bool IsEditor => Application.isEditor;

    public static bool IsRootGameObject(GameObject gameObject)
    {
        return gameObject.transform.parent == null;
    }

    public static bool IsStoppedSpeed(float speed)
    {
        // Speed can be not exact zero, just check it is close to zero, - SDH, 20250208
        return -1e-4f <= speed && speed <= 1e4f;
    }

    public static IEnumerator PlayInstantEffect(GameObject effect, float duration)
    {
        Timer timer = new Timer(duration);
        if (effect != null)
        {
            SetActive(effect, true);
        }
        while (timer.Tick())
        {
            yield return null;
        }
        SetActive(effect, false);
    }

    public static void SetRotationZ(GameObject gameObject, float rotationZRatio)
    {
        if (rotationZRatio > 1.0f) rotationZRatio = 1.0f;
        else if (rotationZRatio < 0.0f) rotationZRatio = 0.0f;
        if (gameObject != null) gameObject.transform.rotation = Quaternion.Euler(0, 0, rotationZRatio * 360);
    }

    public static void ResetRotationZ(GameObject gameObject)
    {
        if (gameObject != null) gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public static bool RandomBool(float truePercentage)
    {
        return UnityEngine.Random.Range(0, 100) < truePercentage;
    }

    public static void RotateObjectForwardingDirection(GameObject gameObject, Vector3 direction, bool hasTopDownStructure)
    {
        if (gameObject == null) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (hasTopDownStructure)
        {
            var localScale = gameObject.transform.localScale;
            if (direction.x < 0) // 위아래가 뒤집히면 안되는 경우 왼쪽으로 날아갈때 위 아래를 뒤집어 주어야 함.
                gameObject.transform.localScale = new Vector3(localScale.x, -localScale.y, localScale.z);
            else
                gameObject.transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
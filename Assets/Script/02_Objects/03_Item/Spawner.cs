using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void MovePlayerToSpawner()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.transform.position = transform.position;
        }
    }
}

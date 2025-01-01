using UnityEngine;
using UnityEngine.SceneManagement;
public class Spawner : MonoBehaviour
{
    private void OnEnable()
    {
        Player.OnPlayerCreated += MovePlayerToSpawner;
    }

    private void OnDisable()
    {
        Player.OnPlayerCreated -= MovePlayerToSpawner;
    }

    private void OnSceneLoaded()
    {

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

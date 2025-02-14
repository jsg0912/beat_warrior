using UnityEngine;
using UnityEngine.SceneManagement;
public class Spawner : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        MovePlayerToSpawner();
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

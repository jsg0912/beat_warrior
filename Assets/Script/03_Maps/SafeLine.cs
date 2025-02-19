using UnityEngine;

// TODO: 플레이어가 벽을 뚫는 버그 때문에 임시로 만들어 놓은 Class로 해당 라인에 플레이어가 닿을 경우, Spawner로 Spawn해서 게임을 이어할 수 있게 함
public class SafeLine : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    public void Start()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawner.MovePlayerToSpawner();
        }
    }
}
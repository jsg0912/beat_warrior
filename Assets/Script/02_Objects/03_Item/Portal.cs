using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SceneController.Instance.ChangeScene((SceneName)(SceneController.Instance.CurrentScene + 1));
            }
        }
    }
}

using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeySetting.keys[Action.Interaction]))
            {
                SceneController.Instance.ChangeScene((SceneName)(SceneController.Instance.CurrentScene + 1));
            }
        }
    }
}

using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.P)) // TODO: KeyCode가 안겹치도록 관리해야됨, KeySettingButton or KeyManager 등을 거치도록 - 신동환, 20241010
            {
                SceneController.Instance.ChangeScene((SceneName)(SceneController.Instance.CurrentScene + 1));
            }
        }
    }
}

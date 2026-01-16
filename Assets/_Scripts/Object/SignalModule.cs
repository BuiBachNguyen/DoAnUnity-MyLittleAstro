using UnityEngine;
using UnityEngine.SceneManagement;

public class SignalModule : MonoBehaviour
{
    bool isActivated = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            if (isActivated == true) return;
            isActivated = true;
            Animator anim = GetComponent<Animator>();
            if (anim == null) return;
            anim.SetTrigger("Triggered");
        }
    }

    void ActivePanel()
    {
        SceneMng.Instance.NextLevel();
    }

}

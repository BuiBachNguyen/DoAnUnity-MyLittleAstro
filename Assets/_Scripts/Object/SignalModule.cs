using UnityEngine;
using UnityEngine.SceneManagement;

public class SignalModule : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
        {
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

using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SignalModule : MonoBehaviour
{
    int EmitTimes = 3;
    float EmitDelay = 1.5f; // secs

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
            AudioManager.Instance.PlaySFX(AudioClipNames.InitModule);
        }
    }

    public void ActivePanel()
    {
        StartCoroutine(BeepAndNextLevel());
    }

    public IEnumerator BeepAndNextLevel()
    {
        int times = 0;
        while (times < EmitTimes)
        {
            AudioManager.Instance.PlaySFX(AudioClipNames.Emit);
            yield return new WaitForSeconds(EmitDelay);
            times += 1;
        }
        SceneMng.Instance.NextLevel();
    }
}

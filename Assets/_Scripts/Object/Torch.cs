using UnityEngine;
using UnityEngine.SceneManagement;

public class Torch : MonoBehaviour
{
    [SerializeField] bool isBurning;

    private void Awake()
    {
        isBurning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Tags.Player))
        {
            UnlockNewLevel();
            SceneMng.Instance.NextLevel();
        }    
    }

    void UnlockNewLevel()
    {
        int currentIndexScene = SceneManager.GetActiveScene().buildIndex;
        if (currentIndexScene >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", currentIndexScene + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) +1);
            PlayerPrefs.Save();
        }    
    }    
}

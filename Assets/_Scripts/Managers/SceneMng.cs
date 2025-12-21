using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMng : MonoBehaviour
{
    #region Singleton
    public static SceneMng Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject); 
    }
    #endregion

    public void BackToStartScene()
    {
        SceneManager.LoadScene(0);
    }    
    public void GoToSelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void LeadSceneWithIndex(int index)
    {
        Debug.Log("Load with index" + index);
        SceneManager.LoadScene(index);
    }    
    public void NextLevel()
    {
        Debug.Log("next of" + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

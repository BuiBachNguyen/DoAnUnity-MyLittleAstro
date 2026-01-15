using System;
using Unity.Jobs;
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
    [SerializeField] GameObject ExitTransitionContainer;
    [SerializeField] ExitTransition ExitTransition;
    [SerializeField] GameObject EnterTransition;

    private void Start()
    {
        if (ExitTransitionContainer == null)
            Debug.LogError("Exit Transition is Null");
        if (EnterTransition == null)
            Debug.LogError("Enter transition is Null");

        ExitTransitionContainer.SetActive(false);
        if(EnterTransition != null)
            EnterTransition.SetActive(true);
        PlayerPrefs.DeleteKey("UnlockedLevel");
    }

    private void FixedUpdate()
    {
        // reset level
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            LoadSceneWithIndex(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void BackToStartScene()
    {
        Debug.Log("Load Start Scene" );
        ExitTransition.LevelIndex = 0;
        ExitTransitionContainer.SetActive(true);
    }    
    public void GoToSelectLevel()
    {
        Debug.Log("Load with index" + 31);
        ExitTransition.LevelIndex = 0; // 30 level. 
        ExitTransitionContainer.SetActive(true);
    }
    public void LoadSceneWithIndex(int index)
    {
        Debug.Log("Load with index" + index);
        ExitTransition.LevelIndex = index;
        ExitTransitionContainer.SetActive(true);
    }    
    public void NextLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("next of" + SceneManager.GetActiveScene().buildIndex);
        ExitTransition.LevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        ExitTransitionContainer.SetActive(true);
    }

    public void LoadLastLevelUnlocked()
    {
        Debug.Log("Lastest Level" + PlayerPrefs.GetInt("UnlockedLevel", 1));
        ExitTransition.LevelIndex = PlayerPrefs.GetInt("UnlockedLevel", 1);
        ExitTransitionContainer.SetActive(true);
    }
}

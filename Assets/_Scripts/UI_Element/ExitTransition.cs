using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTransition : MonoBehaviour
{
    [SerializeField] int levelIndex = 0;

    public int LevelIndex
    {
        get { return levelIndex; }
        set { levelIndex = value; }
    }
    public void LoadAfterAnimated()
    {
        SceneManager.LoadScene(levelIndex);
    }    
}

using UnityEngine;

public class PauseGamePanel : MonoBehaviour
{

    [Header("Reference Panel")]
    [SerializeField] GameObject OptionPanel;
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject CRT_effect;

    private void Start()
    {
        CRT_effect.SetActive(PlayerPrefs.GetInt("CRT_Effect", 1) == 1? true : false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        SwitchPanel(OptionPanel, MainPanel);
    }    
    public void ClosePanel()
    {
        Time.timeScale = 1.0f;
        SwitchPanel(MainPanel, OptionPanel);
    }

    void SwitchPanel(GameObject on, GameObject off)
    {
        on.SetActive(true);
        off.SetActive(false);
    }
    public void SelectLevelButton()
    {
        Time.timeScale = 1.0f;
        SceneMng.Instance.GoToSelectLevel();
    }    
    public void StartSceneButton()
    {
        Time.timeScale = 1.0f;
        SceneMng.Instance.BackToStartScene();
    }
    public void PlayButtonSound()
    {
        AudioManager.Instance.PlaySFX(AudioClipNames.UIButton);
    }    

}

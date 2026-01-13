using UnityEngine;

public class PauseGamePanel : MonoBehaviour
{

    [Header("Reference Panel")]
    [SerializeField] GameObject OptionPanel;
    [SerializeField] GameObject MainPanel;

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

}

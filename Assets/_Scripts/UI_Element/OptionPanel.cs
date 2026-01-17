using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] GameObject CRT_effect;
    [SerializeField] Toggle CRT_toggle;

    private void Start()
    {
        CRT_toggle.isOn = PlayerPrefs.GetInt("CRT_Effect", 1) == 1;
        CRT_effect.SetActive(CRT_toggle.isOn);
    }

    public void OnChangeToggle()
    {
        PlayerPrefs.SetInt("CRT_Effect", CRT_toggle.isOn ? 1 : 0);
        CRT_effect.SetActive(CRT_toggle.isOn);
        AudioManager.Instance.PlaySFX(AudioClipNames.UIButton);
    }    
}

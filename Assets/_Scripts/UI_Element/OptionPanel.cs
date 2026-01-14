using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] GameObject CRT_effect;
    [SerializeField] Toggle CRT_toggle;

    private void Start()
    {
        CRT_toggle.isOn = true;
        CRT_effect.SetActive(CRT_toggle.isOn);
    }

    public void OnChangeToggle()
    {
        CRT_effect.SetActive(CRT_toggle.isOn);
        AudioManager.Instance.PlaySFX(AudioClipNames.UIButton);
    }    
}

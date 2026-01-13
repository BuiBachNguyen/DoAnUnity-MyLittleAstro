using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettingsUI : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    //[Header("Volume Text (Optional)")]
    //[SerializeField] private TextMeshProUGUI bgmVolumeText;
    //[SerializeField] private TextMeshProUGUI sfxVolumeText;

    [Header("Toggle Buttons (Optional)")]
    [SerializeField] private Toggle bgmToggle;
    [SerializeField] private Toggle sfxToggle;

    [Header("Test SFX Button (Optional)")]
    [SerializeField] private Button testSFXButton;

    private void Start()
    {
        InitializeSliders();
        SetupListeners();
    }

    private void InitializeSliders()
    {
        if (bgmSlider != null)
        {
            bgmSlider.value = AudioManager.Instance.GetBGMVolume();
            //UpdateVolumeText(bgmVolumeText, bgmSlider.value);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = AudioManager.Instance.GetSFXVolume();
            //UpdateVolumeText(sfxVolumeText, sfxSlider.value);
        }

        if (bgmToggle != null)
        {
            bgmToggle.isOn = AudioManager.Instance.GetBGMVolume() > 0;
        }

        if (sfxToggle != null)
        {
            sfxToggle.isOn = AudioManager.Instance.GetSFXVolume() > 0;
        }
    }

    private void SetupListeners()
    {
        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }

        if (bgmToggle != null)
        {
            bgmToggle.onValueChanged.AddListener(OnBGMToggleChanged);
        }

        if (sfxToggle != null)
        {
            sfxToggle.onValueChanged.AddListener(OnSFXToggleChanged);
        }

        if (testSFXButton != null)
        {
            testSFXButton.onClick.AddListener(OnTestSFXClicked);
        }
    }

    private void OnBGMVolumeChanged(float value)
    {
        AudioManager.Instance.SetBGMVolume(value);
        //UpdateVolumeText(bgmVolumeText, value);

        if (bgmToggle != null && bgmToggle.isOn != (value > 0))
        {
            bgmToggle.SetIsOnWithoutNotify(value > 0);
        }
    }

    private void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
        //UpdateVolumeText(sfxVolumeText, value);

        if (sfxToggle != null && sfxToggle.isOn != (value > 0))
        {
            sfxToggle.SetIsOnWithoutNotify(value > 0);
        }

        if (value > 0)
        {
            if (AudioManager.Instance.IsSFXEnd())
            {
                AudioManager.Instance.PlaySFX(9);
            }
        }
    }

    private void OnBGMToggleChanged(bool isOn)
    {
        if (isOn)
        {
            float previousValue = bgmSlider != null ? bgmSlider.value : 0.5f;
            if (previousValue <= 0) previousValue = 0.5f;

            AudioManager.Instance.SetBGMVolume(previousValue);
            if (bgmSlider != null)
            {
                bgmSlider.SetValueWithoutNotify(previousValue);
                //UpdateVolumeText(bgmVolumeText, previousValue);
            }
        }
        else
        {
            AudioManager.Instance.SetBGMVolume(0);
            if (bgmSlider != null)
            {
                bgmSlider.SetValueWithoutNotify(0);
                //UpdateVolumeText(bgmVolumeText, 0);
            }
        }
    }

    private void OnSFXToggleChanged(bool isOn)
    {
        if (isOn)
        {
            float previousValue = sfxSlider != null ? sfxSlider.value : 0.5f;
            if (previousValue <= 0) previousValue = 0.5f;

            AudioManager.Instance.SetSFXVolume(previousValue);
            if (sfxSlider != null)
            {
                sfxSlider.SetValueWithoutNotify(previousValue);
                //UpdateVolumeText(sfxVolumeText, previousValue);
            }
        }
        else
        {
            AudioManager.Instance.SetSFXVolume(0);
            if (sfxSlider != null)
            {
                sfxSlider.SetValueWithoutNotify(0);
                //UpdateVolumeText(sfxVolumeText, 0);
            }
        }
    }

    private void OnTestSFXClicked()
    {
        AudioManager.Instance.PlaySFX(0);
    }

    private void UpdateVolumeText(TextMeshProUGUI textComponent, float value)
    {
        if (textComponent != null)
        {
            textComponent.text = Mathf.RoundToInt(value * 100) + "%";
        }
    }

    public void SetBGMVolumeFromExternal(float value)
    {
        if (bgmSlider != null)
        {
            bgmSlider.value = value;
        }
    }

    public void SetSFXVolumeFromExternal(float value)
    {
        if (sfxSlider != null)
        {
            sfxSlider.value = value;
        }
    }
}
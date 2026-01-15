using UnityEngine;
using UnityEngine.UI;
public class ControlUI: MonoBehaviour
{
    [SerializeField] ControlType type;
    Image image;
    public ControlType Type { get { return type; } }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetAlpha(bool enabled)
    {
        if (image == null) return;

        Color c = image.color;
        c.a = enabled ? 1f : 0.2f;
        image.color = c;
    }
}

public enum ControlType
{
    Left, 
    Right,
    Jump,
    Climb,
    ShootMode
}
using UnityEngine;
using System.Collections.Generic;

public class ControlsAvailable : MonoBehaviour
{
    [SerializeField] PlayerController reference;
    [SerializeField] List<ControlUI> controls;

    private ControlConfig config;

    private void Start()
    {
        reference = FindAnyObjectByType<PlayerController>();
        config = reference.Congfig;

        ApplyConfig();
    }

    private void ApplyConfig()
    {
        foreach (var c in controls)
        {
            bool enabled = c.Type switch
            {
                ControlType.Left => config.EnableLeft,
                ControlType.Right => config.EnableRight,
                ControlType.Jump => config.EnableJump,
                ControlType.Climb => config.EnableClimb,
                ControlType.ShootMode => config.EnableShootMode,
                _ => false
            };

            c.SetAlpha(enabled);
        }
    }
}

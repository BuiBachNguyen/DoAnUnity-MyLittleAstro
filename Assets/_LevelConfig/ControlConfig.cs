using UnityEngine;

[CreateAssetMenu(fileName = "ControlConfig", menuName = "Scriptable Objects/ControlConfig")]
public class ControlConfig : ScriptableObject
{
    public bool EnableLeft = true;
    public bool EnableRight = true;
    public bool EnableJump = true;
    public bool EnableClimb = true;
    public bool EnableDash = true;

}

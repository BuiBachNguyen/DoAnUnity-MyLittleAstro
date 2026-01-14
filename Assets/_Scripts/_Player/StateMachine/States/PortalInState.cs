using UnityEngine;

public class PortalInState : FSMState
{
    PlayerController player;
    Rigidbody2D rb;

    public override void Enter()
    {
        player = obj.GetComponent<PlayerController>();
        rb = obj.GetComponent<Rigidbody2D>();
        player.Animator.Play("PortalIn");
        timer = 0.5f;
        AudioManager.Instance.PlaySFX(AudioClipNames.Teleport);
    }

    public override void UpdateState(float delta)
    {
        if (UpdateTimer(delta))
        {
            ChangeState(new PortalOutState());
        } 
            
    }
}

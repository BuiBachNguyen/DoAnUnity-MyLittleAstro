using UnityEngine;

public class FallState : FSMState
{
    PlayerController player;
    Rigidbody2D rb;

    public override void Enter()
    {
        player = obj.GetComponent<PlayerController>();
        rb = obj.GetComponent<Rigidbody2D>();
        player.Animator.Play("Fall");
    }

    public override void UpdateState(float delta)
    {
        player.HandleJump();
        player.HandleMoving();

        if(player.IsOnGround && player.HandleMoving() == false)
        {
            ChangeState(new IdleState());
        }    
        
    }
    public override void Exit()
    {
        AudioManager.Instance.PlayPlayerSFX(AudioClipNames.ToLand);
    }
}

using UnityEngine;

public class ClimbState : FSMState
{
    PlayerController player;
    Rigidbody2D rb;

    public override void Enter()
    {
        player = obj.GetComponent<PlayerController>();
        rb = obj.GetComponent<Rigidbody2D>();
        player.Animator.Play("Climb");
        rb.gravityScale = 0;
    }

    public override void UpdateState(float delta)
    {
        player.HandleClimb();

        if (player.IsGrounded() == false)
        {
            ChangeState(new FallState());
        }

    }
    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = 1;
        rb.linearVelocityY = 0;
    }
}

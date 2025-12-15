using UnityEngine;

public class RunState : FSMState
{
    PlayerController player;
    Rigidbody2D rb;

    public override void Enter()
    {
        player = obj.GetComponent<PlayerController>();
        rb = obj.GetComponent<Rigidbody2D>();
        player.Animator.Play("Run");
    }

    public override void UpdateState(float delta)
    {
        player.HandleMoving();
        player.HandleJump();

        if(player.HandleMoving() == false)
        {
            ChangeState(new IdleState());
        }
        if(player.IsOnGround == false)
        {
            ChangeState(new FallState());
        }

    }
}

using UnityEngine;

public class JumpState : FSMState
{
    PlayerController player;
    Rigidbody2D rb;

    public override void Enter()
    {
        player = obj.GetComponent<PlayerController>();
        rb = obj.GetComponent<Rigidbody2D>();
        player.Animator.Play("Jump");
        player.Jump();
    }

    public override void UpdateState(float delta)
    {
        player.HandleMoving();

        if (rb != null && rb.linearVelocityY <= 0)
        {
            ChangeState(new FallState());
        }
    }
}

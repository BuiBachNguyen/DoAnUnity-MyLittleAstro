using UnityEngine;

public class IdleWithGunState : FSMState
{
    PlayerController player;
    Rigidbody2D rb;

    public override void Enter()
    {
        player = obj.GetComponent<PlayerController>();
        rb = obj.GetComponent<Rigidbody2D>();
        player.Animator.Play("IdleToShoot");
    }

    public override void UpdateState(float delta)
    {
        player.HandleShootPortal();

        player.HandleJump();

        if (player.IsGrounded() == false)
        {
            ChangeState(new FallState());
        }
    }

}

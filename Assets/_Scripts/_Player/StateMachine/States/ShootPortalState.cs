using UnityEngine;

public class ShootPortalState : FSMState
{
    bool hasShot = false;
    PlayerController player;
    Rigidbody2D rb;

    public override void Enter()
    {
        player = obj.GetComponent<PlayerController>();
        rb = obj.GetComponent<Rigidbody2D>();
        player.Animator.Play("Shoot");
        timer = 1.25f;
    }

    public override void UpdateState(float delta)
    {
        if (hasShot == false)
        {
            if (UpdateTimer(delta))
            {
                player.ShootPortal();
                timer = 0.5f;
                hasShot = true;
            }
            return;
        }
        if (UpdateTimer(delta))
        {
            ChangeState(new IdleState());
        }
    }

    public override void Exit()
    {
    }
}

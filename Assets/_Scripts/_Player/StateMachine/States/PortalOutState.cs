using UnityEngine;

public class PortalOutState : FSMState
{
    PlayerController player;
    Rigidbody2D rb;

    public override void Enter()
    {
        player = obj.GetComponent<PlayerController>();
        rb = obj.GetComponent<Rigidbody2D>();
        player.Animator.Play("PortalOut");
        timer = 0.5f;
    }

    public override void UpdateState(float delta)
    {
        if (UpdateTimer(delta))
        {
            ChangeState(new IdleState());
        }

    }
}
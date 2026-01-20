using UnityEngine;

public class IdleState : FSMState
{
    PlayerController player;
    Rigidbody2D rb;

    public override void Enter()
    {
        player = obj.GetComponent<PlayerController>();
        rb = obj.GetComponent<Rigidbody2D>();
        player.Animator.Play("Idle");
    }

    public override void UpdateState(float delta)
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.DialoguePlaying) return;
        if(player.HandleShootPortal() == true)
        {
            return;
        }
        player.HandleClimb();
        player.HandleMoving();
        player.HandleJump();
    }
}

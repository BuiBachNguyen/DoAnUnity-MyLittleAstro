using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ControlConfig _config;

    Collider2D _collider;
    Rigidbody2D _rigidbody;
    Animator _animator;
    [SerializeField] FSM _fsm;


    // ================= Props ================-
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float climbSpeed = 10.0f;
    bool isFacingRight = true;
    bool isOnGround = true;
    bool isClimbing = false;
    bool isGrabLadder = false; // trigger with ladder
    bool isUpStair = false; //check dirrection

    // ================= value INPUT =================

    Vector2 input = new Vector2(0, 0);
    bool jumpPressed = false;


    #region Getter-Setter
    public Animator Animator
    {
        get { return _animator; }
        set { _animator = value; }
    }
    public bool IsOnGround
    {
        get { return isOnGround; }
        set { isOnGround = value; }
    }

    public FSM Fsm
    {
        get { return _fsm; }
        set { _fsm = value; }
    }
    #endregion

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _fsm = GetComponent<FSM>();
    }

    void Start()
    {
        _fsm.ChangeState(new IdleState());
    }

    private void Update()
    {
        Debug.Log(_fsm.currentState.ToString());
    }

    // ================= MOVE =================
    public bool HandleMoving()
    {
        Flip();

        if (Mathf.Abs(input.x) >= 0.1f)
        {
            _rigidbody.linearVelocity = new Vector2(input.x * moveSpeed, _rigidbody.linearVelocity.y);
            if (isOnGround == true)
                _fsm.ChangeState(new RunState());
            return true;
        }
        else
        {
            _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocity.y);
        }
        return false;
    }

    public bool HandleJump()
    {
        if (jumpPressed && isOnGround)
        {
            jumpPressed = false;
            isOnGround = false;
            _fsm.ChangeState(new JumpState());
            return true;
        }
        return false;
    }
    public void Jump()
    {
        Vector2 force = new Vector2(0.0f, jumpForce);
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    public bool HandleClimb()
    {
        if (isClimbing == true)
        {
            _collider.isTrigger = true;
            _fsm.ChangeState(new ClimbState());
            float direction = isUpStair ? -1f : 1f;
            _rigidbody.linearVelocity = new Vector2(0, direction * climbSpeed);
            isClimbing = isGrabLadder;
            return true;
        }

        // else
        //isClimbing = false;
        _collider.isTrigger = false;
        _fsm.ChangeState(new IdleState());

        return false;
    }

    private void Flip()
    {
        if (isFacingRight && input.x < 0 || !isFacingRight && input.x > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 Oscale = transform.localScale;
            Oscale.x = Oscale.x * -1;
            transform.localScale = Oscale;
        }
    }

    // ========= INPUT EVENTS =========
    public void OnMove(InputValue movementvalue)
    {
        input = movementvalue.Get<Vector2>();
    }


    public void OnJump(InputValue isJump)
    {
        if (isOnGround)
        {
            jumpPressed = isJump.isPressed;
        }
    }

    public void OnInteract(InputValue isInteract)
    {
        if (isInteract.isPressed)
        {
            isClimbing = isInteract.isPressed && isGrabLadder;
            Debug.Log("Interacted. IsClimbing = " + isClimbing.ToString());
        }
    }

    // =========== Collision ================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Ground))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Ladder))
        {
            isGrabLadder = true;
            Vector2 stairPos = collision.gameObject.transform.position;
            Vector2 playerPos = transform.position;

            if (playerPos.y < stairPos.y - 0.1f)
            {
                isUpStair = false;
            }
            else
            {
                isUpStair = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Ladder))
        {
            Collider2D L_col = collision.gameObject.GetComponent<Collider2D>();
            if (_collider.bounds.min.y - L_col.bounds.max.y > 0 && isUpStair == false)
            {
                isUpStair = true;
                isGrabLadder = false;
            }
            else if (_collider.bounds.max.y - L_col.bounds.min.y < 0.5 && isUpStair == true)
            {
                isUpStair = false;
                isGrabLadder = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Ladder))
        {
            isGrabLadder = false;
        }
    }

}

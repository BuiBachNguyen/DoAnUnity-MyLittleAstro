using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

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
    bool isFacingRight = true;
    bool isOnGround = true;

    // ================= value INPUT =================

    Vector2 input;
    bool jumpPressed;


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
        //Debug.Log(_fsm.currentState.ToString());
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
        jumpPressed = isJump.isPressed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Ground))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag(Tags.Ground))
        //{
        //    isOnGround = false;
        //}
    }
}

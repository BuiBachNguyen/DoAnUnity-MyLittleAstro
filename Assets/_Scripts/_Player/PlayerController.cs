using System.Collections.Generic;
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
    bool isInteract = false;
    bool isGrabLadder = false; // trigger with ladder
    bool isUpStair = false; //check dirrection
    // ================== Shooting references ===============
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] GameObject PortalPrefab;
    
    Queue<GameObject> BulletQueue = new Queue<GameObject>();
    Queue<GameObject> PortalQueue = new Queue<GameObject>();

    // ================= value INPUT =================

    Vector2 input = new Vector2(0, 0);
    bool jumpPressed = false;
    bool inShootPortalMode = false;

    bool leftShootClicked = false;
    bool rightShootClicked = false;

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
        if(firePoint == null)
        {
            Debug.LogError("Null FirePoint");
        }    
        if(BulletPrefab == null || PortalPrefab == null )
        {
            Debug.LogError("Null Bullet or Portal Prefab at Player");
        }    
    }

    private void Update()
    {
        Debug.Log(_fsm.currentState.ToString());
    }

    // ============== SHOOTING ================
    public bool HandleShootPortal()
    {
        if (inShootPortalMode)
        {
            _fsm.ChangeState(new IdleWithGunState());
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            Vector2 dir = (mouseWorldPos - transform.position).normalized;
            if ((dir.x > 0f && !isFacingRight) || (dir.x < 0f && isFacingRight))
            {
                {
                    isFacingRight = !isFacingRight;
                    Vector3 Oscale = transform.localScale;
                    Oscale.x = Oscale.x * -1;
                    transform.localScale = Oscale;
                }
            }
            if (leftShootClicked)
            {
                ReturnDefaultShootMode();
                Debug.Log("LEFT CLICK");
                //ShootPortal(BulletPrefab, PortalPrefab);
                inShootPortalMode = ! inShootPortalMode;
                _fsm.ChangeState(new ShootPortalState());
            }
            else if (rightShootClicked)
            {
                ReturnDefaultShootMode();
                Debug.Log("RIGHT CLICK");
                //ShootPortal(BulletPrefab, PortalPrefab);
                inShootPortalMode = ! inShootPortalMode;
                _fsm.ChangeState(new ShootPortalState());
            } 
            return true;  
        }
        else
        {
            _fsm.ChangeState(new IdleState());
        }
        return false;
    }

    public void ReturnDefaultShootMode()
    {
        leftShootClicked = false;
        rightShootClicked = false;
    }
    public void ShootPortal()  //GameObject bulletPrefab, GameObject portalPrefab)
    {
        //Clear if >=2. remove to shoot new portal
        if(BulletQueue.Count >= 2 || PortalQueue.Count >= 2)
        {
            while (BulletQueue.Count >= 2)
                Destroy(BulletQueue.Dequeue());
            while(PortalQueue.Count >= 2)
                Destroy(PortalQueue.Dequeue());
        } 
        //Calculate
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector2 dir = (mouseWorldPos - firePoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        AudioManager.Instance.PlayPlayerSFX(AudioClipNames.Shoot);
        //New Bullet, new Portal
        //GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0,0,angle - 90));
        //GameObject portal = Instantiate(portalPrefab);
        GameObject bullet = Instantiate(BulletPrefab, firePoint.position, Quaternion.Euler(0,0,angle - 90));
        GameObject portal = Instantiate(PortalPrefab);
        portal.SetActive(false);
        bullet.GetComponent<Bullet>().SetRef(portal);

        BulletQueue.Enqueue(bullet);
        PortalQueue.Enqueue(portal);

        if (PortalQueue.Count == 2)
        {
            GameObject[] portals = PortalQueue.ToArray();

            portals[0].GetComponent<TelePortal>().Other =
                portals[1].GetComponent<TelePortal>();

            portals[1].GetComponent<TelePortal>().Other =
                portals[0].GetComponent<TelePortal>();
        }
    }


    // ================= MOVEMENT =================
    public bool HandleMoving()
    {
        Flip();

        if (Mathf.Abs(input.x) >= 0.1f)
        {
            _rigidbody.linearVelocity = new Vector2(input.x * moveSpeed, _rigidbody.linearVelocity.y);
            if(AudioManager.Instance.IsPlayerSFXEnd() && Mathf.Abs(this._rigidbody.linearVelocityX) >= 0 && _rigidbody.linearVelocityY == 0)
                AudioManager.Instance.PlayPlayerSFX(AudioClipNames.Run);
            if (isOnGround == true)
            {
                _fsm.ChangeState(new RunState());
            }    
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
            AudioManager.Instance.PlayPlayerSFX(AudioClipNames.Jump);

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
        this.isInteract = isInteract.isPressed;
        if (isInteract.isPressed)
        {
            isClimbing = this.isInteract && isGrabLadder;
            Debug.Log("Interacted. IsClimbing = " + isClimbing.ToString());
        }
        Debug.Log(this.isInteract);
    }

    public void OnShootPortalMode(InputValue isShootPortalMode)
    {
        if (isShootPortalMode.isPressed)
        {
            inShootPortalMode = !(inShootPortalMode);
            leftShootClicked = false;
            rightShootClicked = false;
        }
    }
    public void OnShootPortalLeft(InputValue isShootPortalLeft)
    {
        if(isShootPortalLeft.isPressed)
        {
             leftShootClicked =  true;
        }    
    }    
    public void OnShootPortalRight(InputValue isShootPortalRight)
    {
        if(isShootPortalRight.isPressed)
        {
            rightShootClicked = true;
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
        if(collision.gameObject.CompareTag(Tags.Lever))
        {
            if(isInteract == true)
            {
                Lever lever = collision.gameObject.GetComponent<Lever>();
                lever.SetActivated(!lever.IsActivated);
                Debug.Log("lever interacted");
                isInteract = false;
            }
            return;
        }

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

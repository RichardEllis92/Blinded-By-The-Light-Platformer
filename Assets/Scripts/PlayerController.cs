using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField]
    float movementSpeed = 5f;
    [SerializeField]
    float jumpSpeed = 5f;
    [SerializeField]
    float doubleJumpPower = 10f;
    [SerializeField]
    float climbSpeed = 5f;
    public bool doubleJumpUnlocked = false;
    public bool fireBallUnlocked = false;
    float xScale = 6.66f;
    float yScale = 6.66f;
    bool grounded;
    float gravityScaleAtStart;

    [SerializeField]
    private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }

    public GameObject player;

    //public Rigidbody2D theRB;

    private bool doubleJump;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    public Vector2 physgravity;
    

    public GameObject fireBall;
    public Transform fireBallPoint;
    public float timeBetweenShots;
    private float shotCounter;
    private static Vector2 fireBallPosition;

    void Start()
    {
        instance = this;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        physgravity = Physics2D.gravity;
    }

    
    void Update()
    {
        Run();
        FlipSprite();
        StopMoving();
        PlayerLimits();
        ShootFireball();
        JumpAnimation();
        ClimbLadder();

        if (Input.GetKeyDown(KeyCode.E) && dialogueUI.isOpen == false)
        {
            if(Interactable != null)
            {
                Interactable.Interact(this);
            }
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (dialogueUI.isOpen || DebugController.instance.showConsole){ return; }

        if (IsGrounded() && !value.isPressed)
        {
            doubleJump = false;
        }

        if (value.isPressed)
        {
            if (doubleJumpUnlocked)
            {
                if (value.isPressed && (IsGrounded() || doubleJump))
                {
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, doubleJump ? doubleJumpPower : jumpSpeed);
                    doubleJump = !doubleJump;
                }
            }
            else
            {
                if (value.isPressed && IsGrounded())
                {
                    myRigidbody.velocity += new Vector2(0f, jumpSpeed);
                }
            }
        }
    }

    void Run()
    {
        if (dialogueUI.isOpen || DebugController.instance.showConsole) return;

        Vector2 playerVelocity = new Vector2 (moveInput.x * movementSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            myAnimator.SetBool("isWalking", true);
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
        }
        
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (dialogueUI.isOpen) return;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2((Mathf.Sign(myRigidbody.velocity.x)) / xScale, 1f / yScale);
        }

    }

    void StopMoving()
    {
        if (dialogueUI.isOpen || DebugController.instance.showConsole)
        {
            myRigidbody.velocity = Vector2.zero;
            myAnimator.gameObject.GetComponent<Animator>().enabled = false;
        } 
        else if (!dialogueUI.isOpen && !DebugController.instance.showConsole)
        {
            myAnimator.gameObject.GetComponent<Animator>().enabled = true;
        }
    }

    public void UnlockDoubleJump()
    {
        doubleJumpUnlocked = true;
    }

    public void UnlockFireBall()
    {
        fireBallUnlocked = true;
    }

    public bool IsGrounded()
    {
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void PlayerLimits()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -18.5f, 100f), transform.position.y);
    }

    void JumpAnimation()
    {
        if (IsGrounded())
        {
            myAnimator.SetBool("isJumping", false);
        }
        else
        {
            myAnimator.SetBool("isJumping", true);
        }
    }

    void ShootFireball()
    {
        if (fireBallUnlocked)
        {
            if (shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    fireBallPosition = transform.position;
                    GameObject newFireBall = Instantiate(fireBall, fireBallPosition, Quaternion.LookRotation(transform.localScale));
                    if (transform.localScale.x > 0)
                    {
                        newFireBall.transform.right = transform.right.normalized;
                    }
                    else
                    {
                        newFireBall.transform.right = -transform.right.normalized;
                    }                    
                    shotCounter = timeBetweenShots;
                }
            }
        }
    }

    void ClimbLadder()
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) || dialogueUI.isOpen || DebugController.instance.showConsole) 
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            Physics2D.gravity = physgravity;
            myAnimator.SetBool("isClimbing", false);
            myAnimator.speed = 1;
            return; 
        }

        if (Input.GetKey(KeyCode.W))
        {
            myAnimator.SetBool("isClimbing", true);
            Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
            myRigidbody.velocity = climbVelocity;
            myRigidbody.gravityScale = 0;
            Physics2D.gravity = new Vector2(0, 0);
            myAnimator.speed = 1;
            Debug.Log("Going Up");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            myAnimator.SetBool("isClimbing", true);
            Vector2 climbVelocity = new Vector2(-myRigidbody.velocity.x, moveInput.y * climbSpeed);
            myRigidbody.velocity = climbVelocity;
            myRigidbody.gravityScale = 0;
            Physics2D.gravity = new Vector2(0, 0);
            myAnimator.speed = 1;
            Debug.Log("Going Down");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Vector2 climbVelocity = new Vector2(moveInput.y, -myRigidbody.velocity.y * climbSpeed);
            myAnimator.speed = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector2 climbVelocity = new Vector2(moveInput.y, myRigidbody.velocity.y * climbSpeed);
            myAnimator.speed = 1;
        }
        else if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("isClimbing", true);
            myRigidbody.gravityScale = 0;
            myAnimator.speed = 0;
            myRigidbody.velocity = new Vector2(0, 0);
        }
        

/*
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            if (Input.GetKey(KeyCode.W))
            {
                Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
                myRigidbody.velocity = climbVelocity;
                Debug.Log("Going Up");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Vector2 climbVelocity = new Vector2(-myRigidbody.velocity.x, moveInput.y * climbSpeed);
                myRigidbody.velocity = climbVelocity;
                Debug.Log("Going Down");
            }
            else
            {
                //myRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }
*/
    }
}

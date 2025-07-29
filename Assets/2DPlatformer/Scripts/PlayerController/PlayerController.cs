using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset m_playerInput;
    private InputAction m_moveAction;
    private InputAction m_jumpAction;
    
    private bool m_readyToJump = false;
    private bool m_isJumping = false;
    [SerializeField]
    private float m_topSpeed;

    private Animator m_animator;

    private Rigidbody2D m_rigidbody;

    

    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_jumpForce;

    private Vector2 m_moveVector;
    private void Awake()
    {
        //Set Input Actions
        m_moveAction = InputSystem.actions.FindAction("Move");        
        m_jumpAction = InputSystem.actions.FindAction("Jump");


        //Get Components
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponentInChildren<Animator>();


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void FixedUpdate()
    {
        m_rigidbody.AddForce(m_moveVector * m_moveSpeed, ForceMode2D.Force);
        if (m_readyToJump)
        {
            Jump();
        }
    }

    void PlayerMovement()
    {
        m_moveVector.x = m_moveAction.ReadValue<Vector2>().x;
        //Set Face Direction Based on Input
        if(m_rigidbody.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //while jumping check for ground
        if (m_isJumping)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, 1f))
            {
                m_isJumping = false;
                m_animator.SetTrigger("Idle");
            }
        }
        else
        {
            m_animator.SetFloat("Movement", m_rigidbody.velocity.magnitude);
        }
        if (m_jumpAction.IsPressed())
        {
            SetJump();       
        }
    }

    private void SetJump()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 1.1f))
        {
            m_readyToJump = true;
        }
    }

    private void Jump()
    {
        m_rigidbody.AddForce(m_jumpForce * Vector2.up, ForceMode2D.Impulse);
        m_animator.SetTrigger("Jump");
        m_readyToJump = false;        
        StartCoroutine(SetJumpBool());
    } 

    private IEnumerator SetJumpBool()
    {
        yield return new WaitForSeconds(0.5f);
        m_isJumping = true;
    }
}

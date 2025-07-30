using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
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
        //Get Components
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponentInChildren<Animator>();


    }
    void Start()
    {
        Time.timeScale = 1.0f;   
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
        m_moveVector.x = Input.GetAxis("Horizontal");
        //Set Face Direction Based on Input
        if(m_moveVector.x < 0)
        {
            transform.localScale = new Vector3(-1,1,1);            
        }
        else if (m_moveVector.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);            
        }
        else
        {
            m_animator.SetTrigger("Idle");            
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
            if (m_moveVector.x < 0)
            {                
                m_animator.SetTrigger("Movement");
            }
            else if (m_moveVector.x > 0)
            {                
                m_animator.SetTrigger("Movement");
            }
        }
        if (Input.GetKey(KeyCode.Space)) 
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
        m_readyToJump = false;
        m_isJumping = true;
    }

    private void CapSpeed() 
    {
        if (m_rigidbody.velocity.magnitude > m_topSpeed)
        { 
            m_rigidbody.velocity = m_rigidbody.velocity.normalized * m_topSpeed;
        }
    }
}


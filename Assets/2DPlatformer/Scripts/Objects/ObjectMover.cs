using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    Transform m_parentTransform;
    public Vector2 MoveDirection;
    public float MoveDistance;
    public float MoveSpeed;
    private Vector2 m_targetPosition;
    private Vector2 m_startingPosition;
    
    private bool m_returnToPosition = false;


    void Start()
    {
        m_parentTransform = transform.parent.transform;
        m_startingPosition = m_parentTransform.position;
        m_targetPosition = new Vector2(m_parentTransform.position.x, m_parentTransform.position.y) + (MoveDirection * MoveDistance);        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_returnToPosition)
        {
            m_parentTransform.position = Vector2.MoveTowards(m_parentTransform.position, m_startingPosition, MoveSpeed * Time.deltaTime);
            Vector2 distanceTravelled = new Vector2(m_parentTransform.position.x, m_parentTransform.position.y) - m_targetPosition;
            if(distanceTravelled.magnitude >= MoveDistance)
            {
                m_returnToPosition = false;
                m_parentTransform.position = m_startingPosition;
            }
        }
        else
        {
            m_parentTransform.position = Vector2.MoveTowards(m_parentTransform.position, m_targetPosition, MoveSpeed * Time.deltaTime);
            Vector2 distanceTravelled = new Vector2(m_parentTransform.position.x, m_parentTransform.position.y) - m_startingPosition;
            if (distanceTravelled.magnitude >= MoveDistance)
            {
                m_returnToPosition = true;
                m_parentTransform.position = m_targetPosition;
            }
        }        
    }
}

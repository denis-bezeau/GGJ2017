using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour {
    public float X_THRESHOLD = 0.15f;
    public float MAX_RUN_SPEED = 3;
    public float MAX_SPEED = 3;
    public float m_fMoveSpeed = 8;
    public float m_fJumpSpeed = 80;
    public bool m_bJumping = false;
    public bool m_bRunning = false;
	public bool m_bCrouching = false;

    private Rigidbody2D rigidBody;
    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < -X_THRESHOLD)
        {
            rigidBody.AddForce(new Vector2(-m_fMoveSpeed, 0.0f));

            if (rigidBody.velocity.x <  -getSpeed())
            {
                rigidBody.velocity = new Vector2(-getSpeed(), rigidBody.velocity.y);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > X_THRESHOLD)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(m_fMoveSpeed, 0.0f));

            if (rigidBody.velocity.x > getSpeed())
            {
                rigidBody.velocity = new Vector2(getSpeed(), rigidBody.velocity.y);
            }
        }
        if (Input.GetKey(KeyCode.CapsLock))
        {
            m_bRunning = true;
        }
        else { m_bRunning = false; }

        if (Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical") < -X_THRESHOLD)
        {
            m_bCrouching=  true;
        }
        else
        {
			m_bCrouching = false;
        }

		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) && m_bJumping == false)
        {
            m_bJumping = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, m_fJumpSpeed));
        }
    }

	public bool IsCrouching()
	{
		return m_bCrouching;
	}

	public bool IsJumping()
	{
		return m_bJumping;
	}

    public void GroundCollision()
    {
            m_bJumping = false;
    }

	public Vector2 GetVelocity()
	{
		return rigidBody.velocity;
	}

    public void FinishCollision()
    {
        GameManager.GetInstance().LevelUp();
        GameManager.GetInstance().Respawn();
        

        Debug.Log("FinishCollision()");
    }

    public float getSpeed()
    {
        if (m_bRunning == true)
        {
            return MAX_RUN_SPEED;
        }
        else
        {
            return MAX_SPEED;
        }

    }
}

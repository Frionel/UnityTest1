using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject shadow;
	public float movementSpeed = 2.0f;
	public float jumpSpeed = 10.0f;

	private bool _isGrounded = false;
    private Rigidbody _rigidbody;
    private Animator _animator;
    
    private enum Direction
    {
        Left = 0,
        Right,
        Up,
        Down
    };

    // Use this for initialization
    void Start()
	{
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        GameObject shadowInstance = Instantiate(shadow);
        shadowInstance.name = "Shadow_Player";

        ShadowMovement script = shadowInstance.GetComponent<ShadowMovement>();
        script._gameObject = gameObject;
        script._offset = new Vector3(0.0f, -0.15f, 0.0f);
    }

	// Update is called once per frame
	void Update()
	{
        UpdateMovement();
        UpdateAnimator();
        UpdateGrounded();
    }

    void UpdateMovement()
    {
        Vector3 movement = Vector3.zero;
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement *= movementSpeed * Time.deltaTime;

        bool jump = _isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0));
        movement.z = jump ? -jumpSpeed : _rigidbody.velocity.z;
        
        if(CheckCollision(new Vector3(movement.x, 0.0f, 0.0f)))
        {
            movement.x = 0.0f;
        }

        if(CheckCollision(new Vector3(0.0f, movement.y, 0.0f)))
        {
            movement.y = 0.0f;
        }

        _rigidbody.velocity = movement;
    }

    bool CheckCollision(Vector3 movement)
    {
        float distance = movement.magnitude * Time.fixedDeltaTime;
        movement.Normalize();

        // Check if the body's current velocity will result in a collision
        RaycastHit hitInfo;
        bool hit = _rigidbody.SweepTest(movement, out hitInfo, distance);

        return hit && hitInfo.transform.gameObject.tag != "Movable";
    }

    void UpdateAnimator()
    {
        Vector3 playerVelocity = _rigidbody.velocity;
        _animator.SetBool("isMoving", new Vector2(playerVelocity.x, playerVelocity.y).magnitude > 0.0f);
        _animator.SetBool("isJumping", playerVelocity.z > 0.0f);
        _animator.SetBool("isFalling", playerVelocity.z < 0.0f);

        Direction newDirection = GetDirection();
        _animator.SetInteger("direction", (int)newDirection);
    }

    Direction GetDirection()
    {
        Vector3 playerVelocity = _rigidbody.velocity;

        if (Mathf.Abs(playerVelocity.y) > Mathf.Abs(playerVelocity.x))
        {
            playerVelocity.x = 0.0f;
        }
        else if (Mathf.Abs(playerVelocity.x) > Mathf.Abs(playerVelocity.y))
        {
            playerVelocity.y = 0.0f;
        }

        if (playerVelocity.x > 0.0f) return Direction.Right;
        if (playerVelocity.x < 0.0f) return Direction.Left;
        if (playerVelocity.y > 0.0f) return Direction.Up;

        // Down
        return Direction.Down;
    }

    void UpdateGrounded()
    {
        RaycastHit rayHit;
        int ignoreLayerMask = ~(1 << 8); // Player + Shadow

        Debug.DrawRay(transform.position + GetComponent<CapsuleCollider>().center, transform.TransformDirection(Vector3.forward));

        if (!Physics.Raycast(transform.position + GetComponent<CapsuleCollider>().center, transform.TransformDirection(Vector3.forward), out rayHit, Mathf.Infinity, ignoreLayerMask))
        {
            return;
        }

        float colliderRadius = GetComponent<CapsuleCollider>().radius;
        float margin = 0.075f;

        _isGrounded = Mathf.Abs(colliderRadius - rayHit.distance) < margin;
    }

    private void OnDrawGizmos()
    {
        /*
        CapsuleCollider c = GetComponent<CapsuleCollider>();
        Debug.Log("Center: " + c.center.ToString());
        Gizmos.DrawCube(transform.position + c.center, new Vector3(0.1f, 0.1f, 0.1f));
        */
    }
}

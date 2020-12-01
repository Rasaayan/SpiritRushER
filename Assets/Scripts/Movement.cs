
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody;
    SphereCollider collider;

    bool isJumpPressed = false;
    float floorMargin = 0.1f;
    public float jumpForce = 20f;
    public float gravityMagnitude = 10f;
    public float jumpTime = 0.35f;
    public float jumpHeight = 5.0f;
    public float hoverTime = 0.4f;
    public float turnSpeed = 8f;
    public float forwardSpeed = 3000f;

    Vector3 movementDir = Vector3.zero;

    public Pickups pickupsObj;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Y))
        {
            collider.isTrigger = false;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            collider.isTrigger = true;
        }
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }
        
            movementDir.x = Input.GetAxis("Horizontal");
                
    }
    
    void FixedUpdate()
    {
        rigidBody.AddForce(0, 0, forwardSpeed * Time.deltaTime);

        Vector3 velocity = rigidBody.velocity;
        velocity.x = 0;
        rigidBody.velocity = velocity;

        if (isJumpPressed && IsAirborne())
        {
            jumpForce = GetYVelocity(jumpHeight, jumpTime);
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            AudioManager.Instance.PlayClip("Jump");
        }

        isJumpPressed = false;

        float gravityTime = rigidBody.velocity.y >= 0 ? jumpTime : hoverTime;
        gravityMagnitude = GetGravity(jumpHeight, gravityTime);
        rigidBody.AddForce(Vector3.up * gravityMagnitude);
        
        rigidBody.MovePosition(transform.position + movementDir * turnSpeed * Time.fixedDeltaTime);
        
        if (rigidBody.position.y < -0.5f)
        {
            FindObjectOfType<GameManager>().GameOver();
        }        
    }

    float GetGravity(float height, float time)
    {
        return -(2 * height) / (time * time);
    }

    float GetYVelocity(float height, float time)
    {
        return 2 * height / time;
    }

    bool IsAirborne()
    {
        RaycastHit hitInfo;
        return Physics.SphereCast(transform.position, 0.25f, Vector3.down, out hitInfo, collider.radius + floorMargin);
    }    


    public void TimeWarp(int timeWarpValue) // 0 slow , 1  normal 
    {
        if (timeWarpValue == 0) // slow 
        {
            turnSpeed = 3;
        }
        else if (timeWarpValue == 1) // normal
        {
            turnSpeed = 10;
        }
    }

    public void ActivateEther()
    {

    }
}

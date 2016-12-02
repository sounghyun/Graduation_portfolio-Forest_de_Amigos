using UnityEngine;
using System.Collections;

public class PlayerControll : MonoBehaviour {
    public float speed = 6f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    public GameObject m_camera;
    public Vector3 m_look;
    public Vector3 m_right;
    public float h, v, m_forword_dir,m_right_dir;

    // Use this for initialization

    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();

    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        m_look = m_camera.GetComponent<Transform>().forward;
        m_right = m_camera.GetComponent<Transform>().right;


        v = Input.GetAxisRaw("Horizontal");
        h = Input.GetAxisRaw("Vertical");

        m_forword_dir = h * m_look.x + v * m_right.x;
        m_right_dir = h * m_look.z + v * m_right.z;
  
        // Move the player around the scene.
        Move(m_forword_dir, m_right_dir);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
       



    }

    void Update()
    {

        // Animate the player.
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Jump()
    {
        playerRigidbody.AddForce(Vector3.up*300);
    }
   

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
       // bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
       // anim.SetBool("IsWalking", walking);
    }

}

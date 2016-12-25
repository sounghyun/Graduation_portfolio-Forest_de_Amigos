using UnityEngine;
using System.Collections;

public class PlayerControll : MonoBehaviour {
    public float speed = 6f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	Transform playertransform;
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    public GameObject m_camera;
    public Vector3 m_look;
    public Vector3 m_right;
	public float h, v, m_forword_dir,m_right_dir;
	public float m_TurnSpeed = 180f;
	public float AIrotationtime = 5f;
	public GameObject looking;

    // Use this for initialization

    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
		playertransform = GetComponent<Transform> ();

    }
    void Start () {
		looking = GameObject.FindGameObjectWithTag("lookingit");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        m_look = m_camera.GetComponent<Transform>().forward;	////카메라가 바라보는 방향
        m_right = m_camera.GetComponent<Transform>().right;		////카메라의 오른쪽 방향
        v = Input.GetAxisRaw("Horizontal");						////인풋값 --양옆
		h = Input.GetAxisRaw("Vertical");						////인풋값 --앞뒤

        m_forword_dir = h * m_look.x + v * m_right.x;			////방향값 구하기 : 앞
		m_right_dir = h * m_look.z + v * m_right.z;				////방향값 구하기 : 옆  


		KeyInput ();
    }

    void Update()
    {
		// Move the player around the scene.
		Move(m_forword_dir, m_right_dir);


		// Turn the player around the scene.
		Turn();
		AI ();


        // Animate the player.
        Animating(h, v);
    }

	void KeyInput()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			Jump();
	}

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);

		if (h != 0 || v != 0)
			AIrotationtime = 5f;
    }

	void Turn()
	{
		///좌 우 회 전 시 플레 이 어 회 전
		/// 
		if(v>0)
			TurnFunction (m_right);
		else if(v<0)
			TurnFunction(-m_right);
		///앞 뒤 로 움 직 일 때 플 레 이 어 회 전
		if(h>0)
			TurnFunction ( m_look);
		else if(h<0)
			TurnFunction ( -m_look);


	}

	void AI()
	{
		AIrotationtime = AIrotationtime- Time.deltaTime;

		if (AIrotationtime < 0) 
		{			
			if((looking.transform.position-playertransform.position).sqrMagnitude<100)
				TurnFunction (looking.transform.position-playertransform.position);
			else
				TurnFunction (m_camera.GetComponent<Transform> ().position-playertransform.position);
		}
	}

	void TurnFunction(Vector3 destinationVec3)
	{
		Vector3 t =Vector3.Cross ( playertransform.forward, destinationVec3);
		float result = Vector3.Dot (playertransform.up, t); //스칼라 3중적, 업벡터 와 (바라보는 방향과 가고자하는 방향외적) 내적 = 양수 반시계,음수 시계
		float turn = m_TurnSpeed*Time.deltaTime;				///좌우 로 회전
		Quaternion turnRotation = Quaternion.identity;

		Vector3 temp = new Vector3(destinationVec3.x,0f,destinationVec3.z);
		float sec = Vector3.Angle (temp, playertransform.forward );

		if(sec>5)
			if (result > 0) { 															/// 양 수 반 시 계 회 전 
				turnRotation = Quaternion.Euler (0f, turn, 0f);
				playerRigidbody.MoveRotation (playerRigidbody.rotation * turnRotation);

			} else if (result < 0) {															/// 음 수 시 계 회 전 
				turnRotation = Quaternion.Euler (0f, -turn, 0f);
				playerRigidbody.MoveRotation (playerRigidbody.rotation * turnRotation);
			}

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

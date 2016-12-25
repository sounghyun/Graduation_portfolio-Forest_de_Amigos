using UnityEngine;

using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject player;
    Transform playerposition;
    Vector3 offset ;
	// Use this for initialization
	void Start () {
        playerposition = player.GetComponent<Transform>();
        offset = new Vector3(0, 3, -3);
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Mathf.Sqrt((player.transform.position.x - transform.position.x) * (player.transform.position.x - transform.position.x)
                + ((player.transform.position.z - transform.position.z) * (player.transform.position.z - transform.position.z))) < 5)
            ;
        else
        {
            
            transform.position = Vector3.Lerp(transform.position, playerposition.position + offset, Time.deltaTime);
        }

	}
}

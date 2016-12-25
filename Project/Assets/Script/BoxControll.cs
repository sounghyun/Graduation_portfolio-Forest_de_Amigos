using UnityEngine;
using System.Collections;

public class BoxControll : MonoBehaviour {
    
    public GameObject box;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            box.GetComponent<Transform>().position = new Vector3(38.71f, 5.41f, 49.88f);
           
        }
	}
}

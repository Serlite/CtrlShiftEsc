using UnityEngine;
using System.Collections;

public class _PlayerMovement : MonoBehaviour {

	public float speedMovement = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetAxis("Horizontal") < 0.0f )
		{
			transform.Translate(Vector3.left * speedMovement);
		}
		else if( Input.GetAxis("Horizontal") > 0.0f )
		{
			transform.Translate(Vector3.right * speedMovement);
		}

		if( Input.GetAxis("Vertical") < 0.0f )
		{
			transform.Translate(Vector3.back * speedMovement);
		}
		else if( Input.GetAxis("Vertical") > 0.0f )
		{
			transform.Translate(Vector3.forward * speedMovement);
		}

	}
}

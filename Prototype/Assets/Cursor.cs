using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cursor : MonoBehaviour {

	public Game game;
	Shifter target = null;

	List<Shifter> shifters;

	private Vector2 lastInputAxis = Vector2.zero;
	private Vector2 inputAxis = Vector2.zero;

	// Use this for initialization
	void Start () {
		shifters = new List<Shifter> ();
	}
	
	// Update is called once per frame
	void Update () {
		updateTarget ();

		if (!Input.GetButton ("A_1")){
			shiftPosition ();
		}

		if (target != null) {
			acquireXY ();

			// Adjust rotational values if necessary
			if (target.Type == Shifter.ShiftType.Rotate) {
				if (Input.GetButtonDown ("A_1")) {
					target.startRotate (inputAxis.x, inputAxis.y);
				}
			}

			if (Input.GetButton ("A_1") && target != null && inputAxis.magnitude > 0) {
				target.manipulate (inputAxis.x, inputAxis.y);
			}
		}
	}

	void shiftPosition(){
		Vector3 newPos = transform.position;
		newPos.x += Input.GetAxis ("L_XAxis_1") * Time.deltaTime * 4;
		newPos.z -= Input.GetAxis("L_YAxis_1") * Time.deltaTime * 4;

		if (newPos != transform.position){
			transform.position = newPos;
		}
	}

	void acquireXY(){
		if (game.Control == Game.ControlType.Mouse) {

		}
		else if (game.Control == Game.ControlType.Gamepad) {
			inputAxis.x = Input.GetAxis("L_XAxis_1");
			inputAxis.y = Input.GetAxis("L_YAxis_1");
		}
	}

	void updateTarget(){
		float squareDist = -1;

		if (shifters.Count > 0){
			foreach(Shifter nearTarget in shifters){
				float nearDist = Vector3.Distance(transform.position, nearTarget.transform.position);
			
				if (squareDist == -1 || squareDist > nearDist){
					target = nearTarget;
					squareDist = nearDist;
				}
			}
		}
		else{
			target = null;
		}
	}

	public void AddSelf(Shifter newShifter){
		shifters.Add (newShifter);
		//Component halo = gameObject.GetComponent("Halo"); halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
	}

	public void RemoveSelf(Shifter removeShifter){
		shifters.Remove (removeShifter);
	}
}

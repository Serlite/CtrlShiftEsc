using UnityEngine;
using System.Collections;

public class Shifter : MonoBehaviour {

	public enum ShiftType{Rotate, Translate};
	public ShiftType Type = ShiftType.Rotate;

	Vector3 lastDir = Vector3.zero;

	public Vector2 translateMax = Vector2.zero;
	public Vector2 translateMin = Vector2.zero;
	Vector3 initialPos;

	bool isSelected = false;
	bool isActive = false;

	public Game game;
	public Cursor cursor;

	// Use this for initialization
	void Start () {
		initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void manipulate(float xVal, float zVal){
		if (Type == ShiftType.Rotate) {

			Vector3 currDir = new Vector3(xVal, 0, zVal).normalized;			
			Quaternion newRot = Quaternion.Inverse(Quaternion.FromToRotation(lastDir, (currDir + lastDir) * 0.5f));

			//print (Quaternion.FromToRotation(lastDir, (currDir + lastDir) * 0.5f));

			if (lastDir != Vector3.zero){
				transform.rotation = transform.rotation * newRot;
			}

			lastDir = currDir;
		}
		else if (Type == ShiftType.Translate) {
			Vector3 newPos = transform.position;

			newPos.x = Mathf.Min(Mathf.Max (transform.position.x + xVal * Time.deltaTime, translateMin.x + initialPos.x), translateMax.x + initialPos.x);
			newPos.z = Mathf.Min(Mathf.Max (transform.position.z - zVal * Time.deltaTime, translateMin.y + initialPos.z), translateMax.y + initialPos.z);

			//newPos.x += xVal * Time.deltaTime;
			//newPos.z -= zVal * Time.deltaTime;

			transform.position = newPos;
		}
	}

	public void startRotate(float xVal, float zVal){
		lastDir = new Vector3(xVal, 0, zVal).normalized;
	}

	public void setSelected(){
		isSelected = true;
	}

	public void unsetSelected(){
		isSelected = false;
	}

	void activate(){
		if (Type == ShiftType.Rotate) {
			Color newCol = new Color(0, 255, 0);
			renderer.material.color = newCol;
		}
		else if (Type == ShiftType.Translate) {
			Color newCol = new Color(255, 0, 0);
			renderer.material.color = newCol;
		}
	}

	void deactivate(){
		Color newCol = new Color(255, 255, 255);
		renderer.material.color = newCol;
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Activator"){
			activate();
			isActive = true;
		}

		if (collider.gameObject.tag == "Cursor"){
			if (isActive){
				cursor.AddSelf(this);
			}
		}
	}

	void OnTriggerExit(Collider collider){
		if (collider.gameObject.tag == "Activator"){
			deactivate();
			isActive = false;
		}

		if (collider.gameObject.tag == "Cursor"){
			cursor.RemoveSelf(this);
		}
	}
}

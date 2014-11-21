using UnityEngine;
using System.Collections;

public class _EnemyAI : MonoBehaviour {

	// The player object, in our case, the Oculus player's camera
	public GameObject player;
	// Initialize enemy speed
	public float enemySpeed = 0.0f;
	// How close player must go to enemy before it begins to chase it
	public float chaseDistance = 10.0f;
	// How far away enemy must be from start position before it stops moving
	public float stopDistance = 50.0f;
	// The target the enemy looks at or chases
	private Vector3 target;
	// The starting position of each AI object to calculate how far from origin it has travelled
	private Vector3 StartPosition;

	// Use this to initialize at start
	void Start () {
		// Setting the start position to the enemies current position at start
		StartPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// Setting AI to constantly move towards target (player), speed initially set at zero to refrain from movement
		gameObject.transform.position = Vector3.MoveTowards(transform.position, target, enemySpeed);
		// Within a certain distance from AI, it will look towards player, but remain at zero speed. A warning for player.
		if(Vector3.Distance(gameObject.transform.position, player.transform.position) < 30.0 && Vector3.Distance(gameObject.transform.position, player.transform.position) > chaseDistance){
			GetTarget();
			enemySpeed = 0.0f;
		}
		// When inside the chase distance, the AI will begin to follow the player target
		else if(Vector3.Distance(gameObject.transform.position, player.transform.position) < chaseDistance){
			GetTarget();
			enemySpeed = 0.05f;
		}
		// When the player is outside of the chase radius, and the enemy has left its chase perameter, it will halt in place.
		else if(Vector3.Distance(gameObject.transform.position, StartPosition) > stopDistance){
			enemySpeed = 0.0f;
		}
	}

	// Gets player target
	void GetTarget()
	{
		target = player.transform.position;
		target.y = transform.position.y;
		transform.LookAt(target);
	}
}

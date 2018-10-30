using System;
using UnityEngine;
using System.Collections;

// Controls enemy plane movement. enemy plane tracks the hero plane and tries to shoot it down
public class EnemyControllerAir : MonoBehaviour
{
	public float speed;
	public float tilt;
//	public Boundary boundary;
	public Vector3 rotation;
	public float ypos =50;
	public float turnangle =30;
	public float zMin, zMax, xMin, xMax, yMin, yMax;
	public bool stopmov = false;
	public int movdirection = 1;

	public Transform target;
	//public float range;

	[SerializeField]
	private float range = 10.0f;
	// the height we want the camera to be above the target
	[SerializeField]
	private float height = 5.0f;

	[SerializeField]
	private float rotationDamping;
	[SerializeField]
	private float heightDamping;

	public float responsetime = 2f;
	public float turntimer = 0f;
        public float currentRotationAngle;

        public float wantedRotationAngle;
	public Quaternion torotate;
	public float stepval=0.2f;
	float turnstep = 0;
	void Awake() {
	//	GetComponent<Rigidbody>().velocity = new Vector3 (1,0,0) * speed;
	}

	void FixedUpdate ()
	{
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");

		/*
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		//transform.position += transform.forward * Time.deltaTime * speed * moveVertical;

		GetComponent<Rigidbody>().position = new Vector3 
			(
				Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
				Mathf.Clamp (GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax), 
				Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
			);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);

		//GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
		//Quaternion oldrot = transform.rotation;
		//rotation = new Vector3(0,0,5) * Time.deltaTime * moveHorizontal  ;  

		//float angle = Quaternion.Angle(transform.rotation,oldrot);

		//Vector3 newvel = speed * new Vector3 (Mathf.Cos (angle), 0, Mathf.Sin (angle));
		//Vector3 direction = GetComponent<Rigidbody>().velocity.normalized;
		//Vector3 desiredDirection = new Vector3(5,0,0) * Time.deltaTime * moveHorizontal; // set this to the direction you want.
		//Vector3 newVelocity = newvel;
	//	GetComponent<Rigidbody> ().velocity = newVelocity;

		//GetComponent<Rigidbody>().AddRelativeTorque(0f, 5 * moveHorizontal *  GetComponent<Rigidbody>().mass, 0f);
		//GetComponent<Rigidbody>().AddForce(5 * transform.forward * moveVertical );

		MoveProcess ();
*/
		if (transform.GetComponent<EnemyDamage> () != null) {
			if (transform.GetComponent<EnemyDamage> ().Health <= 0) {
				stopmov = true;
			}
		}
		
		if (!stopmov) {
			//1 is towards z, 2 is towards x, 3 is -z, 4 is -x, 5 is zigzag
			if ((transform.position.z >= zMax) && movdirection == 1) {
				movdirection = 3;
				transform.Rotate (0, Time.deltaTime * turnangle, Time.deltaTime);
			}
			if (transform.position.z <= zMin && movdirection == 3) {
				movdirection = 1;
			}
		
				
			if ((movdirection == 1)) {
				transform.position += transform.forward * Time.deltaTime * speed;
			}

			if (movdirection == 3) {
				//transform.Rotate(0,0,Time.deltaTime * turnangle );
				transform.position -= transform.forward * Time.deltaTime * speed;

			}

			if (transform.position.z <= zMin) {
				transform.Rotate (0, -Time.deltaTime * turnangle, 0);

			}

			transform.position = new Vector3 (
				Mathf.Clamp (transform.position.x, xMin, xMax), 
				ypos, //always at same ht 
				Mathf.Clamp (transform.position.z, zMin, zMax)
			);
		}

		// Calculate the current rotation angles
//		wantedRotationAngle = target.eulerAngles.y;
//		float wantedHeight = target.position.y + height;
		
//		currentRotationAngle = transform.eulerAngles.y;
//		float currentHeight = transform.position.y;
		
	

		//turnstep will always be less than equal to 1. is used for slerp
		if (responsetime > 0) {
			turnstep = turntimer / responsetime;
		}

		if (target != null) {
		if ((Vector3.Distance (transform.position, target.position)) < range) {

			// Damp the rotation around the y-axis
			//when turntimer is 0 store the lookpos
			if (turntimer == 0) {

				Vector3	lookpos = target.position - transform.position;
				torotate = Quaternion.LookRotation (lookpos);
				transform.rotation = Quaternion.Slerp (transform.rotation, torotate, turnstep);
				//currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
				// Damp the height
//			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
				// Convert the angle into a rotation
				//Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
			
			}
			//subsequent rotate by delta
			else {
				transform.rotation = Quaternion.Slerp (transform.rotation, torotate, turnstep);
			}
			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			//ps transform.position = target.position;
			//transform.position -= currentRotation * Vector3.forward * distance;
			
			
			
			
			
			// Always look at the target
			//ps transform.LookAt(target);
			//instead of lookat probably do this
			// rotate to an angle after delay of few seconds
			//ps
			turntimer += Time.deltaTime;
			if (turntimer >= responsetime) {
				//transform.Rotate(0,(wantedRotationAngle-currentRotationAngle),0 );
// Set the height of the camera
				//transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);
				//transform.LookAt(target);
			
				turntimer = 0f;
			}			 


		}
	}
	}

	
	void OnCollisionEnter (Collision col)
	{
		// If it hits an enemy...
		if (col.transform.tag == "Player") {
			// ... find the Enemy script and call the Hurt function.
			//col.gameObject.GetComponent<Enemy> ().Hurt (1f);
			stopMoving();


			// Call the explosion instantiation.



		}

	}

	private void stopMoving() {
		stopmov = true;
	}



}



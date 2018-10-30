using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax, zMin, zMax;
}

//for player movement
public class PlayerControl : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	public Vector3 rotation;
	public float ypos =50;
	public float turnangle =30;

	void Awake() {
	//	GetComponent<Rigidbody>().velocity = new Vector3 (1,0,0) * speed;
		if (GameControl.gcontrol != null) {
			speed = GameControl.gcontrol.playerspeed;
		}
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
		//transform.LookAt (rotation);

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
		float h = CrossPlatformInputManager.GetAxis ("Horizontal");

//		 if(Input.GetKey(KeyCode.W)) {
			transform.position += transform.forward * Time.deltaTime * speed;
  //       }

//		if(Input.GetKey(KeyCode.S)) {
//			transform.position -= transform.forward * Time.deltaTime * speed;
//		}

		if(h > 0) {
			transform.Rotate(0,Time.deltaTime * turnangle,Time.deltaTime  );

         }

		if(h < 0) {
			transform.Rotate(0,-Time.deltaTime * turnangle,-Time.deltaTime );

		}

		transform.position = new Vector3 
			(
				Mathf.Clamp (transform.position.x, boundary.xMin, boundary.xMax), 
				ypos, //plane always at constant height 
				Mathf.Clamp (transform.position.z, boundary.zMin, boundary.zMax)
			);
	}

	private void MoveProcess()
	{
		}

	void OnTriggerEnter (Collider col) {
		if (col.transform.gameObject.name == "Terrain") {
			//GameControl.gcontrol.health -= 300;

			transform.gameObject.GetComponent<PlayerHealth> ().Hurt (3000);
			Debug.Log ("Ḧit Terrain");
		}
	}



}

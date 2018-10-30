using UnityEngine;
using System.Collections;
// this script controls how the enemy missile works. if hero plane deploys flare, enemy missile tracks and hits that.
//if no flare is deployed enemy missile will track the hero plane and hit it


public class AttackPlayerWithHeatSeeker : MonoBehaviour
{
	// The target we are following
	public Transform target;
	public Transform flare1;
	public Transform flare2;
	public bool heatSeeker=false;
	// The distance in the x-z plane to the target
	public float timeahead = 1f;
	//public GameObject plane;
	[SerializeField]
	private float distance = 10.0f;
	// the height we want the camera to be above the target
	[SerializeField]
	private float height = 0.0f;

	[SerializeField]
	private float rotationDamping;

	[SerializeField]
	private float heightDamping;



	[SerializeField]
	public Rigidbody bullet;
	[SerializeField]
	public float bulletSpeed;

	[SerializeField]
	public float bulletFireInterval = 1f;
	public float bullettimer = 0f;

	public Transform leftrocket;
	public Transform rightrocket;
	public float space= 5f;
	public float alternate = 0;
	public float planespeed;

	void Awake ()
	{
		if (GameControl.gcontrol != null) {
			planespeed = GameControl.gcontrol.playerspeed;
		}
	}


	// Update is called once per frame
	void LateUpdate()
	{
		// Early out if we don't have a target
		if (!target)
			return;


		// Calculate the current rotation angles
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;

		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;



		if ((Vector3.Distance(transform.position,target.position)) < distance) {

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, 1);

			// Damp the height
			//	currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation
			Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			//ps transform.position = target.position;
			//transform.position -= currentRotation * Vector3.forward * distance;




			// Set the height of the camera
			//transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);

			// Always look at the target

			Vector3 newpos = new Vector3 (target.position.x,target.position.y, target.position.z);

			newpos = new Vector3 (target.position.x , target.position.y, target.position.z ) + target.forward * planespeed * timeahead;
				transform.LookAt (newpos);
			//	alternate = 1;


			bullettimer += Time.deltaTime;

			if (bullettimer > bulletFireInterval) {
				// Fire once from left once from right
				if (alternate == 0) {
					if (leftrocket != null) {
						Rigidbody bulletInstance = Instantiate (bullet, leftrocket.position, transform.rotation) as Rigidbody;
						bulletInstance.velocity = leftrocket.forward * bulletSpeed;
						if (heatSeeker) {
							bulletInstance.GetComponent<HeatSeek> ().firsttarget = flare1;
							bulletInstance.GetComponent<HeatSeek> ().secondtarget = flare2;
							bulletInstance.GetComponent<HeatSeek> ().plane = target;
					
						}
					}
					alternate = 1;
				} else {
					if (rightrocket != null) {
						Rigidbody bulletInstance1 = Instantiate (bullet, rightrocket.position, transform.rotation) as Rigidbody;
						bulletInstance1.velocity = rightrocket.forward * bulletSpeed;
						if (heatSeeker) {
							bulletInstance1.GetComponent<HeatSeek> ().firsttarget = flare1;
							bulletInstance1.GetComponent<HeatSeek> ().secondtarget = flare2;
							bulletInstance1.GetComponent<HeatSeek> ().plane = target;

						}
					}
					alternate = 0;
				}
				bullettimer = 0f;

			}
		}
	}
}

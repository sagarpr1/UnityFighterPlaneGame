using UnityEngine; 
using System.Collections; 
using UltraReal.Utilities;
using UltraReal.WeaponSystem;
using UnityStandardAssets.CrossPlatformInput;

public class AttackTargetMouse : UltraRealMonobehaviorBase { 
	// The target we are following
	// The distance in the x-z plane to the target
	[SerializeField]
	private float distance = 10.0f;
	// the height we want the camera to be above the target
	[SerializeField]
	private float height = 5.0f;

	[SerializeField]
	private float rotationDamping;
	[SerializeField]
	private float heightDamping;

	/// <summary> 
	/// Reference to the launcher script
	/// </summary>
	private UltraRealLauncherBase launcher;

	/// <summary> 
	/// Finds the launcher script.
	/// </summary>
	protected override void OnStart ()
	{
		base.OnStart ();

		launcher = GetComponent<UltraRealLauncherBase> ();
	}


	// Update is called once per frame
	void LateUpdate()
	{

		//Ray ray =  (Input.mousePosition);
		// Early out if we don't have a target
		//if (!target)
		//	return;
		//Ray ray = new Ray(transform.position,transform.forward-Input.mousePosition );
		base.OnUpdate ();

		// Calculate the current rotation angles
		//var wantedRotationAngle = target.eulerAngles.y;
		//var wantedHeight = target.position.y + height;

//		var currentRotationAngle = transform.eulerAngles.y;
//		var currentHeight = transform.position.y;



//		if ((Vector3.Distance(transform.position,target.position)) < distance) {

			// Damp the rotation around the y-axis
//			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
//			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation
//			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			//ps transform.position = target.position;
			//transform.position -= currentRotation * Vector3.forward * distance;


		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 targetpoint = ray.GetPoint (100f);
		if (Physics.Raycast (ray, out hit, distance)) {
			Transform objectHit = hit.transform;
			transform.LookAt (objectHit);
			// Do something with the object that was hit by the raycast.
		} else {
			//transform.LookAt (transform.f);
		}

			// Set the height of the camera
//			transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);

			// Always look at the target
	//	transform.LookAt(Input.mousePosition);
		//Vector3 mousePositionVector3 = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);

		//mousePositionVector3 = transform.position - Input.mousePosition;
		//transform.LookAt (Input.mousePosition);
		//Vector3 targetdir = mousePositionVector3;// -transform.position;
		//transform.rotation= Quaternion.LookRotation(targetdir,Vector3.up);

		if (Input.GetButton ("Fire1") && launcher != null)
				launcher.Fire ();

			if (Input.GetKeyDown (KeyCode.R))
				launcher.Reload ();
//		}
	}
}

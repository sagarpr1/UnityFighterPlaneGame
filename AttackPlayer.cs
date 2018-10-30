using UnityEngine; 
using System.Collections; 
using UltraReal.Utilities;
using UltraReal.WeaponSystem;

public class AttackPlayer : UltraRealMonobehaviorBase { 
	// The target we are following
	[SerializeField]
	private Transform target;
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
		// Early out if we don't have a target
		if (!target)
			return;

		base.OnUpdate ();
		
		// Calculate the current rotation angles
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;
		
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
		


		if ((Vector3.Distance(transform.position,target.position)) < distance) {

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
			// Damp the height
			//currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
			// Convert the angle into a rotation
			//Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
			
			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			//ps transform.position = target.position;
			//transform.position -= currentRotation * Vector3.forward * distance;
			
			
			
			
			// Set the height of the camera
			//transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);
			
			// Always look at the target
			transform.LookAt(target);
			
			if (launcher != null)
				launcher.Fire ();
			
			if (Input.GetKeyDown (KeyCode.R))
				launcher.Reload ();
		}
	}
}

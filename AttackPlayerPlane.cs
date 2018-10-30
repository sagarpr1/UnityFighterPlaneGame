using UnityEngine; 
using System.Collections; 
using UltraReal.Utilities;
using UltraReal.WeaponSystem;

public class AttackPlayerPlane : UltraRealMonobehaviorBase { 
	// The target we are following
	[SerializeField]
	private Transform target;
	// The distance in the x-z plane to the target
	[SerializeField]
	private float distance = 10.0f;
	// the height we want the camera to be above the target

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
		
		if ((Vector3.Distance(transform.position,target.position)) < distance) {

			
			if (launcher != null)
				launcher.Fire ();
			
			if (Input.GetKeyDown (KeyCode.R))
				launcher.Reload ();
		}
	}
}


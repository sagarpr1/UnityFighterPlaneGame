using UnityEngine;
using System.Collections;
using UltraReal.Utilities;
using UltraReal.WeaponSystem;

public class AttackPlaneObj : UltraRealMonobehaviorBase
{
	// The target we are following
	[SerializeField]
	public GameObject target;
	// The distance in the x-z plane to the target
	[SerializeField]
	public float distance = 10.0f;

	public float currDist = 1000;
	/// <summary> 
	/// Reference to the launcher script
	/// </summary>
	private UltraRealLauncherBase launcher;

	// Use this for initialization
	protected override void OnStart ()
	{
		base.OnStart ();

		launcher = GetComponent<UltraRealLauncherBase> ();
	}


	// Update is called once per frame
	void Update()
	{
		// Early out if we don't have a target
		if (!target)
			return;

		base.OnUpdate ();

	
		currDist = Vector3.Distance (transform.position, target.transform.position);

		if ((Vector3.Distance(transform.position,target.transform.position)) < distance) {

	
			// Always look at the target
			transform.LookAt(target.transform);

			if (launcher != null)
				launcher.Fire ();

			if (Input.GetKeyDown (KeyCode.R))
				launcher.Reload ();
		}
	}
}




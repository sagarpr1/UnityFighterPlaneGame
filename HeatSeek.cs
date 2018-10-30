using UnityEngine;
using System.Collections;
//heatseek for missiles

public class HeatSeek : MonoBehaviour
{

	public float range;
	public Transform firsttarget; //first flare say right hand
	public Transform secondtarget; //second flare say left hand
	public Transform plane; // actual target
	public float bulletAlignInterval = 1f;
	public float bullettimer = 0f;
	public float speed;
	//canAlarm 1 means alarm to plane when it is close (reverse way to signal missile lock)
	public int canAlarm = 0;
	public float alarmRange=100f;
	public float alarmtimer=0;
	public float alarmInterval = 1f;
	public bool alarmOn=false;
	// Use this for initialization
	void Start ()
	{
		bullettimer = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{

		bullettimer += Time.deltaTime;

		//Check target only after a time interval
		if (bullettimer > bulletAlignInterval) {
			if (plane != null && firsttarget != null && secondtarget != null) {
				//distance between rocket & plane
				Vector3 diff = plane.position - transform.position;
				float approachDist = diff.magnitude;
				float firstFlareDist = range + 1;
				float secondFlareDist = range + 1;
				bool flare1Present = false;
				bool flare2Present = false;
				bool flaresAlreadyDetected = false;

				if (approachDist < range) {
					if (firsttarget.gameObject.activeSelf == true) {
						firstFlareDist = (firsttarget.position - transform.position).magnitude;
						flare1Present = true;
					}
					if (secondtarget.gameObject.activeSelf == true) {
						secondFlareDist = (secondtarget.position - transform.position).magnitude;
						flare2Present = true;
					}

					//if both flares are present follow the nearest one
					if (flare1Present && flare2Present) {
						if (secondFlareDist < firstFlareDist) {
							transform.LookAt (secondtarget);
						} else {
							transform.LookAt (firsttarget);
						}
					}
			//if only 1st present
			else if (flare1Present) {
						transform.LookAt (firsttarget);
					} 
			//if only second present
			else if (flare2Present) {
						transform.LookAt (secondtarget);
					}
			//if nothing is present
			else {
						transform.LookAt (plane);
						//code for alarm
						if (plane.GetComponent<WeaponSel> () != null) {
							//if alarm is already on or flare is on skip this
							if ((plane.GetComponent<WeaponSel> ().alarmOn == false) && !plane.GetComponent<WeaponSel>().flareOn) {
								if ((Vector3.Distance (transform.position, plane.position) < alarmRange) && (canAlarm == 1)) {
									alarmHeatLock ();
								}
							}
						}

					}
				}
		//if outside range only follow plane 
		else {
					transform.LookAt (plane);
						
				}
			}

			if (plane != null) {
				transform.GetComponent<Rigidbody> ().velocity = transform.forward * (speed + plane.GetComponent<PlayerControl> ().speed);
			}
			bullettimer = 0f;
		}
	}

	//alarm for 3 sec
	public void alarmHeatLock (){
		
		if (plane.GetComponent<WeaponSel> () != null) {
			//if flare image present & is not on
			if (plane.GetComponent<WeaponSel> ().img3 != null && !plane.GetComponent<WeaponSel>().flareOn) {
				/*alarmtimer += Time.deltaTime;

				if (alarmtimer > 2 * alarmInterval) {
					alarmtimer = 0;
				} else if (alarmtimer > alarmInterval) {
					plane.GetComponent<WeaponSel> ().img3.color = Color.yellow;
				} else {
					plane.GetComponent<WeaponSel> ().img3.color = Color.red;
				}
				*/
				//plane.GetComponent<WeaponSel> ().img3.color = Color.yellow;
				plane.GetComponent<WeaponSel> ().alarmOn = true;
				Debug.Log ("Flare On");
			}
		}
	}

	public void SeekNear()
	{

		/*
		// Find all the colliders on the Enemies layer within the bombRadius.
		Collider[] heatsource = Physics.OverlapSphere (transform.position, range, 1 << LayerMask.NameToLayer ("HeatSource"));

		float nearestheat = range + 1;
		Rigidbody rbtofollow =null;

		// For each collider...
		foreach (Collider en in heatsource) {
			// Check if it has a rigidbody (since there is only one per enemy, on the parent).
			Rigidbody rb = en.GetComponent<Rigidbody> ();
			//if (rb != null && rb.tag == "Enemy") {
		
			if(rb != null && rb.tag != "Player") {
			// Find the Enemy script and set the enemy's health to zero.
				//if (rb.gameObject.GetComponent<BasicDamage> () != null)
				//	rb.gameObject.GetComponent<BasicDamage> ().Health = -1;
				//else if (rb.gameObject.GetComponent<EnemyDamage> () != null)
				//	rb.gameObject.GetComponent<EnemyDamage> ().Health = -1;  

				// Find a vector from the bomb to the enemy.
				Vector3 deltaPos = rb.transform.position - transform.position;
				if (deltaPos.magnitude < nearestheat) {
					nearestheat = deltaPos.magnitude;
					rbtofollow = rb;
				}

				// Apply a force in this direction with a magnitude of bombForce.
				//Vector3 force = deltaPos.normalized * bombForce;
				//rb.AddForce (force);

			}

		}

		if (rbtofollow.transform != null) {
			transform.LookAt (rbtofollow.transform);
		}

		// Set the explosion effect's position to the bomb's position and play the particle system.
		//explosionFX.transform.position = transform.position;
		//explosionFX.Play ();

		// Instantiate the explosion prefab.
		//Instantiate (explosion, transform.position, Quaternion.identity);

		// Play the explosion sound effect.
		//		AudioSource.PlayClipAtPoint (boom, transform.position);

		// Destroy the bomb.
		//Destroy (gameObject);
		*/
	}

}



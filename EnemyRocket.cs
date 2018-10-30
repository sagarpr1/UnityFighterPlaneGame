using UnityEngine;
using System.Collections;

//script for enemy rocket (simple rocket with no heat seeking)
public class EnemyRocket : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.
	public float destroyafter = 5.0f;
//	public Transform player;
	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, destroyafter);
	}


	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}
	
	void OnTriggerEnter (Collider col) 
	{
		// If it hits an enemy...
		if (col.tag == "Player") {
			// ... find the Enemy script and call the Hurt function.
			//	col.gameObject.GetComponent<BasicDamage>().OnDamaged(10);

			// Call the explosion instantiation.
			OnExplode ();
			col.gameObject.GetComponent<PlayerHealth> ().Hurt (100);


			// Destroy the rocket.
			Destroy (gameObject);
		} else if (col.tag == "RocketGuide") {
			Debug.Log ("Rocketguide");
		}
		
		// Otherwise if the player manages to shoot himself...
		else if((col.tag != "Enemy") || (col.tag != "RocketGuide"))
		{
			// Instantiate the explosion and destroy the rocket.
			OnExplode();
			Destroy (gameObject);
		}
	}


}


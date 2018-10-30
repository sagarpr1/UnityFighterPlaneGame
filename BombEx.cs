using UnityEngine; 
using System.Collections; 


	public class BombEx: MonoBehaviour
	{
		
	public GameObject explosion;		// Prefab of explosion effect.
	public float bombRadius = 10f;			// Radius within which enemies are killed.
	public float bombForce = 100f;			// Force that enemies are thrown from the blast.
	public float destroyafter = 3;
	public AudioClip boom;					// Audioclip of explosion.
	public ParticleSystem explosionFX;		// Reference to the particle system of the explosion effect.
	public float damageAmount=1000f;
	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, destroyafter);
	}

	void OnCollisionEnter (Collision col)
	{
		// If it hits an enemy...
		if (col.transform.tag != "Player" && col.transform.tag != "Bomb") {
			// ... find the Enemy script and call the Hurt function.
			//col.gameObject.GetComponent<Enemy> ().Hurt (1f);


			// Call the explosion instantiation.
			Explode ();

			// Destroy the rocket.
			Destroy (gameObject);
		}

	}

	public void Explode()
	{
		// Find all the colliders on the Enemies layer within the bombRadius.
		Collider[] enemies = Physics.OverlapSphere (transform.position, bombRadius, 1 << LayerMask.NameToLayer ("Enemies"));

		// For each collider...
		foreach (Collider en in enemies) {
			// Check if it has a rigidbody (since there is only one per enemy, on the parent).
			Rigidbody rb = en.GetComponent<Rigidbody> ();
			if (rb != null && rb.tag == "Enemy") {
				// Find the Enemy script and set the enemy's health to zero.
				if (rb.gameObject.GetComponent<EnemyDamage> () != null)
					rb.gameObject.GetComponent<EnemyDamage> ().OnDamaged (damageAmount);
				else if (rb.gameObject.GetComponent<BasicDamage> () != null)
					rb.gameObject.GetComponent<BasicDamage> ().Health = -1;
				

				// Find a vector from the bomb to the enemy.
				Vector3 deltaPos = rb.transform.position - transform.position;

				// Apply a force in this direction with a magnitude of bombForce.
				Vector3 force = deltaPos.normalized * bombForce;
				rb.AddForce (force);

			} else if (en.GetComponent<Transform> ().tag == "Enemy") {
				if (en.transform.GetComponent<EnemyDamage> () != null) {
					en.transform.GetComponent<EnemyDamage> ().OnDamaged (damageAmount);
				}
			} else if (en.GetComponent<Transform> ().tag != "Player") {
				if (en.transform.GetComponent<EnemyDamage> () != null) {
					en.transform.GetComponent<EnemyDamage> ().OnDamaged (damageAmount);
				}
			}
		}

		// Set the explosion effect's position to the bomb's position and play the particle system.
		explosionFX.transform.position = transform.position;
		explosionFX.Play ();

		// Instantiate the explosion prefab.
		Instantiate (explosion, transform.position, Quaternion.identity);

		// Play the explosion sound effect.
		AudioSource.PlayClipAtPoint (boom, transform.position);

		// Destroy the bomb.
		Destroy (gameObject);

	}
	}



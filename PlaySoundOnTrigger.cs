using UnityEngine;
using System.Collections;

public class PlaySoundOnTrigger : MonoBehaviour
{
	public GameObject plane;
	public AudioSource audioSound;
	public AudioSource mainMusic;
	public float range;
	public bool musicOn = false;
	public float dist;

	// Use this for initialization
	void Start ()
	{
		if (audioSound != null) {
			audioSound.Stop ();
		}
		Debug.Log ("Parent is " + transform.gameObject.name);
	}
	
	// Update is called once per frame
	void Update ()
	{
		dist = Vector3.Distance(transform.position, plane.transform.position);
		if ((Vector3.Distance (transform.position, plane.transform.position) <= range) && (transform.parent.gameObject.activeInHierarchy) && (!musicOn)) {
			// Play the explosion sound effect.

			Debug.Log("Dist is " + dist);
			mainMusic.volume= 0.1f;
			audioSound.Play ();
			musicOn = true;
		} else if (musicOn && !transform.parent.gameObject.activeInHierarchy) {
		//	mainMusic.UnPause ();
			audioSound.Stop ();
			musicOn = false;
		}
		else if ((Vector3.Distance (transform.position, plane.transform.position) > range))
		{
			audioSound.Stop ();
			musicOn = false;
		}
	}


}


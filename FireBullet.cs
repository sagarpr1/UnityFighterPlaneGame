using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//script for firing bullets
public class FireBullet : MonoBehaviour
{
	public float firetime;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Fire()
	{
		GameObject obj = ObjectPoolerScript.current.GetPooledObject ();

		if (obj == null)
			return;

		obj.transform.position = transform.position;
		obj.transform.rotation = transform.rotation;
		obj.SetActive (true);
	}


}


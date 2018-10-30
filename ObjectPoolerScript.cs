using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolerScript : MonoBehaviour
{
	public static ObjectPoolerScript current;
	public GameObject pooledObject;
	public int pooledAmount = 20;
	public bool willGrow = true;
	List<GameObject> pooledObjectslist;
	// Use this for initialization


	void Awake()
	{
		current = this;
	}
	void Start ()
	{
		pooledObjectslist = new List<GameObject> ();

		for (int i = 0; i < pooledAmount; i++) {
			GameObject obj = (GameObject)Instantiate (pooledObject);
			obj.SetActive (false);
			pooledObjectslist.Add (obj);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public GameObject GetPooledObject() {
		for (int i = 0; i < pooledObjectslist.Count; i++) {
			if (!pooledObjectslist [i].activeInHierarchy) {
				return(pooledObjectslist [i]);
			}
		}

		if (willGrow) {
			GameObject obj = (GameObject)Instantiate (pooledObject);
			pooledObjectslist.Add (obj);
			return obj;
		}

		return null;
	}
}


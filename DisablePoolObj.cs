﻿using UnityEngine;
using System.Collections;

public class DisablePoolObj : MonoBehaviour
{

	void OnEnable() {
		Invoke ("Destroy", 2f);
	}

	void Destroy() {
		gameObject.SetActive (false);
	}

	void OnDisable() {
		CancelInvoke ();
	}
}


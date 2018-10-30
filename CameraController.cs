using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


// Camera control for hero plane. can switch to see what is behind the hero plane or in front of the plane
public class CameraController : MonoBehaviour
{

	public Transform target;
	public float followdist = 0;
	public float followht = 0;
	public float anglex=0;
	public float angley=0;
	public float anglez=0;
	public float rotateby = 0;
	public bool backview = true;
	public float movpos =40;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (target == null)
			return;
			
		if (target != null) {
			//transform.position = new Vector3 (target.position.x, target.position.y + followht, target.position.z - followdist);
		}

		if (CrossPlatformInputManager.GetButtonDown ("RotateCam")) {
			transform.Rotate (anglex, rotateby, 0);

		}

		if (CrossPlatformInputManager.GetButtonDown ("MoveCam")) {
			if (GameControl.gcontrol.currentlevel == "Level3") {
				movpos = 50;
			} else {
				movpos = 40;
			}


			if (backview) {
				
				transform.localPosition = transform.localPosition + new Vector3 (0, 0, movpos);
					backview = false;
				} else {
					transform.localPosition = transform.localPosition - new Vector3 (0, 0, movpos);
					backview = true;
				}


		}


	}
}


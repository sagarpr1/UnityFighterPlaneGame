using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

// code for dropping bomb
public class DropBombs : MonoBehaviour
{
	//	[HideInInspector]
	//	public bool bombLaid = false;		// Whether or not a bomb has currently been laid.
	public int bombCount = 1000;			// How many bombs the player has.
	//	public AudioClip bombsAway;			// Sound for when the player lays a bomb.
	public Rigidbody bomb;				// Prefab of the bomb.
	public GameObject plane;
	public float bombspeed=1;
	public float bombtimer = 0f;
	public float dropinterval = 1f;
	public bool canAim = false;
	public AudioClip missileLaunch;
	private Vector3 screenPoint;
	private Vector3 offset;
	public float maxTargetDist = 500f;
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	public float projFinlSpeed = 0;
	public bool selectControl=false;
	public Camera fpsCam;
	public float verticalSpeed = 0f;
	public GameObject hitpoint;
	public float cursorTimer=0;
	public float cursorVisibleTime=1;
	//public Texture2D cursorTexture;
	//public CursorMode cursorMode = CursorMode.Auto;


	//public RectTransform Target;

	//	private GUITexture bombHUD;			// Heads up display of whether the player has a bomb or not.


	void Awake ()
	{
		// Setting up the reference.
		//		bombHUD = GameObject.Find("ui_bombHUD").GetComponent<GUITexture>();
		if (canAim) {
		//	Cursor.visible = true;
			//Cursor.SetCursor (cursorTexture, hotSpot, cursorMode);
			if (hitpoint != null) {
				hitpoint.SetActive (false);
			}
		}
	}


	void Update ()
	{
		// If the bomb laying button is pressed, the bomb hasn't been laid and there's a bomb to lay...
		//if(CrossPlatformInputManager.GetButtonDown("Fire2") && !bombLaid && bombCount > 0)
		if (bombtimer <= dropinterval) {
			bombtimer += Time.deltaTime;
		}
		cursorTimer += Time.deltaTime;

		if (cursorTimer > cursorVisibleTime) {
			cursorTimer = 0;

		//	Cursor.SetCursor (null, Vector2.zero, cursorMode);
			if (hitpoint != null) {
				hitpoint.SetActive (false);
			}
		}
		/*
		if (Input.GetButtonDown ("Fire 1")) {
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
			Screen.showCursor = false;
		}

		if (Input.GetButton ("Fire 1")) {
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			//transform.position = curPosition;
			transform.LookAt(curPosition);
			bombCount--;

			// Set bombLaid to true.
			//			bombLaid = true;

			// Play the bomb laying sound.
			//			AudioSource.PlayClipAtPoint(bombsAway,transform.position);

			// Instantiate the bomb prefab.
			Rigidbody bombinst = Instantiate (bomb, transform.position, transform.rotation) as Rigidbody;
			bombinst.velocity = transform.forward * (plane.GetComponent<PlayerControl> ().speed + bombspeed);
			bombtimer = 0;
			//		StartCoroutine (DelayDestroy ());

		}

		if (Input.GetButtonUp ("Fire 1")) {
			Screen.showCursor=true;

		}
		*/
		//fpsCam = GetComponentInParent<Camera> ();
		selectControl = false;
		if (CrossPlatformInputManager.GetButton ("Select1") || CrossPlatformInputManager.GetButton ("Select2") || CrossPlatformInputManager.GetButton ("Select3") || CrossPlatformInputManager.GetButton ("RotateCam")) {
			selectControl = true;
		}

		float h = CrossPlatformInputManager.GetAxis ("Horizontal");
		//stop counter if bombtimer count is done

		if (Input.GetButton ("Fire1") && (h==0) && (selectControl==false) && (bombCount > 0) && (bombtimer > dropinterval)) {

			/*
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Vector3 targetpoint = ray.GetPoint (100f);
			if (Physics.Raycast (ray, out hit, maxTargetDist)) {
				Transform objectHit = hit.transform;
				transform.LookAt (objectHit);
				// Do something with the object that was hit by the raycast.
			} else {
				//transform.LookAt (transform.f);
			}
			*/
			//Cursor.visible = true;

			if (canAim) {


				Vector3 rayOrigin = Camera.main.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, transform.position.z));
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				//Ray planeray = Camera.main.ScreenToWorldPoint (transform.position);
				RaycastHit hit;

			//	laserLine.SetPosition (0, gunEnd.position);

				if (Physics.Raycast (ray, out hit, maxTargetDist))
			//	if (Physics.Raycast (rayOrigin, Camera.main.transform.forward, out hit, maxTargetDist))
				{
					//laserLine.SetPosition (1, hit.point);
					Debug.Log("Ray hit");
					transform.LookAt (hit.point);

					//if(hit.distance < Vector3.Distance
					//Cursor.SetCursor (cursorTexture, Vector2.zero, cursorMode);

					if (hitpoint != null) {
						hitpoint.transform.position = new Vector3(hit.point.x,hit.point.y,hit.point.z);
						hitpoint.SetActive (true);
					}
				}
				else
				{
					//laserLine.SetPosition (1, rayOrigin + (fpsCam.transform.forward * weaponRange));
					//transform.LookAt(rayOrigin + (fpsCam.transform.forward * maxTargetDist));
					Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, maxTargetDist);
						Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);
					Debug.Log("No Ray hit");
						transform.LookAt (curPosition);
					//Cursor.SetCursor (cursorTexture, Vector2.zero, cursorMode);

					if (hitpoint != null) {
						hitpoint.transform.position = new Vector3(curPosition.x,curPosition.y,curPosition.z);
						hitpoint.SetActive (true);
					}
				}



				//Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, maxTargetDist);
			//	Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);

			//	transform.LookAt (curPosition);
				projFinlSpeed = plane.GetComponent<PlayerControl> ().speed  + bombspeed;
			}
			//if no targeting allowed
			else {
				projFinlSpeed = plane.GetComponent<PlayerControl> ().speed + bombspeed;
			}

			// Decrement the number of bombs.
		
			bombCount--;

			// Set bombLaid to true.
			//			bombLaid = true;

			// Play the bomb laying sound.
			//			AudioSource.PlayClipAtPoint(bombsAway,transform.position);

			// Instantiate the bomb prefab.
			Rigidbody bombinst = Instantiate (bomb, transform.position, transform.rotation) as Rigidbody;
			bombinst.velocity = transform.forward * (projFinlSpeed) + (-transform.up)*(verticalSpeed);
			//if missile play missile sound
			if (canAim) {
				// Play the explosion sound effect.
				AudioSource.PlayClipAtPoint (missileLaunch, transform.position);

			}
			bombtimer = 0;
			//		StartCoroutine (DelayDestroy ());
		}

		if (Input.GetButtonUp ("Fire1")) {
			//Screen.showCursor=true;
			//Cursor.visible = true;

		}


	}
	//	private IEnumerator DelayDestroy()
	//	{
	//		yield return new WaitForSeconds(5);
	//	}


	// The bomb heads up display should be enabled if the player has bombs, other it should be disabled.
	//		bombHUD.enabled = bombCount > 0;



}

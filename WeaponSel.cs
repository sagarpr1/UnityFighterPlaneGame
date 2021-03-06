using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class WeaponSel : MonoBehaviour
{
//	[HideInInspector]
//	public bool bombLaid = false;		// Whether or not a bomb has currently been laid.
	public GameObject weapon1 ;			// How many bombs the player has.
	public GameObject weapon2 ;			// How many bombs the player has.
//	public GameObject weapon3 ;			// How many bombs the player has.
	public int numFlares=1000;
	public GameObject flare;
	public GameObject flare1;
	public GameObject flare2;
	public AudioSource mainMusic;
	public AudioSource alarmMusic;
	public AudioClip flareSound;
	public AudioClip alarmSound;
	public float flareOnDuration = 10f;
	public float flaretimer = 0f;
	public bool flareOn = false;
	public bool alarmOn = false; //alarm of flare is already on when true
//	public AudioClip bombsAway;			// Sound for when the player lays a bomb.
	public Image img1;
	public Image img2;
	public Image img3;
	public float alarmtimer = 0;
	public float alarmOnDuration = 2;
	public bool alarmPlayed=false;
//	private GUITexture bombHUD;			// Heads up display of whether the player has a bomb or not.


	void Awake ()
	{
		// Setting up the reference.
//		bombHUD = GameObject.Find("ui_bombHUD").GetComponent<GUITexture>();
		weapon1.SetActive(false);
		weapon2.SetActive(true);
//		weapon3.SetActive(false);
		flare1.SetActive(false);
		flare2.SetActive(false);
		flaretimer = 0;
		img1.GetComponent<Image>().color = Color.red;
		img2.GetComponent<Image>().color = Color.blue;
		img3.GetComponent<Image>().color = Color.red;
	}


	void Update ()
	{
		// If the bomb laying button is pressed, the bomb hasn't been laid and there's a bomb to lay...
		//if(CrossPlatformInputManager.GetButtonDown("Fire2") && !bombLaid && bombCount > 0)

		if (CrossPlatformInputManager.GetButtonDown ("Select1")) {
			// Decrement the number of bombs.
			weapon1.SetActive(true);
			weapon2.SetActive(false);
			//weapon3.SetActive(false);
			//flare1.SetActive(false);
			//flare2.SetActive(false);
			img1.GetComponent<Image>().color = Color.blue;
			img2.GetComponent<Image>().color = Color.red;
			//img3.GetComponent<Image>().color = Color.red;
		}
		else if (CrossPlatformInputManager.GetButtonDown ("Select2")) {
			// Decrement the number of bombs.
			weapon2.SetActive(true);
			weapon1.SetActive(false);
			//weapon3.SetActive(false);
			//flare1.SetActive(false);
			//flare2.SetActive(false);

			img1.GetComponent<Image>().color = Color.red;
			img2.GetComponent<Image>().color = Color.blue;
			//img3.GetComponent<Image>().color = Color.red;

		}
		else if (CrossPlatformInputManager.GetButtonDown ("Select3")) {
			// Decrement the number of bombs.
			//weapon3.SetActive(true);
		
			if (numFlares > 0) {
				flare.transform.position = transform.position;
				flare.transform.rotation = transform.rotation;

				flare1.SetActive (true);
				flare2.SetActive (true);
				numFlares--;
			}
			// Play the explosion sound effect.
			mainMusic.Pause();
			AudioSource.PlayClipAtPoint (flareSound, transform.position);
			//mainMusic.UnPause ();

			img3.GetComponent<Image>().color = Color.blue;
			flareOn = true;
			alarmOn = false;


			//weapon1.SetActive(false);
			//weapon2.SetActive(false);
			//img1.GetComponent<Image>().color = Color.red;
			//img2.GetComponent<Image>().color = Color.red;

		}

		if (flareOn) {
			flaretimer += Time.deltaTime;
			if (flaretimer > flareOnDuration) {

				flare1.SetActive (false);
				flare2.SetActive (false);
				//flare1.transform.
				img3.GetComponent<Image>().color = Color.red;
				flaretimer = 0;
				flareOn = false;
			}
		}

		if (alarmOn) {
			alarmtimer += Time.deltaTime;

			if (alarmtimer > alarmOnDuration) {
				alarmtimer = 0;
				img3.GetComponent<Image> ().color = Color.red;
				alarmOn = false;
				alarmPlayed = false;
			} else {
				img3.GetComponent<Image> ().color = Color.yellow;
				// Play the explosion sound effect.
				if (!alarmPlayed) {
					mainMusic.Pause ();
					//AudioSource.PlayClipAtPoint (alarmSound, transform.position);
					alarmMusic.Play();
					//		mainMusic.UnPause();
					alarmPlayed = true;
				}

			}
		} else {
					mainMusic.UnPause();

		}
			


			

			// Set bombLaid to true.
//			bombLaid = true;

			// Play the bomb laying sound.
//			AudioSource.PlayClipAtPoint(bombsAway,transform.position);
		//		StartCoroutine (DelayDestroy ());


	}
	//	private IEnumerator DelayDestroy()
	//	{
	//		yield return new WaitForSeconds(5);
	//	}


		// The bomb heads up display should be enabled if the player has bombs, other it should be disabled.
//		bombHUD.enabled = bombCount > 0;



}

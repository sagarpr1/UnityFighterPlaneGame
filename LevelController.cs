using UnityEngine;
using System.Collections;

//Level controller. each level will end when main enemies are detroyed. if hero plane is destroyed, game has to be started again

public class LevelController : MonoBehaviour
{


	public GameObject enemy1;
	public GameObject enemy2;

	public GameObject player;

	public GameObject gameOverMsg;
	public GameObject gameWonMsg;
	public GameObject gameStartMsg;

	public bool enemy1present=false;
	public bool enemy2present=false;
	public bool playerpresent=true;
	public bool startGame = true;
	public bool chkNumEnemyDestr=false;
	public int numEnemyToDestroy=0;
	public int numEnemyDestroyed=0;
	public int numEnemyAlreadyDest=0;
	public bool EnemyAirAIPresent=false;
	public GameObject EnemyAIPlane;
	public float EnemyAIspeed=0;
	private bool isIntCached;
	public bool banneradloaded;
	public bool interadloaded;
	public int numtimesIntershown = 0;

	void Awake(){
		if (enemy1 != null) {
			enemy1present = true;
		} else {
			enemy1present = false;
		}
		if (enemy2 != null) {
			enemy2present = true;
		} else {
			enemy2present = false;
		}

		if (player != null) {
			playerpresent = true;
		} else {
			playerpresent = false;
		}

		gameOverMsg.SetActive (false);
		gameWonMsg.SetActive (false);
		gameStartMsg.SetActive (true);
		Time.timeScale = 0;
		startGame = true;
	banneradloaded = false;
	interadloaded = false;
	}
	// Use this for initialization
	void Start ()
	{
		gameStartMsg.SetActive (true);
		Time.timeScale = 0;
		GetComponent<PlayerControl>().enabled = false;

		if (EnemyAirAIPresent) {
			if (EnemyAIPlane != null) {
			//	EnemyAIspeed = EnemyAIPlane.transform.GetComponent<EnemyPlaneAI> ().currentSpeed;
				EnemyAIPlane.transform.GetComponent<EnemyPlaneAI> ().currentSpeed = 0;
			}
		}

		if (chkNumEnemyDestr) {
			if (GameControl.gcontrol != null) {
				numEnemyAlreadyDest = GameControl.gcontrol.numEnemyDestroyed;
			}
			numEnemyToDestroy = numEnemyAlreadyDest + numEnemyToDestroy;
		}

		numtimesIntershown = PlayerPrefs.GetInt("numtimeInterstitialshown");
	}
	
	// Update is called once per frame
	void Update ()
	{

		// Starting the game : A pause is displayed which is enabled when Start is Pressed
		if (startGame) {
			Time.timeScale = 0;
			GetComponent<PlayerControl>().enabled = false;
			if (EnemyAirAIPresent) {
				if (EnemyAIPlane != null) {
			//		EnemyAIspeed = EnemyAIPlane.transform.GetComponent<EnemyPlaneAI> ().currentSpeed;
					EnemyAIPlane.transform.GetComponent<EnemyPlaneAI> ().currentSpeed = 0;
				}
			}
		} else {
			Time.timeScale = 1;
			GetComponent<PlayerControl>().enabled = true;
			if (EnemyAirAIPresent) {
				if (EnemyAIPlane != null) {
					
					EnemyAIPlane.transform.GetComponent<EnemyPlaneAI> ().currentSpeed = GameControl.gcontrol.playerspeed;
				}
			}
		}

		// Game lose and win 
		if (playerpresent && (GameControl.gcontrol.health <= 0)) { // was present now it is destroyed means game lost
			//GameControl.gcontrol.health = GameControl.gcontrol.maxHealth;
			if (!gameWonMsg.activeInHierarchy) {
				gameOverMsg.SetActive (true);

				//ReloadGame1 ();
				StartCoroutine ("ReloadGame");
			}
		} else {
			//if enemies were present and now destroyed you won
			if (chkNumEnemyDestr) {
				if (GameControl.gcontrol.numEnemyDestroyed >= numEnemyToDestroy) {
					if (!gameOverMsg.activeInHierarchy) {
						gameWonMsg.SetActive (true);
					//set some of the global save variables
					GameControl.gcontrol.goldcoins += 200;
					if (GameControl.gcontrol.currentlevel == "Level1") {
					GameControl.gcontrol.levelunlocked = 2;
					} else if (GameControl.gcontrol.currentlevel == "Level2") {
					GameControl.gcontrol.levelunlocked = 3;
					}


						//showInterstitial ();
							StartCoroutine("GameRate");
					}

				}
			} else {
				if (enemy1present && enemy2present) {
					if ((enemy1.activeInHierarchy == false) && (enemy2.activeInHierarchy == false)) {
						//		GameControl.gcontrol.health = GameControl.gcontrol.maxHealth;
						if (!gameOverMsg.activeInHierarchy) {
							gameWonMsg.SetActive (true);
							//set some of the global save variables
							GameControl.gcontrol.goldcoins += 200;
							if (GameControl.gcontrol.currentlevel == "Level1") {
							GameControl.gcontrol.levelunlocked = 2;
							} else if (GameControl.gcontrol.currentlevel == "Level2") {
							GameControl.gcontrol.levelunlocked = 3;
							}
							//showInterstitial ();

							StartCoroutine("GameRate");


						}
					}
				}
			//if only one enemy was there and you destrooyed it you won
			else if (enemy1present && !enemy2present) {
					if (enemy1.activeInHierarchy == false) {
						//		GameControl.gcontrol.health = GameControl.gcontrol.maxHealth;
						if (!gameOverMsg.activeInHierarchy) {
							gameWonMsg.SetActive (true);
						//set some of the global save variables
						GameControl.gcontrol.goldcoins += 200;
						if (GameControl.gcontrol.currentlevel == "Level1") {
						GameControl.gcontrol.levelunlocked = 2;
						} else if (GameControl.gcontrol.currentlevel == "Level2") {
						GameControl.gcontrol.levelunlocked = 3;
						}

							//showInterstitial ();
							//if (GameControl.gcontrol.currentlevel != "Level4") {
							//StartCoroutine ("ReloadGame");
							//ReloadGame1();
							//}
							//else {
							StartCoroutine("GameRate");
							//}
						}
					}
				}

			}
		}

	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		//Time.timeScale = 1;
		GetComponent<PlayerControl>().enabled = false;
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		//Application.LoadLevel(Application.loadedLevel);
		//Time.timeScale = 1;
		//
		        if (numtimesIntershown == 0) {
			//Appodeal.show (Appodeal.INTERSTITIAL);

			PlayerPrefs.SetInt ("numtimeInterstitialshown", numtimesIntershown);
			Debug.Log ("enter1");
			}
		Debug.Log ("enterbetw");
		numtimesIntershown++;
			if (numtimesIntershown >= 3) {
				numtimesIntershown =0;
				PlayerPrefs.SetInt ("numtimeInterstitialshown", numtimesIntershown);
			Debug.Log ("enter2");
			}


			Application.LoadLevel ("main");
	}



	IEnumerator GameRate()
	{			
		// ... pause briefly
		//Time.timeScale = 1;
		GetComponent<PlayerControl>().enabled = false;
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		//Application.LoadLevel(Application.loadedLevel);

		//int alreadyRated = 0;

		// if already rated do not need rating scene
		if (PlayerPrefs.GetInt ("alreadyRated") == 1) {
			Application.LoadLevel ("main");
		} else {
			Application.LoadLevel ("thanks");
		}
	}


	public void StarttheGame() {
		gameStartMsg.SetActive (false);
		startGame = false;
		Time.timeScale = 1;
		GetComponent<PlayerControl>().enabled = true;
		if (EnemyAirAIPresent) {
			if (EnemyAIPlane != null) {
		//		EnemyAIspeed = EnemyAIPlane.transform.GetComponent<EnemyPlaneAI> ().currentSpeed;
				EnemyAIPlane.transform.GetComponent<EnemyPlaneAI> ().currentSpeed = GameControl.gcontrol.playerspeed;
			}
		}
	}



}


using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour {

	public GameObject Level2Unlocked;
	public GameObject Level2Locked;

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	public void Start(){
		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.Load ();

			if (Level2Locked != null && Level2Unlocked != null) {
				if (GameControl.gcontrol.levelunlocked == 0) {
					Level2Locked.SetActive (true);
					Level2Unlocked.SetActive (false);

				} else if (GameControl.gcontrol.levelunlocked >= 1) {
					Level2Locked.SetActive (false);
					Level2Unlocked.SetActive (true);
				}
			}

		}
	}

	public void LoadLevel1(){
		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.health = GameControl.gcontrol.maxHealth;
			GameControl.gcontrol.currentlevel = "Level1";   
		}
		Application.LoadLevel ("MissionBrief");
	}

	public void LoadLevel2(){
		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.health = GameControl.gcontrol.maxHealth;
			GameControl.gcontrol.currentlevel = "Level2";   
		}
		Application.LoadLevel ("MissionBrief");
	}

	public void LoadLevel3(){
		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.health = GameControl.gcontrol.maxHealth;
			GameControl.gcontrol.currentlevel = "Level3";   
		}
		Application.LoadLevel ("MissionBrief");
	}

	public void LoadLevel4(){
		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.health = GameControl.gcontrol.maxHealth;
			GameControl.gcontrol.currentlevel = "Level4";   
		}
		Application.LoadLevel ("MissionBrief");
	}


	public void SaveScore(){
		Application.LoadLevel("savescore");
	}



	public void GoToMain() {
	
		//if (GameControl.gcontrol != null) {
		//	GameControl.gcontrol.Save ();
		//}
		Application.LoadLevel ("main");
	}

	public void GoToHelp() {
		Application.LoadLevel ("help");
	}


	public void GoToSettings() {
		Application.LoadLevel ("settings");
	}

	public void SetSpeed1() {
		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.playerspeed = 150;
			GameControl.gcontrol.SavePref ();
		}

		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.Save ();

		}
	}

	public void SetSpeed2() {
		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.playerspeed = 300;
			GameControl.gcontrol.SavePref ();
		}

		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.Save ();
		}
	}

	public void SetSpeed3() {
		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.playerspeed = 200;
		}

		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.Save ();
		}
			
	}

	public void SetSpeed4() {
		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.playerspeed = 300;
		}

		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.Save ();
		}

	}

	public void SetDifficultyLevel1() {
		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.difficultylevel = 1;
		}

		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.Save ();
		}
	}

	public void SetDifficultyLevel2() {
		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.difficultylevel = 2;
		}

		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.Save ();
		}
	}

	public void RateUs() {
		PlayerPrefs.SetInt ("alreadyRated", 1);
		Application.OpenURL ("market://details?id=" + Application.bundleIdentifier);


	}
}

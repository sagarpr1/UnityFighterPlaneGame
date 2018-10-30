using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Game controller
public class GameControl : MonoBehaviour
{
	public static GameControl gcontrol;
	public float health=0;
	public float experience;
	public float maxHealth = 500;
	public int numEnemyDestroyed = 0;
	public string currentlevel = "main";
	public int goldcoins = 0;
	public int numtimeInterstitialshown=0;
	public int levelunlocked = 3;
	public float playerspeed = 150;
	public int difficultylevel = 1;

	// Use this for initialization
	void Awake ()
	{
		if (gcontrol == null) {
			DontDestroyOnLoad (gameObject);
			gcontrol = this;
		} else if (gcontrol != this) {
			Destroy (gameObject);
		}
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void SavePref() {
		PlayerPrefs.SetFloat ("playerspeed", playerspeed);
		PlayerPrefs.SetInt ("numEnemyDestroyed", numEnemyDestroyed);
		PlayerPrefs.SetInt ("numtimeInterstitialshown", numtimeInterstitialshown);
		PlayerPrefs.SetInt ("levelunlocked", levelunlocked);

	}
	public void LoadPref() {
		playerspeed = PlayerPrefs.GetFloat ("playerspeed");
		numEnemyDestroyed = PlayerPrefs.GetInt ("numEnemyDestroyed");
		numtimeInterstitialshown = PlayerPrefs.GetInt ("numtimeInterstitialshown");
		levelunlocked = PlayerPrefs.GetInt ("levelunlocked");
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;
	//	if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
		 file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
		//File.
		PlayerData data = new PlayerData ();
		data.health = health;
		data.experience = experience;
		data.levelunlocked = levelunlocked;
		data.numEnemyDestroyed = numEnemyDestroyed;
		data.goldcoins = goldcoins;
		data.numtimesInterstitialShown = numtimeInterstitialshown;
		data.playerspeed = playerspeed;
		data.difficultylevel = difficultylevel;
		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load() {
		if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData) bf.Deserialize (file);
			file.Close ();
			health = data.health;
			experience = data.experience;
			numEnemyDestroyed = data.numEnemyDestroyed;
			levelunlocked = data.levelunlocked;
			goldcoins = data.goldcoins;
			numtimeInterstitialshown = data.numtimesInterstitialShown;
			playerspeed = data.playerspeed;
			difficultylevel = data.difficultylevel;
		
		}
}
}

[Serializable]
class PlayerData
{
	public float health;
	public float experience;
	public int numEnemyDestroyed;
	public int goldcoins;
	public int numtimesInterstitialShown;
	public int levelunlocked;
	public float playerspeed;
	public int difficultylevel;

}


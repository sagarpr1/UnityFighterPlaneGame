using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypeLetter : MonoBehaviour
{
	public float letterPause = 0.05f;
	//public AudioClip typeSound1;
	//public AudioClip typeSound2;
	public string levelname ="Level1";

	string message;
	Text textComp;

	// Use this for initialization
	void Start ()
	{
		textComp = GetComponent<Text>();
		if (GameControl.gcontrol != null) {
			GameControl.gcontrol.health = GameControl.gcontrol.maxHealth;
			levelname = GameControl.gcontrol.currentlevel;   
		}

		//message = textComp.text;
		if (levelname == "Level1") {
			message = " Welcome to your first Mission.\n Mission : To destroy enemy's \nweapons factory. \n\n Follow the mud path " +
				"\n and it will lead you to the factory \n Tip: Toggle camera for better missile aim";
		} else if (levelname == "Level2") {
			message = " Welcome to your second Mission. \n Your mission is to destroy enemy air base\n" + "Follow the mud path \n"
 +  "Tip1 : Using zig zag movement can help you escape some of the the heat seeking missiles. \n" +
				"";

		}
		else if (levelname == "Level3") {
			message = " Welcome to your third Mission. \n Your mission is to quitely follow an enemy plane\n" +
				"and discover the remaining enemies. \n Destroy their base" +
				"";

		}
		else if (levelname == "Level4") {
			message = " Welcome to your fourth Mission. \n Your mission is to find \n" +
				"and discover the remaining enemies. \n Destroy their base" +
				"";

		}
		textComp.text = "";
		StartCoroutine(TypeText ());
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void WriteText(string msg) {
	}

	IEnumerator TypeText () {
		foreach (char letter in message.ToCharArray()) {
			textComp.text += letter;
			//if (typeSound1 && typeSound2)
			//	SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
			//yield return 0;
			yield return new WaitForSeconds (letterPause);
		}
	}

	public void SetLevel(string level) {
		levelname = level;
	}

	public void SkipMission() {
		Application.LoadLevel (levelname);
	}

	public void GoToMission() {
		Application.LoadLevel (levelname);

	}
}


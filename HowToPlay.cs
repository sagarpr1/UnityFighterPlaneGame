using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
	public float LeftMoveTutTimeInt = 3f;
	public float RightMoveTutTimeInt = 3f;
	public float WeaponChoseTimeInt = 3f;
	public float BombFireTimeInt = 3f;
	public float MissileFireTimeInt = 3f;
	public float FlareAlarmTimeInt = 6f;
	public float CameraTimeInt=3f;

	public float LeftMoveTutTimer = 0;
	public float RightMoveTutTimer = 0;
	public float WeaponChoseTimer = 0;
	public float BombFireTimer = 0;
	public float MissileFireTimer = 0;
	public float FlareAlarmTimer = 0;
	public float CameraTimer=0;

	Text textString;
	string message;
	int msgcount =0;
	// Use this for initialization
	void Start ()
	{
		msgcount = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		LeftMoveTutTimer += Time.deltaTime;

		if (LeftMoveTutTimer > LeftMoveTutTimeInt) {

		}
	 }
}   


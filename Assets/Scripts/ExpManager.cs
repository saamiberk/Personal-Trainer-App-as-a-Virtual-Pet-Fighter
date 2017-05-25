using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour {
	public static int FoodExp;
	public static int FoodValue=0;
	public Image FoodBar;
	public Text FoodText;

	public GameObject foodExBar;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		FoodBar.transform.localScale = new Vector3 (FoodValue / 1f, 1f, 1f);
	}

	public static void AddExpPoints(int points)
	{
		FoodExp += points;
	}

	public void toggleBar(){
		if (foodExBar.activeSelf)
			foodExBar.SetActive (false);
		else
			foodExBar.SetActive (true);
	}
}

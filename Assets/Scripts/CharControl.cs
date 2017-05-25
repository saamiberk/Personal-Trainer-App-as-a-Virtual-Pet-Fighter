using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour {
	public GameObject fruitMarketPosition;
	public GameObject meatMarketPosition;
	public List <GameObject> fruits;
	public List <GameObject> meats;
	Animator animasyon;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void giveFruit(){
		foreach (GameObject t in fruits) {
			//t.gameObject.SetActive (true);
			Instantiate (t, fruitMarketPosition.transform.position, t.transform.rotation);
		}
	}

	public void giveMeat(){
		foreach (GameObject t in meats) {
			//t.gameObject.SetActive (true);
			Instantiate (t, meatMarketPosition.transform.position, t.transform.rotation);
		}
	}	
}

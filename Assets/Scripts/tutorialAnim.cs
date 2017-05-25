using UnityEngine;
using System.Collections.Generic;

public class tutorialAnim : MonoBehaviour {
	float currentTime;
	List<GameObject> childs=new List<GameObject>();
	int childCntr=0;
	// Use this for initialization
	void Start () {
		currentTime = Time.time;
		foreach (Transform child in transform) {
			childs.Add (child.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
			if (Time.time - currentTime > 1f) {
				childs [childCntr].SetActive (true);
				childCntr++;
				currentTime = Time.time;
			if (childs.Count == childCntr)
				transform.GetComponent<tutorialAnim> ().enabled = false;
			}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class imageFadeIn : MonoBehaviour {
	Color origin;
	// Use this for initialization
	void Start () {
		origin = transform.GetComponent<Image> ().color;
		origin.a = 1f;

	}
	
	// Update is called once per frame
	void Update () {
		transform.GetComponent<Image> ().color = Color.Lerp (transform.GetComponent<Image> ().color, origin, 1f / 80);
	}
}

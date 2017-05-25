using UnityEngine;
using UnityEngine.UI;

public class textFadeIn : MonoBehaviour {

	Color origin;
	// Use this for initialization
	void Start () {
		origin = transform.GetComponent<Text> ().color;
		origin.a = 1f;

	}

	// Update is called once per frame
	void Update () {
		transform.GetComponent<Text> ().color = Color.Lerp (transform.GetComponent<Text> ().color, origin, 1f / 80);
	}
}

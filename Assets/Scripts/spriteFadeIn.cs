using UnityEngine;


public class spriteFadeIn : MonoBehaviour {

	Color origin;
	// Use this for initialization
	void Start () {
		origin = transform.GetComponent<SpriteRenderer> ().color;
		origin.a = 1f;

	}

	// Update is called once per frame
	void Update () {
		transform.GetComponent<SpriteRenderer> ().color = Color.Lerp (transform.GetComponent<SpriteRenderer> ().color, origin, 1f / 80);
	}
}

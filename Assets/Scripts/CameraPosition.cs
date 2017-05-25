using UnityEngine;

public class CameraPosition : MonoBehaviour {

    public bool destroy = false;

	void Start () {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;

        if(!(gameObject.scene.name == "Training Ground"))
        {
            if (login.characterNumber == 6 || login.characterNumber == 7 || login.characterNumber == 8)
            {
                gameObject.GetComponentInChildren<SweatScript>().ConfigureSweat(0.5f, 0.6f, 1);
            }
            if (login.characterNumber == 3)
            {
                gameObject.GetComponentInChildren<SweatScript>().ConfigureSweat(0.45f, 0.45f, 1);
            }
        }
    }

	void Update () {
	    if(destroy == true)
        {
            Destroy(gameObject);
        }
	}
}

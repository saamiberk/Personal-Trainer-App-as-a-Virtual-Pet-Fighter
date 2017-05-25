using UnityEngine;

public class seaAnim : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        Animator anim = GetComponent<Animator>();
        anim.Play("backgroundSea");


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

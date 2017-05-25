using UnityEngine;
using System.Collections.Generic;



public class ActivateCharacter : MonoBehaviour {
    private bool isAtk = false;
    private float wait = 0.5f;
    private List<GameObject> models;
    private Animator anim;

    void Start () {
        anim = gameObject.GetComponentInChildren<Animator>();

        models = new List<GameObject>();
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        models[1].SetActive(true);
    }

    public void Atk()
    {
        if(login.characterNumber == 0 || login.characterNumber == 1 || login.characterNumber == 2)
        {
            models[1].SetActive(false);
            models[0].SetActive(true);
            anim.SetInteger("State", 1);
            isAtk = true;
        }
        else
        {
            models[1].GetComponent<idleAnim>().startCoroutine();
        }
    }

    void Update () {
        if (isAtk == true)
        {
            wait -= Time.deltaTime;
            if(wait <= 0)
            {
                wait = 1;
                isAtk = false;
                anim.SetInteger("State", 0);
            }
        }
        else
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                models[0].SetActive(false);
                models[1].SetActive(true);
            }
        }
	}
}
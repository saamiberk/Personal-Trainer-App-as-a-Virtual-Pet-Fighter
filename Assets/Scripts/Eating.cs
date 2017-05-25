using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Eating : MonoBehaviour
{
    float currentTime;
    List<GameObject> childs = new List<GameObject>();
    int childCntr = 0;

    public static int eat = 0;


    void Start()
    {
        currentTime = Time.time;
        foreach (Transform child in transform)
        {
            childs.Add(child.gameObject);
        }
    }

    void Update()
    {
        //Debug.Log(Time.time - currentTime);
        if (Time.time - currentTime > 1f)
        {
            childs[childCntr].SetActive(false);
            childCntr++;
            currentTime = Time.time;
            if (childs.Count == childCntr)
                Destroy(gameObject);

            if (gameObject.name == "EatBanana(Clone)")
            {
                eat = 1;
            }
            if (gameObject.name == "EatOrange(Clone)")
            {
                eat = 2;
            }
            if (gameObject.name == "EatBurger(Clone)")
            {
                eat = 3;
            }
            if (gameObject.name == "EatPizza(Clone)")
            {
                eat = 4;
            }
        }
    }
}

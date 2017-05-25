using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodClick : MonoBehaviour
{

    public Transform character;
    public ExpManager expMan;
    public List<int> FoodPoints = new List<int>();
    public GameObject burgerEat, bananaEat, orangeEat, pizzaEat;



    void OnMouseDown()
    {
        if (transform.tag == "banana")
        {
            Instantiate(bananaEat, transform.position, transform.rotation);
            ExpManager.AddExpPoints(FoodPoints[0]);
        }
        if (transform.tag == "burger")
        {
            Instantiate(burgerEat, transform.position, transform.rotation);
            ExpManager.AddExpPoints(FoodPoints[1]);
        }
        if (transform.tag == "orange")
        {
            Instantiate(orangeEat, transform.position, transform.rotation);
            ExpManager.AddExpPoints(FoodPoints[2]);
        }
        if (transform.tag == "pizza")
        {
            Instantiate(pizzaEat, transform.position, transform.rotation);
            ExpManager.AddExpPoints(FoodPoints[3]);
        }

        Destroy(this.gameObject);
    }
}


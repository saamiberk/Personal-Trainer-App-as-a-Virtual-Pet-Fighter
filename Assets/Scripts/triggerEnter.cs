using UnityEngine;

public class triggerEnter : MonoBehaviour {
	public GameObject chairPos,orginPos,toActive,toDisable,foodSpawn,banana,burger,orange, pizza,panel;
	int trigCount=0;

    void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "char") {
			if (trigCount % 2 == 0) {
				other.transform.position = chairPos.transform.position;
				//toActive.SetActive (true);
                other.transform.FindChild("toolContainer").gameObject.SetActive(true);
				toDisable.SetActive (false);
                panel.SetActive(true);

			} else {
				other.transform.position = orginPos.transform.position;
                other.transform.FindChild("toolContainer").gameObject.SetActive(false);
                toDisable.SetActive (true);
                panel.SetActive(false);
            }
			trigCount++;
		}
	}

	public void bananaSpawn(){

        if (MoneyScript.coin>=5)
        {
            Instantiate(banana, foodSpawn.transform.position, banana.transform.rotation);
        }	           
	}

	public void burgerSpawn(){

        if (MoneyScript.coin >= 15)
        {
            Instantiate(burger, foodSpawn.transform.position, burger.transform.rotation);
        }  
    }

    public void orangeSpawn(){

        if (MoneyScript.coin >=10)
        {
            Instantiate(orange, foodSpawn.transform.position, orange.transform.rotation);
        }
    }

    public void pizzaSpawn(){

        if (MoneyScript.coin>=20)
        {
            Instantiate(pizza, foodSpawn.transform.position, pizza.transform.rotation);
        }   
    }
}

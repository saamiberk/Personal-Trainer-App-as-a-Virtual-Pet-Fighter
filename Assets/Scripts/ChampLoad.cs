using UnityEngine;
using System.Collections.Generic;

public class ChampLoad : MonoBehaviour {

    [SerializeField]
    private GameObject[] champ;
    private List<GameObject> models;

    void Start () {

        if (gameObject.scene.name != "MainScreen-L" ||
              gameObject.scene.name != "MainScreen-F" || gameObject.scene.name != "MainScreen-S")
        {
            for (int i = 0; i < champ.Length; i++)
            {
                if (i == login.characterNumber)
                {
                    champ[login.characterNumber].SetActive(true);
                }
                else
                {
                    Destroy(champ[i]);
                }
            }
        }
        
        if (gameObject.scene.name == "MainScreen-L" || 
            gameObject.scene.name == "MainScreen-F" || gameObject.scene.name == "MainScreen-S")
        {
            champ[login.characterNumber].GetComponent<player>().HealthAndEnergy();
        }
    }
    public void CharLoad()
    {
          for (int i = 0; i<champ.Length; i++)
          {
            if (i == login.characterNumber)
            {
                champ[login.characterNumber].SetActive(true);
            }
            else
            {
                Destroy(champ[i]);
            }
        }
    }
}
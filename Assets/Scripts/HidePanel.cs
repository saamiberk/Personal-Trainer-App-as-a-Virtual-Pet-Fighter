using UnityEngine;
using System.Collections;

public class HidePanel : MonoBehaviour {

    [SerializeField]
    private GameObject[] panelObjects;

    public void HideObjects()
    {
        for(int i = 0; i < 7; i++)
        {
            panelObjects[i].SetActive(false);
        }
    }

    public void EnableObjects()
    {
        for (int i = 0; i < 7; i++)
        {
            panelObjects[i].SetActive(true);
        }
    }
}
using UnityEngine;
using System.Collections;

public class RandomLoadingBackground : MonoBehaviour {

    public GameObject[] backgrounds;
    private int randomBackground;
    private Vector3 rectVector = new Vector3 (0, 0, 0);
    private float z = 0;
    private bool zoomIn = true;

    void Start () {
        randomBackground = Random.Range(0, 3);

        for(int i = 0; i < 3; i++)
        {
            if(i == randomBackground)
            {
                backgrounds[i].SetActive(true);
            }
            else
            {
                backgrounds[i].SetActive(false);
            }
        }
	}

    void Update()
    {        
        backgrounds[randomBackground].GetComponent<RectTransform>().position = rectVector;

        if (zoomIn == true)
        {
            z -= 0.3f * Time.deltaTime;
            rectVector = new Vector3(0, 0, z);
            if(z <= -8)
            {
                zoomIn = false;
            }
        }
        
        if(zoomIn == false)
        {
            z += 0.3f * Time.deltaTime;
            rectVector = new Vector3(0, 0, z);
            if (z >= 0)
            {
                zoomIn = true;
            }
        }      
    }
}
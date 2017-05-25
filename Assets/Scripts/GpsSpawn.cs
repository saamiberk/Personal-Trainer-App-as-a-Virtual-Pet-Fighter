using UnityEngine;

public class GpsSpawn : MonoBehaviour
{
    GameObject clone;

    [SerializeField]
    public GameObject[] gpsCharacters; 
   
    void Start()
    {
        if (login.characterNumber == 0)
        {
             clone = (GameObject)Instantiate(gpsCharacters[0], transform.position, Quaternion.identity);
        }
        if (login.characterNumber == 1)
        {
             clone = (GameObject)Instantiate(gpsCharacters[1], transform.position, Quaternion.identity);
        }
        if (login.characterNumber == 2)
        {
             clone = (GameObject)Instantiate(gpsCharacters[2], transform.position, Quaternion.identity);
        }
        if (login.characterNumber == 3)
        {
            clone = (GameObject)Instantiate(gpsCharacters[3], transform.position, Quaternion.identity);
        }
        if (login.characterNumber == 4)
        {
            clone = (GameObject)Instantiate(gpsCharacters[4], transform.position, Quaternion.identity);
        }
        if (login.characterNumber == 5)
        {
            clone = (GameObject)Instantiate(gpsCharacters[5], transform.position, Quaternion.identity);
        }
        if (login.characterNumber == 6)
        {
            clone = (GameObject)Instantiate(gpsCharacters[6], transform.position, Quaternion.identity);
        }
        if (login.characterNumber == 7)
        {
            clone = (GameObject)Instantiate(gpsCharacters[7], transform.position, Quaternion.identity);
        }
        if (login.characterNumber == 8)
        {
            clone = (GameObject)Instantiate(gpsCharacters[8], transform.position, Quaternion.identity);
        }
    }

    public void DestroyClone()
    {
        Destroy(clone, 0.05f);
    }
}

         
     

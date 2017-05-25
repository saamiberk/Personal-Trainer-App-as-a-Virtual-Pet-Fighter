using UnityEngine;

public class TrainingCharacterSpawner : MonoBehaviour
{
    public GameObject[] trainingCharacters;
    private bool doOnce = false;
  
    void Start()
    {       
        DoOnce();
        
    }

    public void DoOnce()
    {
        if (doOnce == false)
        {
            if (login.characterNumber == 0)
            {
                Instantiate(trainingCharacters[0], transform.position, Quaternion.identity);
            }
            if (login.characterNumber == 1)
            {
                Instantiate(trainingCharacters[1], transform.position, Quaternion.identity);
            }
            if (login.characterNumber == 2)
            {
                Instantiate(trainingCharacters[2], transform.position, Quaternion.identity);
            }
            if (login.characterNumber == 3)
            {
                Instantiate(trainingCharacters[3], transform.position, Quaternion.identity);
            }
            if (login.characterNumber == 4)
            {
                Instantiate(trainingCharacters[4], transform.position, Quaternion.identity);
            }
            if (login.characterNumber == 5)
            {
                Instantiate(trainingCharacters[5], transform.position, Quaternion.identity);
            }
            if (login.characterNumber == 6)
            {
                Instantiate(trainingCharacters[6], transform.position, Quaternion.identity);
            }
            if (login.characterNumber == 7)
            {
                Instantiate(trainingCharacters[7], transform.position, Quaternion.identity);
            }
            if (login.characterNumber == 8)
            {
                Instantiate(trainingCharacters[8], transform.position, Quaternion.identity);
            }
            doOnce = true;
        }
    }
}

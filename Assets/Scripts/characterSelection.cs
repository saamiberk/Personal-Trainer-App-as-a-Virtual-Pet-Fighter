using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.SceneManagement;


public class characterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    public GameObject[] backgroundList;
    private int index;

    private void Start()
    {
        characterList = new GameObject[transform.childCount];      

        // Fill the array with our models
        for (int i = 0; i < transform.childCount; i++)
            characterList[i] = transform.GetChild(i).gameObject;

        // We toggle off their renderer
        foreach (GameObject go in characterList)
            go.SetActive(false);

        // We toggle on the first index;
        if (characterList[0])
            characterList[0].SetActive(true);
}

    public void ToggleLeft()
    {
        // Toggle off the current model
        characterList[index].SetActive(false);
        backgroundList[index].SetActive(false);

        index--;
        if (index < 0)
            index = characterList.Length - 1;


        // Toggle on the new model
        characterList[index].SetActive(true);
        backgroundList[index].SetActive(true);

        characterList[index].GetComponent<idleAnim>().startCoroutine();
    }

    public void ToggleRight()
    {
        // Toggle off the current model
        characterList[index].SetActive(false);
        backgroundList[index].SetActive(false);

        index++;
        if (index == characterList.Length)
            index = 0;


        // Toggle on the new model
        characterList[index].SetActive(true);
        backgroundList[index].SetActive(true);

        characterList[index].GetComponent<idleAnim>().startCoroutine();
    }

    public void ConfirmButton()
    {        
        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("UPDATE PetInformation SET PetNumber=@number WHERE PetId = (SELECT PlayerID FROM  PlayerInformation WHERE Name = @name)", connection);
          
            if (index == 0)
            {
                LoadingScreenManager.LoadScene("MainScreen-L");
                cmd.Parameters.AddWithValue("@number", 0);
                login.characterNumber = 0;
            }
            if (index == 1)
            {
                LoadingScreenManager.LoadScene("MainScreen-F");
                cmd.Parameters.AddWithValue("@number", 1);
                login.characterNumber = 1;
            }
            if (index == 2)
            {
                LoadingScreenManager.LoadScene("MainScreen-S");
                cmd.Parameters.AddWithValue("@number", 2);
                login.characterNumber = 2;
            }

            string name = login.myName;
            cmd.Parameters.AddWithValue("name", name);
                        
            // First Time  = 0 Character doesnt select. Set FirstTime to 1
            SqlCommand cmd2 = new SqlCommand("UPDATE PlayerInformation SET FirstTime=1 WHERE Name=@id", connection);
            cmd2.Parameters.AddWithValue("@id", name);
            cmd2.ExecuteNonQuery();

            cmd.ExecuteNonQuery();
        }       
    }
}



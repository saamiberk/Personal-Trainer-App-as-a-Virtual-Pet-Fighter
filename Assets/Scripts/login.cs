using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class login : MonoBehaviour
{

    public static string connectionString;

    [SerializeField]
    private GameObject UserName;
    [SerializeField]
    private GameObject Password;

    private string userName;
    private string password;
    public static string myName;

    [SerializeField]
    private GameObject[] panel;

    public static int characterNumber;
    private int check = 0;



    void Start()
    {    
        connectionString = "workstation id=VirtualPetDB.mssql.somee.com;packet size=4096;user id=berkcl123_SQLLogin_1;pwd=xdi56ufqts;data source=VirtualPetDB.mssql.somee.com;persist security info=False;initial catalog=VirtualPetDB;MultipleActiveResultSets=true";

        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            SaveLoad.Load();

            if (PlayerInfo.current.playerName != "")
            {
                UserName.GetComponent<InputField>().text = PlayerInfo.current.playerName;
            }
        }
    }

    public void LoginButton()
    {   
        userName = UserName.GetComponent<InputField>().text;
        password = Password.GetComponent<InputField>().text;
        if(userName == "" || password == "")
        {
            Debug.Log("User didn't enter their username or password.");

            if(userName == "")
            {
                UsernameWarningText();               
            }
            if(password == "")
            {
                PasswordWarningText();                
            }
        }
        else
        {
            Login(userName, password);
        }
    }

    public void Login(string name, string password)
    {
        myName = name;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PlayerInformation WHERE Name=@name and Password=@password", connection);
            
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", password);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    check = Convert.ToInt32(reader["FirstTime"]);
                    if (check == 0)
                    {
                        PlayerInfo.current = new PlayerInfo();
                        PlayerInfo.current.playerName = name;
                        SaveLoad.CreateNewSave();

                        LoadingScreenManager.LoadScene("CharSelection");
                    }
                }
                else
                {
                    UsernameWarningText();
                    UserName.GetComponent<InputField>().text = "";
                    UserName.GetComponent<InputField>().placeholder.GetComponent<Text>().text =
                        "Incorrect username or password.";
                    PasswordWarningText();
                    Password.GetComponent<InputField>().text = "";
                    Password.GetComponent<InputField>().placeholder.GetComponent<Text>().text =
                        "Incorrect username or password";
                }
            }

            if (check == 1)
            {
                if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
                {
                    SaveLoad.Load();

                    if (!(PlayerInfo.current.playerName == name))
                    {
                        PlayerInfo.current = new PlayerInfo();
                        PlayerInfo.current.playerName = name;
                        SaveLoad.CreateNewSave();
                    }
                }
                else
                {
                    PlayerInfo.current = new PlayerInfo();
                    PlayerInfo.current.playerName = name;
                    SaveLoad.CreateNewSave();
                }

                SqlCommand command = new SqlCommand("SELECT * FROM PlayerInformation INNER JOIN PetInformation ON PlayerID = PetID WHERE Name=@name and Password=@password", connection);

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@password", password);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                    characterNumber = Convert.ToInt32(reader["PetNumber"]);
                    }

                    if (characterNumber == 0 || characterNumber == 3 || characterNumber == 6)
                    {
                        LoadingScreenManager.LoadScene("MainScreen-L");
                    }
                    if (characterNumber == 1 || characterNumber == 4 || characterNumber == 7)
                    {
                        LoadingScreenManager.LoadScene("MainScreen-F");
                    }
                    if (characterNumber == 2 || characterNumber == 5 || characterNumber == 8)
                    {
                        LoadingScreenManager.LoadScene("MainScreen-S");
                    }
                }
            }
        }
    }

    public void UsernameWarningText()
    {
        UserName.GetComponent<InputField>().placeholder.GetComponent<Text>().text =
            "Please enter a valid username.";
        UserName.GetComponent<InputField>().placeholder.GetComponent<Text>().fontSize = 13;
        UserName.GetComponent<InputField>().placeholder.GetComponent<Text>().color = Color.red;
    }

    public void PasswordWarningText()
    {
        Password.GetComponent<InputField>().placeholder.GetComponent<Text>().text =
            "Please enter a valid password.";
        Password.GetComponent<InputField>().placeholder.GetComponent<Text>().fontSize = 13;
        Password.GetComponent<InputField>().placeholder.GetComponent<Text>().color = Color.red;
    }

    public void UsernameInfoText()
    {
        UserName.GetComponent<InputField>().placeholder.GetComponent<Text>().text =
            "Enter username...";
        UserName.GetComponent<InputField>().placeholder.GetComponent<Text>().fontSize = 14;
        UserName.GetComponent<InputField>().placeholder.GetComponent<Text>().color = Color.gray;
    }

    public void PasswordInfoText()
    {
        Password.GetComponent<InputField>().placeholder.GetComponent<Text>().text =
            "Enter password...";
        Password.GetComponent<InputField>().placeholder.GetComponent<Text>().fontSize = 14;
        Password.GetComponent<InputField>().placeholder.GetComponent<Text>().color = Color.gray;
    }

    public void RegisterButton()
    {
        UsernameInfoText();
        PasswordInfoText();

        panel[0].SetActive(false);
        panel[1].SetActive(true);
        panel[2].SetActive(false);
        panel[3].SetActive(false);
        panel[4].SetActive(true);
    }

    public void PlayButton()
    {
        UsernameInfoText();
        PasswordInfoText();

        panel[0].SetActive(true);
        panel[1].SetActive(false);
        panel[2].SetActive(false);
        panel[3].SetActive(false);
        panel[4].SetActive(true);

    }

    public void LoginBack()
    {
        UsernameInfoText();
        PasswordInfoText();

        panel[0].SetActive(false);
        panel[1].SetActive(false);
        panel[2].SetActive(true);
        panel[3].SetActive(false);
        panel[4].SetActive(true);
    }

    public void Tutorial()
    {
        panel[0].SetActive(false);
        panel[1].SetActive(false);
        panel[2].SetActive(false);
        panel[3].SetActive(true);
        panel[4].SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
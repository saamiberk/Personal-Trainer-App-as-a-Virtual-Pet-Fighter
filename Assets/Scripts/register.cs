using UnityEngine;
using System;
using UnityEngine.UI;
using System.Data;
using System.Data.SqlClient;

public class register : MonoBehaviour {

    [SerializeField]
    private GameObject UserName;
    [SerializeField]
    private GameObject Email;
    [SerializeField]
    private GameObject Password;
    [SerializeField]
    private GameObject[] Panel;
    [SerializeField]
    private GameObject InfoText;

    private string userName;
    private string email;
    private string password;

    public void Register(string name, string email, string password)
    {
        using (SqlConnection connection = new SqlConnection(login.connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO PlayerInformation(Name,Email,Password)VALUES (@name,@email,@password)", connection);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            SqlCommand cmd2 = new SqlCommand("INSERT INTO PetInformation(PetNumber)VALUES (0)", connection);
            
            int cmd0 = cmd.ExecuteNonQuery();
            int cmd1 = cmd2.ExecuteNonQuery();

            if (cmd0 == 1 && cmd1 == 1)
            {
                Debug.Log("Registration is succesful.");
                InfoText.SetActive(true);
                ButtonBack();
            }
            else
            {
                InfoText.SetActive(true);
                InfoText.GetComponent<Text>().text = "Something went wrong. Please try again.";
                InfoText.GetComponent<Text>().color = Color.HSVToRGB(60, 255, 255);
                ButtonBack();
            }
        }
    }

    public void RegisterButton()
    {
        userName = UserName.GetComponent<InputField>().text;
        email = Email.GetComponent<InputField>().text;
        password = Password.GetComponent<InputField>().text;

        if (userName == "" || password == "" || email == "")
        {
            Debug.Log("User didn't enter their username, password or e-mail.");

            if (userName == "")
            {
                UsernameWarningText();
            }
            if (password == "")
            {
                PasswordWarningText();
            }
            if (email == "")
            {
                EmailWarningText();
            }
        }
        else
        {
            Register(userName, email, password);
        }        
    }

    public void ButtonBack()
    {
        UsernameInfoText();
        PasswordInfoText();
        EmailInfoText();

        Panel[0].SetActive(true);
        Panel[1].SetActive(false);
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

    void EmailWarningText()
    {
        Email.GetComponent<InputField>().placeholder.GetComponent<Text>().text =
            "Please enter a valid e-mail.";
        Email.GetComponent<InputField>().placeholder.GetComponent<Text>().color = Color.red;
        Email.GetComponent<InputField>().placeholder.GetComponent<Text>().fontSize = 13;
    }

    void EmailInfoText()
    {
        Email.GetComponent<InputField>().placeholder.GetComponent<Text>().text =
            "Enter e-mail...";
        Email.GetComponent<InputField>().placeholder.GetComponent<Text>().color = Color.gray;
        Email.GetComponent<InputField>().placeholder.GetComponent<Text>().fontSize = 14;
    }

}

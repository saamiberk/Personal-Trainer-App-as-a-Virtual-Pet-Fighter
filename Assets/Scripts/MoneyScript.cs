using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data.SqlClient;

public class MoneyScript : MonoBehaviour {

    private string myName;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private GameObject buttonHome;
    [SerializeField]
    private GameObject notMoney;

    public static int coin ;

    void Start () {
        myName = login.myName;
        coin = MainScreen.gold;
        moneyText.text = coin.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            coin += 20;
        }
    }

    public void DbMoneyUpdate()
    {
        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("UPDATE PetInformation Set Gold = @gold WHERE PetId = (SELECT PlayerID FROM  PlayerInformation WHERE Name = @name)", connection);
            cmd.Parameters.AddWithValue("@name", myName);
            cmd.Parameters.AddWithValue("@gold", coin);         
            cmd.ExecuteNonQuery();
        }
    }

    public void Banana()
    {
       
        if (coin >= 5)
        {
            notMoney.SetActive(false);
            coin = coin - 5;
            moneyText.text = coin.ToString();

        }
        else notMoney.SetActive(true);
      
    }
    public void Orange()
    {
      
        if (coin>=10)
        {
            notMoney.SetActive(false);
            coin = coin - 10;
            moneyText.text = coin.ToString();

        }
        else notMoney.SetActive(true);


    }
    public void Burger()
    {
       
        if (coin>=15)
        {
            notMoney.SetActive(false);
            coin = coin - 15;
            moneyText.text = coin.ToString();

        }
        else notMoney.SetActive(true);

    }
    public void Pizza()
    {
        if (coin>=20)
        {
            notMoney.SetActive(false);
            coin = coin - 20;
            moneyText.text = coin.ToString();
        }
        else notMoney.SetActive(true);

    }
}

using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;

public class EatingRefresh : MonoBehaviour {

    [SerializeField]
    private stat health;

    [SerializeField]
    private stat energy;

    private string myName;
 
    void Start()
    {
        myName = login.myName;
        HealthAndEnergy();
    }

    private void Awake()
    {
        health.Initialize();
        energy.Initialize();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health.CurrentVal -= 10;
            energy.CurrentVal -= 10;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            health.CurrentVal += 10;
            energy.CurrentVal += 10;
        }
        if (Eating.eat != 0)
        {
            if (Eating.eat == 1)
            {
                energy.CurrentVal = energy.CurrentVal + 2;
                health.CurrentVal = health.CurrentVal + 3;
                Eating.eat = 0;
            }
            if (Eating.eat == 2)
            {
                energy.CurrentVal = energy.CurrentVal + 5;
                health.CurrentVal = health.CurrentVal + 6;
                Eating.eat = 0;
            }
            if (Eating.eat == 3)
            {
                energy.CurrentVal = energy.CurrentVal + 7;
                health.CurrentVal = health.CurrentVal + 8;
                Eating.eat = 0;
            }
            if (Eating.eat == 4)
            {
                energy.CurrentVal = energy.CurrentVal + 13;
                health.CurrentVal = health.CurrentVal + 15;
                Eating.eat = 0;
            }           
        }
    }

    public void DbUpdate()
    {
        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("UPDATE PetInformation Set CurrentEnergy = @crenergy, CurrentHealth = @crhealth WHERE PetId = (SELECT PlayerID FROM  PlayerInformation WHERE Name = @name)", connection);
            cmd.Parameters.AddWithValue("@crhealth", health.CurrentVal);
            cmd.Parameters.AddWithValue("@crenergy", energy.CurrentVal);
            cmd.Parameters.AddWithValue("@name", myName);
            cmd.ExecuteNonQuery();
        }
    }

    public void HealthAndEnergy()
    {
        health.CurrentVal = player.HealthCurrentVal;
        energy.CurrentVal = player.EnergyCurrentVal;
    }
}

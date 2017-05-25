using UnityEngine;
using System;
using System.Data.SqlClient;

public class player : MonoBehaviour {

    [SerializeField]
    private stat health;

    [SerializeField]
    private stat energy;

    public static float EnergyCurrentVal;
    public static float HealthCurrentVal;
  
    private string myName;

    private void Awake()
    {
        health.Initialize();
        energy.Initialize();
    }

    public void HealthAndEnergy()
    {
        myName = login.myName;

        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT CurrentHealth,CurrentEnergy FROM PetInformation INNER JOIN PlayerInformation on petID=PlayerID WHERE Name=@name", connection);
            
            cmd.Parameters.AddWithValue("@name", myName);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    health.CurrentVal = Convert.ToInt32(reader["CurrentHealth"].ToString());
                    energy.CurrentVal = Convert.ToInt32(reader["CurrentEnergy"].ToString());                 
                            
                }
                HealthCurrentVal = health.CurrentVal;
                EnergyCurrentVal = energy.CurrentVal;

            }          
        }
    }
}
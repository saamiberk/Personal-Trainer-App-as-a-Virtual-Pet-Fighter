using System;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;


public class UpdateGPSText : MonoBehaviour
{
    private GameObject gpsText;
    public Text gpsInfo;
    public string myName;
    public double km = 0;

    [SerializeField]
    private GameObject buttonBack;

    void Start()
    {
        myName = login.myName;
        buttonBack = GameObject.FindGameObjectWithTag("gpsButton");
        buttonBack.GetComponent<Button>().onClick.AddListener(GPSDbConnection);
        
    }

    void Update()
    {
        gpsText = GameObject.FindGameObjectWithTag("gpsText");
        gpsInfo = gpsText.GetComponent<Text>();
        gpsInfo.text = "";

        switch (GPS.Instance.state)
        {
            case LocationState.Enabled:
                gpsInfo.text = "Latitude: " + GPS.Instance.latitude.ToString() +
                    "| Longitude: " + GPS.Instance.longitude.ToString() + "\n\n" +
                    "Distance: " + Math.Round(GPS.Instance.distance).ToString() + "m";
                break;

            case LocationState.Disabled:
                gpsInfo.text = "GPS is disabled. Please make sure that\n" +
                    "your device has a GPS connection.";
                break;

            case LocationState.Failed:
                gpsInfo.text = "Failed to initialize location service.\n" +
                    "Try again later.";
                break;

            case LocationState.TimedOut:
                gpsInfo.text = "Connection timed out. Try again later.";
                break;
        }
    }

    public void GPSDbConnection()
    { 
        km = GPS.Instance.distance / 1000;
        km = (Math.Round(km, 3));
        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("UPDATE PetInformation Set KM = KM + @km  WHERE PetId = (SELECT PlayerID FROM  PlayerInformation WHERE Name = @name)",connection);
            cmd.Parameters.AddWithValue("@KM", km);                          
            cmd.Parameters.AddWithValue("@name", myName);
            cmd.ExecuteNonQuery();
        }   
    }
}
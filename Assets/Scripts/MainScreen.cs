using System;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class MainScreen : MonoBehaviour
{
    [SerializeField]
    private stat health;
    [SerializeField]
    private stat energy;
    [SerializeField]
    private GameObject fadeOut;
    [SerializeField]
    private GameObject sleepAnim;
    [SerializeField]
    private Text[] text;
    [SerializeField]
    private GameObject characters;
    [SerializeField]
    private GameObject volumeCheck;


    private DateTime sleepTime;
    private DateTime currentTime;
    public static int gold;
    public int expCount;
    public static int levelCount;
    private bool isEvolved=false;
    public int charNumber = 0;

    void Start()
    {
        if (PlayerInfo.current.isSleeping == true)
        {
            FadeOnStart();
            setVolume();
        }
        LevelCheck();
        DataInformation();
    }
     void Update()
    {
       
    }

    public void DataInformation()
    {
        int j = 1;
        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PetInformation INNER JOIN PlayerInformation ON PetID = PlayerID WHERE Name=@name", connection);
            string name = login.myName;
            cmd.Parameters.AddWithValue("name", name);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                for (int i = 0; i < 10; i++)
                {
                    j++;
                    text[i].text = reader[j].ToString();
                }

            }   

            gold = Convert.ToInt32(reader["Gold"].ToString());           
        }
       
    }

    public void Fade()
    {
        StartCoroutine("FadeInBlack");
        setVolume();

    }

    public void FadeOnStart()
    {
        fadeOut.GetComponent<HidePanel>().HideObjects();
        fadeOut.SetActive(true);
        
        if (!(login.characterNumber == 0 || login.characterNumber == 1 || login.characterNumber == 2
            || login.characterNumber == 3 || login.characterNumber == 4))
        {
            sleepAnim.GetComponent<RectTransform>().anchorMin = new Vector2(0.8f, 0.8f);
            sleepAnim.GetComponent<RectTransform>().anchorMax = new Vector2(0.8f, 0.8f);
        }

        sleepAnim.SetActive(true);
        sleepAnim.GetComponent<ImageAnimator>().sleep = true;
    }

    public void LevelCheck()
    {
        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PetInformation INNER JOIN PlayerInformation ON PetID = PlayerID WHERE Name=@name", connection);
            cmd.Parameters.AddWithValue("@name", login.myName);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    expCount = Convert.ToInt32(reader["Exp"].ToString());
                }

            }
        }

        if (expCount >= 100)
        {
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                int i = expCount / 100;
                expCount -= (100 * i);
                text[5].text = expCount.ToString();
                SqlCommand command = new SqlCommand("UPDATE PetInformation Set Level = Level + @level, Exp = Exp - (100 * @level), Atack = Atack + 50, Defense = Defense + 20 WHERE PetId = (SELECT PlayerID FROM  PlayerInformation WHERE Name = @name)", connection);
                command.Parameters.AddWithValue("@name", login.myName);
                command.Parameters.AddWithValue("@level", i);
                command.ExecuteNonQuery();
            }
        }

        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PetInformation INNER JOIN PlayerInformation ON PetID = PlayerID WHERE Name=@name", connection);
            cmd.Parameters.AddWithValue("@name", login.myName);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    levelCount = Convert.ToInt32(reader["Level"].ToString());

                    if (levelCount == 5 && Convert.ToInt32(reader["PetNumber"].ToString()) < 3)
                    {
                        login.characterNumber += 3;
                        isEvolved = true;
                       
                    }

                    if (levelCount == 10 && Convert.ToInt32(reader["PetNumber"].ToString()) < 6 && Convert.ToInt32(reader["PetNumber"].ToString()) > 2)
                    {
                        login.characterNumber += 3;
                        isEvolved = true;
                        
                    }

                    characters.GetComponent<ChampLoad>().CharLoad();
                }

            }
        }
        if (isEvolved)
        {
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE PetInformation Set PetNumber = @petnumber WHERE PetId = (SELECT PlayerID FROM  PlayerInformation WHERE Name = @name)", connection);
                cmd.Parameters.AddWithValue("@petnumber", login.characterNumber);
                cmd.Parameters.AddWithValue("@name", login.myName);
                cmd.ExecuteNonQuery();
                isEvolved = false;
            }

        }
        
    }
    
    public void setVolume()
    {
        if (volumeCheck.GetComponent<AudioSource>().volume == 1)
        {
            volumeCheck.GetComponent<AudioSource>().volume = 0;
        }
        else volumeCheck.GetComponent<AudioSource>().volume = 1;
    }

    public IEnumerator FadeInBlack()
    {
        if (PlayerInfo.current.isSleeping == false) //If pet is not sleeping
        {
            //Upadate sleeping information
            PlayerInfo.current.isSleeping = true;
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {                
                SaveLoad.Save();
            }

            //Sleep pet
            fadeOut.GetComponent<HidePanel>().HideObjects();
            fadeOut.GetComponent<CanvasRenderer>().SetAlpha(0);
            fadeOut.SetActive(true);
            fadeOut.GetComponent<Image>().CrossFadeAlpha(1, 1, true);
            yield return new WaitForSeconds(1);
            if(!(login.characterNumber == 0 || login.characterNumber == 1 || login.characterNumber == 2 
                || login.characterNumber == 3 || login.characterNumber == 4))
            {
                sleepAnim.GetComponent<RectTransform>().anchorMin = new Vector2(0.8f, 0.8f);
                sleepAnim.GetComponent<RectTransform>().anchorMax = new Vector2(0.8f, 0.8f);
            }
            sleepAnim.SetActive(true);
            sleepAnim.GetComponent<ImageAnimator>().sleep = true;

            //Update the sleeping time with current sleeping time
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE PetInformation Set SleepTime = GETDATE() WHERE PetId = (SELECT PlayerID FROM  PlayerInformation WHERE Name = @name)", connection);
                cmd.Parameters.AddWithValue("@name", login.myName);
                cmd.ExecuteNonQuery();
            }
            yield return null;
        }
        else //If pet is sleeping
        {
            //Wake up pet
            sleepAnim.SetActive(false);
            sleepAnim.GetComponent<ImageAnimator>().sleep = false;
            sleepAnim.GetComponent<Image>().sprite = sleepAnim.GetComponent<ImageAnimator>().spriteFrames[0];
            fadeOut.GetComponent<HidePanel>().EnableObjects();
            fadeOut.GetComponent<Image>().CrossFadeAlpha(0, 1, true);

            //Get the time when the pet started sleeping
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT SleepTime FROM PetInformation INNER JOIN PlayerInformation ON PetID = PlayerID WHERE Name=@name", connection);
                string name = login.myName;
                cmd.Parameters.AddWithValue("name", name);
                    
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    sleepTime = reader.GetDateTime(0);
                    //Debug.Log("Sleep Time: " + sleepTime.ToString());
                }
                connection.Close();
            }

            //Get the current time
            using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT GETDATE()", connection);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    currentTime = reader.GetDateTime(0);
                }
            }

            //Find how much time had passed for the duration of pet's sleeping time
            TimeSpan span = currentTime.Subtract(sleepTime);
            PlayerInfo.current.isSleeping = false;
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                SaveLoad.Save();
            }
            Debug.Log("Time Passed(Hours): " + span.Hours);

            //If the pet had slept more than 1 hour, update the energy level
            if(span.Hours > 0)
            {
                if((player.HealthCurrentVal + (span.Hours * 10)) > 100) //If health level passes 100 after adding the energy that gained from sleeping
                {
                    player.HealthCurrentVal = 100;
                    health.CurrentVal = player.HealthCurrentVal;

                    if ((player.EnergyCurrentVal + (span.Hours * 10)) > 100) //If energy level passes 100 after adding the energy that gained from sleeping
                    {
                        player.EnergyCurrentVal = 100;
                        energy.CurrentVal = player.EnergyCurrentVal;

                        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE PetInformation Set CurrentEnergy = @newEnergy, CurrentHealth = @newHealth WHERE PetId = (SELECT PlayerID FROM  PlayerInformation WHERE Name = @name)", connection);
                            cmd.Parameters.AddWithValue("@name", login.myName);
                            cmd.Parameters.AddWithValue("@newHealth", 100);
                            cmd.Parameters.AddWithValue("@newEnergy", 100);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else //If energy level doesn't pass 100 after adding the energy that gained from sleeping
                    {
                        player.EnergyCurrentVal = player.EnergyCurrentVal + (span.Hours * 10);
                        energy.CurrentVal = player.EnergyCurrentVal;

                        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE PetInformation Set CurrentEnergy = CurrentEnergy + @newEnergy, CurrentHealth = @newHealth WHERE PetId = (SELECT PlayerID FROM  PlayerInformation WHERE Name = @name)", connection);
                            cmd.Parameters.AddWithValue("@name", login.myName);
                            cmd.Parameters.AddWithValue("@newHealth", 100);
                            cmd.Parameters.AddWithValue("@newEnergy", span.Hours * 10);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else //If health level doesn't pass 100 after adding the energy that gained from sleeping
                {
                    player.HealthCurrentVal = player.HealthCurrentVal + (span.Hours * 10);
                    health.CurrentVal = player.HealthCurrentVal;

                    if ((player.EnergyCurrentVal + (span.Hours * 10)) > 100) //If energy level passes 100 after adding the energy that gained from sleeping
                    {
                        player.EnergyCurrentVal = 100;
                        energy.CurrentVal = player.EnergyCurrentVal;

                        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE PetInformation Set CurrentEnergy = @newEnergy, CurrentHealth = CurrentHealth + @newHealth WHERE PetId = (SELECT PlayerID FROM  PlayerInformation WHERE Name = @name)", connection);
                            cmd.Parameters.AddWithValue("@name", login.myName);
                            cmd.Parameters.AddWithValue("@newHealth", span.Hours * 10);
                            cmd.Parameters.AddWithValue("@newEnergy", 100);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else //If energy level doesn't pass 100 after adding the energy that gained from sleeping
                    {
                        player.EnergyCurrentVal = player.EnergyCurrentVal + (span.Hours * 10);
                        energy.CurrentVal = player.EnergyCurrentVal;

                        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE PetInformation Set CurrentEnergy = CurrentEnergy + @newEnergy, CurrentHealth = CurrentHealth + @newHealth WHERE PetId = (SELECT PlayerID FROM  PlayerInformation WHERE Name = @name)", connection);
                            cmd.Parameters.AddWithValue("@name", login.myName);
                            cmd.Parameters.AddWithValue("@newHealth", span.Hours * 10);
                            cmd.Parameters.AddWithValue("@newEnergy", span.Hours * 10);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
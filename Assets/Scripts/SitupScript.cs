using System.Collections;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;

public class SitupScript : MonoBehaviour
{
    [SerializeField]
    private stat energy;
    
    private bool position;
    public Text countText;
    public Text expText;
    private int count;
    private string myName;
    public string animNo;
    public string animIdle;
    public string animAtk;

    public AudioClip checkSound;
    public AudioClip checkSound2;
    public float Volume;
    AudioSource checkAudio;

    private bool ready = false;
    [SerializeField]
    private GameObject tiredText;
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject buttonBack;
    [SerializeField]
    private GameObject sweat;
    [SerializeField]
    private GameObject finishSweat;
    [SerializeField]
    private GameObject energyBar;

    public int situpCount = 0;
    public int exp = 0;
    bool spawn = false;
    bool lowEnergy = false;
 
    void Start()
    {
        SmoothMoves.BoneAnimation boneAni = gameObject.GetComponent<SmoothMoves.BoneAnimation>();
        string aniName = boneAni.GetAnimationClipName(0);
        animNo = aniName.Split('_')[1];
        animIdle = "Idle_" + animNo;
        animAtk = "Atk_" + animNo;

        button.GetComponent<Button>().onClick.AddListener(ReadyButton);
        buttonBack.GetComponent<Button>().onClick.AddListener(DbUpdate);
        energy.CurrentVal = player.EnergyCurrentVal;

        checkAudio = GetComponent<AudioSource>();
        count = 0;
        position = false;
        setText();
        myName = login.myName;
        //energyDbUpdate();

        if (energy.CurrentVal < 30)
        {
            StartCoroutine("LowEnergy");
            lowEnergy = true;
        }
    }

    void Update()
    {
        //Debug.Log("x = " + Input.acceleration.x);
        //Debug.Log("y = " + Input.acceleration.y);
        //Debug.Log("z = " + Input.acceleration.z);

        float x = Input.acceleration.x;
        float y = Input.acceleration.y;
        float z = Input.acceleration.z;
        
        if (ready)
        {
            if (energy.CurrentVal > 0)
            {
                //First if statement checks for which move needs to be done. In this case it is 
                //getting up.
                if (position == false)
                {
                    if (y > 0.9 || y < -0.9 || x < 0) //Second if statement checks the uncorrect device movement
                    {
                        Debug.Log("Wrong move!!");
                    }
                    else
                    {
                        if (z > -0.3 && z < 0.2) //Third if statement checks for the correct device movement                     
                        {
                            count++;
                            checkAudio.PlayOneShot(checkSound2, Volume);
                            StartCoroutine("animate");
                            StartCoroutine("SweatTime");
                            energy.CurrentVal = energy.CurrentVal - 1;
                            situpCount++;
                            exp = situpCount * 5;
                            setText();
                            position = true;
                        }
                    }
                }
                else //If the user has got up, then else statement is going to check if the user has lay down.
                {
                    if (z < -0.9)
                    {
                        checkAudio.PlayOneShot(checkSound, Volume);
                        position = false;
                    }
                }
                if (energy.CurrentVal < 30)
                {
                    //Show a message that energy level is getting low.
                    if (lowEnergy == false)
                    {
                        StartCoroutine("LowEnergy");
                        lowEnergy = true;
                    }
                }
            }
            else
            {
                //When energy hits 0, stop counting.
                StartCoroutine("FinishSweat");
                tiredText.SetActive(true);
            }
        }

        if (Input.GetKeyDown("space"))
        {
            energy.CurrentVal += 20;
        }
    }

    IEnumerator animate()
    {
        SmoothMoves.BoneAnimation boneAni = gameObject.GetComponent<SmoothMoves.BoneAnimation>();

        int i = boneAni.GetAnimationClipIndex(animAtk);
        if (i == -1) yield break;

        if (boneAni.IsPlaying(animAtk) == true) boneAni.Stop();

        boneAni.CrossFade(animAtk);
        yield return new WaitForSeconds(boneAni[animAtk].length);

        if (boneAni != null)
            boneAni.CrossFade(animIdle);
    }

    IEnumerator SweatTime()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(sweat, transform.position, Quaternion.identity);
    }

    IEnumerator FinishSweat()
    {
        yield return new WaitForSeconds(1f);
        if (spawn == false)
        {
            if (login.characterNumber == 0) finishSweat.GetComponentInChildren<SweatScript>().ConfigureSweat(0.5f, 0.35f, 1f);
            if (login.characterNumber == 1) finishSweat.GetComponentInChildren<SweatScript>().ConfigureSweat(0.5f, 0.32f, 1.2f);
            if (login.characterNumber == 2) finishSweat.GetComponentInChildren<SweatScript>().ConfigureSweat(0.51f, 0.38f, 1);
            if (login.characterNumber == 3) finishSweat.GetComponentInChildren<SweatScript>().ConfigureSweat(0.45f, 0.35f, 1f);
            if (login.characterNumber == 4) finishSweat.GetComponentInChildren<SweatScript>().ConfigureSweat(0.485f, 0.5f, 1f);
            if (login.characterNumber == 5) finishSweat.GetComponentInChildren<SweatScript>().ConfigureSweat(0.525f, 0.45f, 1f);
            if (login.characterNumber == 6) finishSweat.GetComponentInChildren<SweatScript>().ConfigureSweat(0.6f, 0.57f, 1.3f);
            if (login.characterNumber == 7) finishSweat.GetComponentInChildren<SweatScript>().ConfigureSweat(0.48f, 0.58f, 1.1f);
            if (login.characterNumber == 8) finishSweat.GetComponentInChildren<SweatScript>().ConfigureSweat(0.55f, 0.63f, 1.2f);

            finishSweat.SetActive(true);
            spawn = true;
        }
    }

    IEnumerator LowEnergy()
    {
        while (energy.CurrentVal > 0 && energy.CurrentVal < 30)
        {
            energyBar.SetActive(false);
            yield return new WaitForSeconds(0.8f);
            energyBar.SetActive(true);
            yield return new WaitForSeconds(0.8f);
        }
    }

    void setText()
    {
        countText.text = "Count: " + count.ToString();
        expText.text = "Exp: " + exp.ToString();
    }

    

    public void ReadyButton()
    {
        ready = true;
        button.SetActive(false);
    }
    
    private void Awake()
    {
        energy.Initialize();
    }

    public void DbUpdate()
    {
        using (SqlConnection connection = new SqlConnection(DbConnection.connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("UPDATE PetInformation Set CurrentEnergy = CurrentEnergy - @situp, Situps = Situps + @situps, Exp = Exp + @exp, Gold = Gold + @gold  WHERE PetId = (SELECT PlayerID FROM  PlayerInformation WHERE Name = @name)", connection);
            cmd.Parameters.AddWithValue("@situp", situpCount);
            cmd.Parameters.AddWithValue("@situps", situpCount);
            cmd.Parameters.AddWithValue("@exp", exp);
            cmd.Parameters.AddWithValue("@name", myName);

            if (situpCount < 10)
            {
                cmd.Parameters.AddWithValue("@gold", 0);
            }
            else if (situpCount >= 10 && situpCount < 20)
            {
                cmd.Parameters.AddWithValue("@gold", 10);
            }
            else
            {
                cmd.Parameters.AddWithValue("@gold", 20);
            }

            cmd.ExecuteNonQuery();
        }
    }
}
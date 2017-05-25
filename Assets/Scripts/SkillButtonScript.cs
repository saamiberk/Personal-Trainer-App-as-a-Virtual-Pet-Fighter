using UnityEngine;
using UnityEngine.UI;

public class SkillButtonScript : MonoBehaviour {

    public GameObject atk1_char0;
    public GameObject atk1_char1;
    public GameObject atk1_char2;
    public GameObject atk2_char0;
    public GameObject atk2_char1;
    public GameObject atk2_char2;
    public GameObject def1_char0;

    private string btnAtk1 = "AttackButton1";
    private string btnAtk2 = "AttackButton2";
    private string btnDef = "DefenseButton";

    private int character;

    private bool isOpen = false;
    private Animator anim;

    private float wait = 1;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        wait -= Time.deltaTime;
        if(wait <= 0)
        {
            if(gameObject.name == "AttackButton1" || gameObject.name == "AttackButton2")
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                gameObject.GetComponent<Button>().onClick.AddListener
                    (player.GetComponent<ActivateCharacter>().Atk);
            }
        }

    }
    public void playAnimation()
    {
        if (isOpen == false)
        {
            anim.SetInteger("State", 1);
            isOpen = true;
        }
        else if (isOpen == true)
        {
            anim.SetInteger("State", 2);
            isOpen = false;
        }
    }

    public void spawnSkill()
    {
        if (login.characterNumber == 0 || login.characterNumber == 3 || login.characterNumber == 6)
        {
            if (gameObject.name == btnAtk1)
            {
                Instantiate(atk1_char0, transform.position, Quaternion.identity);
            }
            if (gameObject.name == btnAtk2)
            {
                Instantiate(atk2_char0, transform.position, Quaternion.identity);
            }
        }
        if(login.characterNumber == 1 || login.characterNumber == 4 || login.characterNumber == 7)
        {
            if (gameObject.name == btnAtk1)
            {
                Instantiate(atk1_char1, transform.position, Quaternion.identity);
            }
            if (gameObject.name == btnAtk2)
            {
                Instantiate(atk2_char1, transform.position, Quaternion.identity);
            }
        }
        if (login.characterNumber == 2 || login.characterNumber == 5 || login.characterNumber == 8)
        {
            if (gameObject.name == btnAtk1)
            {
                Instantiate(atk1_char2, transform.position, Quaternion.identity);
            }
            if (gameObject.name == btnAtk2)
            {
                Instantiate(atk2_char2, transform.position, Quaternion.identity);
            }
        }

        if (gameObject.name == btnDef)
        {
            Instantiate(def1_char0, transform.position, Quaternion.identity);
        }
    }
}

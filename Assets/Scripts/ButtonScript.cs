using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonScript : MonoBehaviour {



    [SerializeField]
    private GameObject[] panel;
    [SerializeField]
    private GameObject[] button;
    [SerializeField]
    private Animator[] anim;
    [SerializeField]
    private GameObject sporButton;



    private bool check = true;
    private bool boolean = true;
   

	public void Button_Click()
    {
        panel[1].SetActive(false);
        check = true;
        sporButton.SetActive(false);
        button[0].SetActive(false);
        panel[0].SetActive(true);
        button[1].SetActive(true);
        All_Animation();
        
    }

    public void Cancel_Click()
    {
        panel[1].SetActive(false);
        check = true;
        button[0].SetActive(true);
        panel[0].SetActive(false);
        button[1].SetActive(false);
        sporButton.SetActive(false);
       
    }

   public void Settings_Click()
    {
       
        if (check)
        {
         
            panel[1].SetActive(true);           
            check = false;

        }

        else
        {
            panel[1].SetActive(false);
            check = true;
        }
           
               
    }

    public void Resume()
    {
        panel[1].SetActive(false);
        check = true;
        
    }

    public void All_Animation()
    {
       
        anim[0].Play("button_Click", -1, 0f);
        anim[1].Play("button_Click2", -1, 0f);
        anim[3].Play("bShopNext", -1, 0f);
        anim[2].Play("button_Click3", -1, 0f);

    }

    public void Information_Button()
    {
        if (boolean)
        {
            panel[2].SetActive(true);
            boolean = false;

        }

        else
        {
            panel[2].SetActive(false);
            boolean = true;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
        
    }

    public void ClickSporButton()
    {
        sporButton.SetActive(true);
        button[0].SetActive(true);
        panel[0].SetActive(false);
        button[1].SetActive(false);
        button[1].SetActive(true);
        button[0].SetActive(false);
      
    }
    
    public void SitUpButton()
    {
        LoadingScreenManager.LoadScene("SitupExercise");
    }

    public void FightScreen()
    {
        LoadingScreenManager.LoadScene("Training Ground");
    }

    public void FoodScreen()
    {
        LoadingScreenManager.LoadScene("FoodScreen");
    }

    public void PushupScreen()
    {
        LoadingScreenManager.LoadScene("PushupExercise");
    }

    public void Running()
    {
        LoadingScreenManager.LoadScene("Walking Exercise");
    }

    public void Home()
    {
        if (login.characterNumber == 0 || login.characterNumber == 3 || login.characterNumber == 6)
        {
            LoadingScreenManager.LoadScene("MainScreen-L");
        }
        if (login.characterNumber == 1 || login.characterNumber == 4 || login.characterNumber == 7)
        {
            LoadingScreenManager.LoadScene("MainScreen-F");
        }
        if (login.characterNumber == 2 || login.characterNumber == 5 || login.characterNumber == 8)
        {
            LoadingScreenManager.LoadScene("MainScreen-S");
        }
    }
}

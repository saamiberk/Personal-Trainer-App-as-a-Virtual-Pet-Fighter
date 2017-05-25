using UnityEngine;
using System.Collections;

public class ChangeAccount : MonoBehaviour {

    public void ReturnToLogin()
    {
        LoadingScreenManager.LoadScene("LoginScreen");
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public GameObject tools;
    private List<GameObject> models;
    private int Idle = 0;
    private int Walking = 1;

    public float speed = 2f;
    public bool buttonDownRight = false;
    public bool buttonDownLeft = false;
    public bool changeAnimation = false;
    public bool rotation = false;

    private int moveRight_model_x; private int moveRight_tool_x;
    private int moveRight_model_y; private int moveRight_tool_y;
    private int moveRight_model_z; private int moveRight_tool_z;

    private int moveLeft_model_x; private int moveLeft_tool_x;
    private int moveLeft_model_y; private int moveLeft_tool_y;
    private int moveLeft_model_z; private int moveLeft_tool_z;

    private int stopFalse_model_x; private int stopFalse_tool_x;
    private int stopFalse_model_y; private int stopFalse_tool_y;
    private int stopFalse_model_z; private int stopFalse_tool_z;

    private int stopTrue_model_x; private int stopTrue_tool_x;
    private int stopTrue_model_y; private int stopTrue_tool_y;
    private int stopTrue_model_z; private int stopTrue_tool_z;

    void Start()
    {
        models = new List<GameObject>();
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        models[Idle].SetActive(true);

        ConfigureRotation();
    }

    void Update()
    {
        if (buttonDownRight == true)
        {
            Debug.Log("Move right method has been called.");
            moveRight();
        }
        if (buttonDownLeft == true)
        {
            Debug.Log("Move left method has been called.");
            moveLeft();
        }
        if (buttonDownRight == false && buttonDownLeft == false)
        {
            Debug.Log("Stop method has been called.");
            stop();
        }
    }

    public void moveRight()
    {
        transform.Translate(speed * Time.deltaTime, 0f, 0f);
        if (changeAnimation == false)
        {
            models[Idle].SetActive(false);
            models[Walking].SetActive(true);
            models[Walking].transform.eulerAngles = new Vector3(moveRight_model_x, moveRight_model_y, moveRight_model_z);
            tools.transform.eulerAngles = new Vector3(moveRight_tool_x, moveRight_tool_y, moveRight_tool_z);
            rotation = false;
            changeAnimation = true;
        }

    }

    public void moveLeft()
    {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        if (changeAnimation == false)
        {
            models[Idle].SetActive(false);
            models[Walking].SetActive(true);
            models[Walking].transform.eulerAngles = new Vector3(moveLeft_model_x, moveLeft_model_y, moveLeft_model_z);
            tools.transform.eulerAngles = new Vector3(moveLeft_tool_x, moveLeft_tool_y, moveLeft_tool_z);
            rotation = true;
            changeAnimation = true;
        }
    }

    public void stop()
    {
        transform.Translate(0f, 0f, 0f);
        if (changeAnimation == true)
        {
            models[Idle].SetActive(true);
            models[Walking].SetActive(false);
            changeAnimation = false;
            if (rotation == true)
            {
                models[Idle].transform.eulerAngles = new Vector3(stopTrue_model_x, stopTrue_model_y, stopTrue_model_z);
                tools.transform.eulerAngles = new Vector3(stopTrue_tool_x, stopTrue_tool_y, stopTrue_tool_z);
            }
            if (rotation == false)
            {
                models[Idle].transform.eulerAngles = new Vector3(stopFalse_model_x, stopFalse_model_y, stopFalse_model_z);
                tools.transform.eulerAngles = new Vector3(stopFalse_tool_x, stopFalse_tool_y, stopFalse_tool_z);
            }
        }
    }

    public void onPointerDownRight()
    {
        buttonDownRight = true;
    }

    public void onPointerUpRight()
    {
        buttonDownRight = false;
    }

    public void onPointerDownLeft()
    {
        buttonDownLeft = true;
    }

    public void onPointerUpLeft()
    {
        buttonDownLeft = false;
    }

    void ConfigureRotation()
    {
        if (login.characterNumber == 1 || login.characterNumber == 3 || login.characterNumber == 4 || login.characterNumber == 7)
        {
            moveRight_model_x = 0; /**/ moveRight_model_y = 180; /**/ moveRight_model_z = 0;
        }
        else
        {
            moveRight_model_x = 0; /**/ moveRight_model_y = 0; /**/ moveRight_model_z = 0;
        }

        if (login.characterNumber == 1 || login.characterNumber == 3 || login.characterNumber == 4 || login.characterNumber == 7)
        {
            moveLeft_model_x = 0; /**/ moveLeft_model_y = 0; /**/ moveLeft_model_z = 0;
        }
        else
        {
            moveLeft_model_x = 0; /**/ moveLeft_model_y = 180; /**/ moveLeft_model_z = 0;
        }

        if (login.characterNumber == 0 || login.characterNumber == 1 || login.characterNumber == 2 || login.characterNumber == 4)
        {
            stopTrue_model_x = 0; /**/ stopTrue_model_y = 0; /**/ stopTrue_model_z = 0;
        }
        else
        {
            stopTrue_model_x = 0; /**/ stopTrue_model_y = 180; /**/ stopTrue_model_z = 0;
        }

        if (login.characterNumber == 0 || login.characterNumber == 1 || login.characterNumber == 2 || login.characterNumber == 3 || login.characterNumber == 4)
        {
            stopFalse_model_x = 0; /**/ stopFalse_model_y = 180; /**/ stopFalse_model_z = 0;
        }
        else
        {
            stopFalse_model_x = 0; /**/ stopFalse_model_y = 0; /**/ stopFalse_model_z = 0;
        }

        moveRight_tool_x = 90; /**/ moveRight_tool_y = 180; /**/ moveRight_tool_z = 180;
        moveLeft_tool_x = 90; /**/ moveLeft_tool_y = 0; /**/ moveLeft_tool_z = 180;
        stopTrue_tool_x = 90; /**/ stopTrue_tool_y = 0; /**/ stopTrue_tool_z = 180;
        stopFalse_tool_x = 90; /**/ stopFalse_tool_y = 180; /**/ stopFalse_tool_z = 180;
    }
}

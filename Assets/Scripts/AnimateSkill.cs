using UnityEngine;
using UnityEngine.UI;

public class AnimateSkill : MonoBehaviour {

    public Sprite[] spriteFrames;
    private int frameNumber = 0;
    private int framesPerSecond = 5;

    private bool startAnimation = false;

    private float minX = 0.15f;
    private float maxX = 0.15f;
    private float rotationY = 0;

    private Vector3 pos;
    private int loopCounter;

    private bool start;

    private float time;

    void Update()
    {
        playSkill(rotationY);
    }

    void playSkill(float y)
    {
        if (gameObject.name == "Skill0")
        {
            y = 180;
            if (startAnimation == false)
            {
                gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(minX, 0.3f);
                gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(maxX, 0.3f);
                minX += (float)0.5 * Time.deltaTime;
                maxX += (float)0.5 * Time.deltaTime;

                if (minX >= 1.5f && maxX >= 1.5f)
                {
                    startAnimation = true;
                    gameObject.GetComponentInParent<CameraPosition>().destroy = true;
                }
            }
        }

        if (gameObject.name == "Skill1")
        {
            y = 180;
            if(loopCounter == 2)
            {
                gameObject.GetComponentInParent<CameraPosition>().destroy = true;
            }
        }

        if (gameObject.name == "Skill2")
        {
            y = 180;
            if (loopCounter == 2)
            {
                gameObject.GetComponentInParent<CameraPosition>().destroy = true;
            }
        }

        if (gameObject.name == "Skill3")
        {
            if (startAnimation == false)
            {
                gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(minX, 0.35f);
                gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(maxX, 0.35f);
                minX += (float)0.5 * Time.deltaTime;
                maxX += (float)0.5 * Time.deltaTime;

                if (minX >= 1.5f && maxX >= 1.5f)
                {
                    startAnimation = true;
                    gameObject.GetComponentInParent<CameraPosition>().destroy = true;
                }
            }
        }

        if (gameObject.name == "Skill4")
        {
            y = 180;
            framesPerSecond = 10;
            if (startAnimation == false)
            {
                gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(minX, 0.3f);
                gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(maxX, 0.3f);
                minX += (float)0.7 * Time.deltaTime;
                maxX += (float)0.7 * Time.deltaTime;

                if (minX >= 1.5f && maxX >= 1.5f)
                {
                    startAnimation = true;
                    gameObject.GetComponentInParent<CameraPosition>().destroy = true;
                }
            }
        }

        if (gameObject.name == "Skill5")
        {
            y = 180;
            if (loopCounter == 2)
            {
                gameObject.GetComponentInParent<CameraPosition>().destroy = true;
            }
        }

        if (gameObject.name == "DefSkill")
        {
            if (loopCounter == 2)
            {
                gameObject.GetComponentInParent<CameraPosition>().destroy = true;
            }
        }

        gameObject.GetComponent<RectTransform>().rotation = new Quaternion(0, y, 0, 0);

        frameNumber = (int)(time * framesPerSecond) % spriteFrames.Length;
        time += Time.deltaTime * 2;
        //Debug.Log("Frame: " + frameNumber);
        gameObject.GetComponent<Image>().sprite = spriteFrames[frameNumber];
        //Debug.Log("Time: " + time);

        if (frameNumber >= spriteFrames.Length - 1)
        {
            loopCounter++;
            frameNumber = 0;
        }
    }
}

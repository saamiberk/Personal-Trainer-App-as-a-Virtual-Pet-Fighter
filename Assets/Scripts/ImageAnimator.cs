using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour {

    public Sprite[] spriteFrames;
    private int frameNumber = 0;
    private int framesPerSecond = 5;

    private float time = 0;
    public bool sleep = false;

    void Update () {

        if(!(gameObject.name == "Zzz"))
        {
            animateImage(2);
        }
        else
        {
            if(sleep == true)
            {
                animateImage(0.5f);
            }
        }
    }

    public void animateImage(float speed)
    {
        frameNumber = (int)(time * framesPerSecond) % spriteFrames.Length;
        time += Time.deltaTime * speed;
        //Debug.Log("Frame: " + frameNumber);
        gameObject.GetComponent<Image>().sprite = spriteFrames[frameNumber];
        //Debug.Log("Time: " + time);
    }
}

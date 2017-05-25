using UnityEngine;
using UnityEngine.UI;

public class SweatScript : MonoBehaviour {

    public Sprite[] spriteFrames;
    private int frameNumber = 0;
    private int framesPerSecond = 3;

    private int loopCounter;

    private float time;

    void Update () {
        time += Time.deltaTime * 2;

        frameNumber = (int)(time * framesPerSecond) % spriteFrames.Length;        
        gameObject.GetComponent<Image>().sprite = spriteFrames[frameNumber];

        if (frameNumber >= spriteFrames.Length - 1)
        {
            loopCounter++;
            frameNumber = 0;
        }

        if (loopCounter == 2)
        {
            gameObject.GetComponentInParent<CameraPosition>().destroy = true;
        }
    }

    public void ConfigureSweat(float x, float y, float s)
    {
        gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(x, y);
        gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(x, y);
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(s, 1, 1);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExerciseInfo : MonoBehaviour {

    [SerializeField]
    private GameObject exerciseImage;
    [SerializeField]
    private GameObject infoButton;
    [SerializeField]
    private Text[] infoTexts;
    [SerializeField]
    private GameObject parentCanvas;

    IEnumerator ShowExerciseInfo()
    {
        Debug.Log("Corouting has started.");
        infoButton.SetActive(false);
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(1);
        exerciseImage.GetComponent<CanvasRenderer>().SetAlpha(1);
        infoTexts[0].color = new Color(infoTexts[0].color.r, infoTexts[0].color.g, infoTexts[0].color.b, 1);
        infoTexts[1].color = new Color(infoTexts[1].color.r, infoTexts[1].color.g, infoTexts[1].color.b, 1);
    
        exerciseImage.GetComponent<Image>().CrossFadeAlpha(0, 0.5f, true);
        gameObject.GetComponent<Image>().CrossFadeAlpha(0, 0.5f, true);
        while (infoTexts[0].color.a > 0.0f && infoTexts[1].color.a > 0.0f)
        {
            infoTexts[0].color = new Color(infoTexts[0].color.r, infoTexts[0].color.g, infoTexts[0].color.b, infoTexts[0].color.a - (Time.deltaTime) * 2);
            infoTexts[1].color = new Color(infoTexts[1].color.r, infoTexts[1].color.g, infoTexts[1].color.b, infoTexts[1].color.a - (Time.deltaTime) * 2);
            yield return null;
        }
        parentCanvas.SetActive(false);
    }

    public void CloseInfoPanel()
    {
        StartCoroutine("ShowExerciseInfo");
    }
}

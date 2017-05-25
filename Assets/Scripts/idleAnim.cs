using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleAnim : MonoBehaviour
{
    public string animNo;
    public string animIdle;
    public string animAtk;
    private bool start;

    void Start()
    {
        SmoothMoves.BoneAnimation boneAni = gameObject.GetComponent<SmoothMoves.BoneAnimation>();
        string aniName = boneAni.GetAnimationClipName(0);
        animNo = aniName.Split('_')[1];
        animIdle = "Idle_" + animNo;
        animAtk = "Atk_" + animNo;
        start = true;
    }

    void Update()
    {   
        if(!(gameObject.scene.name == "Training Ground"))
        {
            if(login.characterNumber == 0 || login.characterNumber == 1 || login.characterNumber == 2 
                || login.characterNumber == 3 || login.characterNumber == 4 || login.characterNumber == 5 
                || login.characterNumber == 6 || login.characterNumber == 7 || login.characterNumber == 8)
            {
                if (start == true)
                {
                    StartCoroutine("animate");
                    start = false;
                }
            }
        }
    }

    IEnumerator animate()
    {
        SmoothMoves.BoneAnimation boneAni = gameObject.GetComponent<SmoothMoves.BoneAnimation>();

        int i = boneAni.GetAnimationClipIndex(animAtk);
        if (i == -1) yield break;

        if (boneAni.IsPlaying(animAtk) == true) boneAni.Stop();

        //Debug.Log(animAtk.Length);

        boneAni.CrossFade(animAtk);
        yield return new WaitForSeconds(boneAni[animAtk].length);

        //if (boneAni != null)
        boneAni.CrossFade(animIdle);
    }

    public void startCoroutine()
    {

        if (gameObject.activeInHierarchy)
        {
            StartCoroutine("animate");
        }
    }
}

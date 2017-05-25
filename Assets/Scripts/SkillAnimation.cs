using System.Collections;
using UnityEngine;

public class SkillAnimation : MonoBehaviour {

    public string animNo;
    public string animIdle;
    public string animAtk;
    private bool start;

    void Start () {
        SmoothMoves.BoneAnimation boneAni = gameObject.GetComponent<SmoothMoves.BoneAnimation>();
        string aniName = boneAni.GetAnimationClipName(0);
        animNo = aniName.Split('_')[1];
        animIdle = "Idle_" + animNo;
        animAtk = "Atk_" + animNo;
    }

    IEnumerator animate()
    {
        SmoothMoves.BoneAnimation boneAni = gameObject.GetComponent<SmoothMoves.BoneAnimation>();

        int i = boneAni.GetAnimationClipIndex(animAtk);
        if (i == -1) yield break;

        if (boneAni.IsPlaying(animAtk) == true) boneAni.Stop();

        Debug.Log(animAtk.Length);

        boneAni.CrossFade(animAtk);
        yield return new WaitForSeconds(boneAni[animAtk].length);

        //if (boneAni != null)
            boneAni.CrossFade(animIdle);
    }

    public void startCoroutine()
    {

        if (gameObject.activeInHierarchy)
        {
            Debug.Log("");
            StartCoroutine("animate");
        }      
    } 
}

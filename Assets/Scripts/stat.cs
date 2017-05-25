using UnityEngine;
using System;

[Serializable]
public class stat
{
    [SerializeField]
    private barScript bar;
    [SerializeField]
    private float maxVal;
    [SerializeField]
    private float currentVal;

    public float CurrentVal
    {
        get
        {
            return currentVal;

        }

        set
        {          
            this.currentVal = Mathf.Clamp(value, 0, MaxVal);
            bar.Value = currentVal;
         
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {
            bar.MaxValue = maxVal;
            this.maxVal = value;
        }
    }

    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}

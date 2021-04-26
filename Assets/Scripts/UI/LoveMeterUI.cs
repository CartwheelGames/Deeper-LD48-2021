using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveMeterUI : MonoBehaviour {
    
    [SerializeField] private RectTransform _meterFill;

    public void SetPercentageFilled(float percentage){

        percentage = Math.Min(Math.Max(percentage, 0), 1);
        _meterFill.localScale = new Vector3(1f, percentage, 1f);
    }
}

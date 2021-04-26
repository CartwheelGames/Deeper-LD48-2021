using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SideTextController : MonoBehaviour
{
    [SerializeField] private TextMeshPro _rejectText;
    [SerializeField] private TextMeshPro _acceptText;
    
    public void TurnOnSidePrompts(){
        
        _acceptText.gameObject.SetActive(true);
        _rejectText.gameObject.SetActive(true);
    }
    
    public void TurnOffSidePrompts(){
        
        _acceptText.gameObject.SetActive(false);
        _rejectText.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardSwitcherUI : MonoBehaviour {

    [SerializeField] private CardUI _cardA;
    [SerializeField] private CardUI _cardB;

    private bool _frontCardIsA = true;
    private CardUI _CurrentCard => _frontCardIsA ? _cardA : _cardB;
    private CardUI _OtherCard => _frontCardIsA ? _cardB : _cardA;

    private void Start(){
        
        _CurrentCard.MoveToFront();
    }
    
    public void FlipCurrentCard(bool isLeft){

        _CurrentCard.Flip(isLeft);
    }

    public void TransitionOutCurrentCard(){

        _CurrentCard.TransitionOut();
        _OtherCard.MoveToFront();

        _frontCardIsA = !_frontCardIsA;
    }

    public void SetCurrentCardData(CardPayload cardPayload){

        _CurrentCard.SetCardPayload(cardPayload);
    }
}

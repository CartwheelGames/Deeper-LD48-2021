using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardSwitcherUI : MonoBehaviour {

    [SerializeField] private CardUI _cardA;
    [SerializeField] private CardUI _cardB;
    [SerializeField] private Camera _camera;

    private bool _frontCardIsA = true;
    private CardUI _CurrentCard => _frontCardIsA ? _cardA : _cardB;
    private CardUI _OtherCard => _frontCardIsA ? _cardB : _cardA;

    private void Start(){
        
        _CurrentCard.MoveToFront();
        _OtherCard.MoveToBack();
    }
    
    public void FlipCurrentCard(bool isLeft){

        _CurrentCard.Flip(isLeft);
        
        // turn the other card off while the current card is flipping bc
        // it's not updated yet and so it will look different than when
        // it will be revealed
        _OtherCard.TurnOff();
    }

    public void TransitionOutCurrentCard(){

        // Turn it back on again, now that it's about to be updated
        _OtherCard.TurnOn();
        
        _CurrentCard.TransitionOut();
        _OtherCard.MoveToFront();

        _frontCardIsA = !_frontCardIsA;
    }

    public void SetCurrentCardData(CardPayload cardPayload){

        _CurrentCard.SetCardPayload(cardPayload);
        
        SetCameraBackgroundColor(cardPayload.BackgroundColor);
    }

    private void SetCameraBackgroundColor(Color backgroundColor){

        _camera.backgroundColor = backgroundColor;
    }
}

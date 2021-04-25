using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour {

    private Sprite CardFrontImage { set => _cardFrontImage.sprite = value; }
    private Sprite CardBackImage { set => _cardBackImage.sprite = value; }
    private string Description { set => _descriptionText.text = value; }
    private string Outcome { set => _outcomeText.text = value; }
    
    [SerializeField] private SpriteRenderer _cardFrontImage;
    [SerializeField] private SpriteRenderer _cardBackImage;
    [SerializeField] private TextMeshPro _descriptionText;
    [SerializeField] private TextMeshPro _outcomeText;
    [SerializeField] private Animator _animator;
    
    private CardPayload _cardPayload;

    public void SetCardPayload(CardPayload cardPayload){
        
        _cardPayload = cardPayload;

        Description = _cardPayload.Description;
    }
    public void Flip(bool isLeft){
        PlayAnim(isLeft ? "FlipLeft" : "FlipRight");

        Outcome = isLeft ? _cardPayload.RejectResponse : _cardPayload.AcceptResponse;
    }

    public void TransitionOut(){
        PlayAnim("TransitionOut");
    }

    public void MoveToFront(){
        PlayAnim("MoveToFront");
    }
    public void MoveToBack(){
        PlayAnim("MoveToBack");
    }

    private void PlayAnim(string anim){
        _animator.CrossFade(anim, 0f);
    }
}

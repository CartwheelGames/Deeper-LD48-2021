using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour {

    private Sprite CardFrontImage { set => _cardFrontImage.sprite = value; }
    private Sprite CardBackImage { set => _cardBackImage.sprite = value; }
    private string Description { set => _descriptionText.text = value; }
    private string Outcome { set => _outcomeText.text = value; }
    private string RejectPrompt { set => _rejectText.text = value; }
    private string AcceptPrompt { set => _acceptText.text = value; }
    
    [SerializeField] private SpriteRenderer _cardFrontImage;
    [SerializeField] private SpriteRenderer _cardBackImage;
    [SerializeField] private TextMeshPro _descriptionText;
    [SerializeField] private TextMeshPro _outcomeText;
    [SerializeField] private TextMeshPro _rejectText;
    [SerializeField] private TextMeshPro _acceptText;
    [SerializeField] private Animator _animator;
    
    private CardPayload _cardPayload;

    public void SetCardPayload(CardPayload cardPayload){
        
        _cardPayload = cardPayload;

        Description = _cardPayload.Description;
        RejectPrompt = _cardPayload.RejectText;
        AcceptPrompt = _cardPayload.AcceptText;

        if (_cardPayload.Location != null)
        {
            CardFrontImage = _cardPayload.Location;
            CardBackImage = _cardPayload.Location;
        }
    }

    public void TurnOff(){
        
        gameObject.SetActive(false);
    }
    
    public void TurnOn(){
        
        gameObject.SetActive(true);
    }

    public void Flip(bool isLeft){
        
        PlayAnim(isLeft ? "FlipLeft" : "FlipRight");

        Outcome = isLeft ? _cardPayload.RejectResponse : _cardPayload.AcceptResponse;
    }

    public void TransitionOut(){

        var f = this;
        PlayAnim("TransitionOut");
    }

    public void MoveToFront(){

        var f = this;
        PlayAnim("MoveToFront");
    }
    public void MoveToBack(){
        
        PlayAnim("MoveToBack");
    }

    private void PlayAnim(string anim){
        
        _animator.CrossFade(anim, 0f);
    }
}

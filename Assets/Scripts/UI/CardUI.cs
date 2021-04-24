using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour {

    public Sprite CardBackImage { set => _cardBackImage.sprite = value; }
    public string Description { set => _descriptionText.text = value; }
    
    [SerializeField] private SpriteRenderer _cardBackImage;
    [SerializeField] private TextMeshPro _descriptionText;
    [SerializeField] private Animator _animator;

    public void Flip(bool isLeft){
        PlayAnim(isLeft ? "FlipLeft" : "FlipRight");
    }

    private void PlayAnim(string anim){
        _animator.CrossFade(anim, 0.5f);
    }
}

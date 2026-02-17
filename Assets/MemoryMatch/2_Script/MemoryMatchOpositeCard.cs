using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class MemoryMatchOpositeCard : MonoBehaviour
{
    private Image selfImage;
    private GameObject sticker;
    private void Awake()
    {
        selfImage = GetComponent<Image>();
        sticker = transform.GetChild(0).gameObject;
        ResetOpositeCards();
    }

    public void SetSelfImage(Sprite sp1)
    {
        selfImage.sprite = sp1;

    }
    public void SetStickerImage(Sprite sp2)
    {
        Image img = sticker.GetComponent<Image>();
        img.sprite = sp2;
       //img.SetNativeSize();
    }
   
    public void ResetOpositeCards()
    {
        Vector3 v = transform.localScale;
        v.x = 0;
        transform.localScale = v;
    }
    public void FlipOpositeCards(int flipVal)
    {
        transform.DOScaleX(flipVal, 0.1f).SetEase(Ease.Linear);
    }
}

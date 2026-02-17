using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MemoryMatchCards : MonoBehaviour, IPointerClickHandler
{
    private int id;
    private MemoryMatchOpositeCard opositeSide;

    public bool isClick, isMatch;

    public int ID => id;

    private MemoryMatchManager instance;
    private void Start()
    {
        instance = MemoryMatchManager.instance;
        StartCoroutine(PlayTutorialFlip());
    }
    public void SetId(int id)
    {
        this.id = id;
    }

    private IEnumerator PlayTutorialFlip()
    {
        isClick = true;
        OnOpositeCard();
        yield return new WaitForSeconds(1.5f);
        OffOpositeCard();
        isClick = false;
    }
    public void SetImage(Sprite sp)
    {
        GetComponent<Image>().sprite = sp;
    }

    public void SetOpoSiteSide(MemoryMatchOpositeCard go)
    {
        opositeSide = go;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMatch || isClick || instance.isMatchRunning) return;

        Debug.Log("pointerClick");
        isClick = true;
       
        OnOpositeCard();
        // First card tap
        if (instance.tapCount == 0)
        {
            instance.selectedCard = this;
        }

        instance.tapCount++;

        // Second card tap
        if (instance.tapCount == 2)
        {
            instance.isMatchRunning = true;
            var selected = instance.selectedCard;

            if (selected != null && selected.ID == id)
            {

                selected.isMatch = true;
                isMatch = true;
                MatchCards();
                instance.tapCount = 0;
                Debug.Log("Match");
            }
            else
            {
                Invoke(nameof(ResetData), 1f);
                Debug.Log("NotMatch");
                
            }
            Invoke(nameof(MatchRunningBoolFalse), 1.5f);
        }
        
    }

    private void ResetData()
    {
        var selected = instance.selectedCard;

        isClick = false;

        if (selected != null)
        {
            OffOpositeCard();
            selected.OffOpositeCard();
            selected.isClick = false;

           
        }

        instance.tapCount = 0;
        instance.selectedCard = null;
        instance.selectedCardId = -1;
    }

    public void MatchCards()
    {
       
        instance.MatchIncreased();
    }
    private void MatchRunningBoolFalse()
    {
        instance.isMatchRunning = false;
    }


    private void PlayFlipAnimation(int flipVal)
    {
        transform.DOScaleX(flipVal, 0.1f).SetEase(Ease.Linear);
    }
    public void OnOpositeCard()
    {
        StartCoroutine(OpositeCardOn());
    }
    private IEnumerator OpositeCardOn()
    {
        PlayFlipAnimation(0);
        yield return new WaitForSeconds(0.1f);
        opositeSide.GetComponent<MemoryMatchOpositeCard>().FlipOpositeCards(1);
    }
    public void OffOpositeCard()
    {
        StartCoroutine(OpositeCardOff());
    }
    private IEnumerator OpositeCardOff()
    {
        opositeSide.GetComponent<MemoryMatchOpositeCard>().FlipOpositeCards(0);
        yield return new WaitForSeconds(0.1f);
        PlayFlipAnimation(1);
    }
}

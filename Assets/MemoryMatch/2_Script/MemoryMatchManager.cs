using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MemoryMatchManager : MonoBehaviour
{
    public static MemoryMatchManager instance;

    [SerializeField] List<Sprite> allcardsSprite, allOpositeSideCardsSprite;
    [SerializeField] List<MemoryMatchCards> allCardsImage;
    [SerializeField] List<MemoryMatchOpositeCard> allOpositeSideImage;
    public List<Sprite> allStickers;
    [HideInInspector] public int selectedCardId = -1, tapCount;
    [HideInInspector] public bool isMatchRunning;
    [HideInInspector] public MemoryMatchCards selectedCard;
    private int matchCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

    }
    void Start()
    {
        
        GameStart();
       
    }

    public void GameStart()
    {
        LoadRandomCards();
        CardImageSuffle();
        LoadStickerOnCards();
    }
    private void LoadRandomCards()
    {
        int randCard = UnityEngine.Random.Range(0, allcardsSprite.Count);

        for (int i = 0; i < allCardsImage.Count; i++)
        {
            allCardsImage[i].SetImage(allcardsSprite[randCard]);
            allOpositeSideImage[i].SetSelfImage(allOpositeSideCardsSprite[randCard]);
        }
    }
    private void CardImageSuffle()
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < allCardsImage.Count; i++)
        {
            int randomIndex = rnd.Next(i, allCardsImage.Count); // pick from i to end

            // swap using temp
            MemoryMatchCards temp = allCardsImage[i];
            allCardsImage[i] = allCardsImage[randomIndex];
            allCardsImage[randomIndex] = temp;

            MemoryMatchOpositeCard temp1 = allOpositeSideImage[i];
            allOpositeSideImage[i] = allOpositeSideImage[randomIndex];
            allOpositeSideImage[randomIndex] = temp1;
        }
        allStickers = allStickers.OrderBy(x => Guid.NewGuid()).ToList();
    }

    private void LoadStickerOnCards()
    {
        for (int i = 0; i < allOpositeSideImage.Count; i += 2)
        {
            allOpositeSideImage[i].SetStickerImage(allStickers[i / 2]);
            allOpositeSideImage[i + 1].SetStickerImage(allStickers[i / 2]);
            allCardsImage[i].SetId(i / 2);
            allCardsImage[i + 1].SetId(i / 2);
            allCardsImage[i].SetOpoSiteSide(allOpositeSideImage[i]);
            allCardsImage[i + 1].SetOpoSiteSide(allOpositeSideImage[i + 1]);
        }
    }
    
    public void MatchIncreased()
    {
        instance.matchCount++;
        if (matchCount == 4)
        {
            GameOver();
            SoundManager.Instance.PlayGameOver();

        }
    }
    void GameOver()
    {
        Invoke("ReloadGame", 3f);
    }
    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}

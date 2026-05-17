using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using DG.Tweening;

public class CardManager : MonoBehaviour
{
    public static CardManager sigleton { private set; get; }
    public GameObject deckCardPrefab;
    public GameObject deckCardEffect;
    public GameObject emptyUIPrefab;
    public Transform deckList;
    public Transform canvas;
    public Transform onTopCanvas;
    public Transform center;
    public Volume zoomInPost;
    public CardBehaviour selectedCard;

    public GameObject slaveObject;
    private bool slaveBool;
    private void Awake()
    {
        sigleton = this;
    }

    private void Update()
    {
        if (slaveBool)
        {
            zoomInPost.weight = slaveObject.transform.localScale.x;
        }
    }

    public void AddToDeck(CardBehaviour card)
    {
        if (deckList.childCount<8)
        {
            GameObject placeHolder = Instantiate(emptyUIPrefab, deckList);
            placeHolder.name = "placeholder";
            LayoutRebuilder.ForceRebuildLayoutImmediate(deckList.GetComponent<RectTransform>());

            GameObject a2bEffect = Instantiate(deckCardEffect, card.transform.position, Quaternion.identity, canvas);
            a2bEffect.GetComponent<DeckListA2B>().Destination = placeHolder.transform.position;

            a2bEffect.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = card.cardName;
            //a2bEffect.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = "" + card.cost;

            a2bEffect.transform.Find("Outer Mana Crystal").GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + card.cost; //horrible code

            GameObject deckCard = Instantiate(deckCardPrefab, deckList);

            deckCard.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = card.cardName;
            //deckCard.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = ""+ card.cost;

            deckCard.transform.Find("Outer Mana Crystal").GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + card.cost; //horrible code
        }

    }

    public void AddToDeck(CardScript card)
    {
        if (deckList.childCount < 9)
        {
            GameObject placeHolder = Instantiate(emptyUIPrefab, deckList);
            placeHolder.name = "placeholder";
            LayoutRebuilder.ForceRebuildLayoutImmediate(deckList.GetComponent<RectTransform>());

            GameObject a2bEffect = Instantiate(deckCardEffect, card.transform.position, Quaternion.identity, canvas);
            a2bEffect.GetComponent<DeckListA2B>().Destination = placeHolder.transform.position;

            a2bEffect.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = card.cardName;

            a2bEffect.transform.Find("Outer Mana Crystal").GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + card.cost; //horrible code

            GameObject deckCard = Instantiate(deckCardPrefab, deckList);

            deckCard.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = card.cardName;

            deckCard.transform.Find("Outer Mana Crystal").GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + card.cost; //horrible code
        }

    }

    public void UpdateDeck()
    {
        //Destroy(deckList.GetChild(deckList.childCount-2).gameObject);
        Destroy(deckList.Find("placeholder").gameObject);
        //deckList.GetChild(deckList.childCount - 1).gameObject.SetActive(true);
        for (int i=0;i<deckList.childCount;i++)
        {
            if (deckList.GetChild(i).gameObject.activeSelf == false)
            {
                deckList.GetChild(i).gameObject.SetActive(true);
                return;
            }
        }
    }

    public void ZoomInOnCard(CardScript card)
    {
        canvas.GetComponent<GraphicRaycaster>().enabled = false;

        GameObject dupliCard = Instantiate(card.gameObject, card.transform.position, Quaternion.identity, onTopCanvas);

        dupliCard.GetComponent<CardScript>().zoomInCard = true;
        dupliCard.GetComponent<CardScript>().startingPos = card.transform.position;

        dupliCard.transform.DOMove(center.position, 0.5f).SetEase(Ease.OutCubic);
        dupliCard.transform.DOScale(1.5f,0.5f).SetEase(Ease.OutCubic);
        //dupliCard.transform.localScale *= 1.5f;

        //some whacky shit, hope this works
        slaveObject.transform.DOScale(1, 0.5f);
        slaveBool = true;
        //zoomInPost.weight = center.localScale.x;
        //zoomInPost.weight = 1;
    }

    public void ZoomOutOfCard(CardScript card)
    {
        canvas.GetComponent<GraphicRaycaster>().enabled = true;
        //dupliCard.transform.position = center.position;
        //dupliCard.transform.localScale *= 1.5f;
        card.transform.DOMove(card.startingPos, 0.5f).SetEase(Ease.OutCubic);
        card.transform.DOScale(1f, 0.5f).SetEase(Ease.OutCubic);

        Destroy(card.gameObject,0.5f);

        slaveObject.transform.DOScale(0, 0.5f).OnComplete(() => { slaveBool = false; });

        //zoomInPost.weight = 0;
    }
}

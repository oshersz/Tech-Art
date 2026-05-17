using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    public static CardManager sigleton { private set; get; }
    public GameObject deckCardPrefab;
    public GameObject deckCardEffect;
    public GameObject emptyUIPrefab;
    public Transform deckList;
    public Transform canvas;
    public CardBehaviour selectedCard;
    private void Awake()
    {
        sigleton = this;
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
        if (deckList.childCount < 8)
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
}

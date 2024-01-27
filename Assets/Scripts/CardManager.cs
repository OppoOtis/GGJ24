using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Card[] cardPool;

    List<Card> allTimeDeck;

    List<Card> deck;
    List<Card> hand;
    public Transform handVisual;
    public GameObject cardPrefab;

    public Transform[] heldCardsPos, highlightedCardsPos;
    public Transform selectedPos;

    Card selectedCard;

    public GameObject[] characters;

    // Start is called before the first frame update
    void Start()
    {
        BlackBoard.manager = this;
        allTimeDeck = new List<Card>();
        deck = new List<Card>();
        hand = new List<Card>();

        allTimeDeck.Add(new Card(cardType.damageHead));
        allTimeDeck.Add(new Card(cardType.damageHead));
        allTimeDeck.Add(new Card(cardType.damageHead));

        StartGame();
        StartTurn();

    }

    // Update is called once per frame
    void Update()
    {
        if (BlackBoard.playerTurn)
        {
            HoverCards();
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            BlackBoard.selectedCard = null;
            BlackBoard.otto.StopTalking();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DrawCard();
        }
    }

    public void StartGame()
    {
        foreach(Card crd in allTimeDeck)
        {
            deck.Add(crd.Clone());
        }
    }

    public void StartTurn()
    {
        BlackBoard.playerTurn = true;

        while(hand.Count < 3 && deck.Count > 0)
        {
            DrawCard();
        }
    }

    public void EndTurn()
    {
        BlackBoard.playerTurn = false;
    }

    void CreateCard()
    {

    }

    void HoverCards()
    {

    }

    public void DiscardCard(Card crd)
    {
        hand.Remove(crd);

        //then play a little dying animation or something
        Destroy(crd.visual);
        ReOrderCards();
    }

    public void SelectCard(Card card)
    {
        //when you click on a card, select it
        selectedCard = card;
    }

    void DrawCard()
    {
        //get a random card from the deck
        Card drawnCard = deck[Random.Range(0, deck.Count)];
        deck.Remove(drawnCard);

        //spawn a card and add it to your hand
        hand.Add(drawnCard);

        switch (hand.Count)
        {
            case 1:
                drawnCard.selectableCard.heldPos = heldCardsPos[0];
                drawnCard.selectableCard.highLightPos = highlightedCardsPos[0];
                break;
            case 2:
                drawnCard.selectableCard.heldPos = heldCardsPos[1];
                drawnCard.selectableCard.highLightPos = highlightedCardsPos[1];
                break;
            case 3:
                drawnCard.selectableCard.heldPos = heldCardsPos[2];
                drawnCard.selectableCard.highLightPos = highlightedCardsPos[2];
                break;
        }
        drawnCard.selectableCard.selectedPos = selectedPos;

        drawnCard.active = true;
    }

    public void ReOrderCards()
    {
        for(int i = 0; i < hand.Count; i++)
        {
            hand[i].selectableCard.heldPos = heldCardsPos[i];
            hand[i].selectableCard.highLightPos = highlightedCardsPos[i];
        }
    }
}

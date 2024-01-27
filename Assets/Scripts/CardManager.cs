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

    public Transform[] heldCardsPos, highlightedCardsPos;

    Card selectedCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BlackBoard.playerTurn)
        {
            HoverCards();
        }
    }

    void CreateCard()
    {

    }

    void HoverCards()
    {
        //optional
        foreach(Card crd in hand)
        {

        }
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
        switch (hand.Count)
        {
            case 0:
                hand.Add(drawnCard);
                drawnCard.selectableCard.heldPos = heldCardsPos[0];
                drawnCard.selectableCard.highLightPos = highlightedCardsPos[0];
                break;
            case 1:
                hand.Add(drawnCard);
                drawnCard.selectableCard.heldPos = heldCardsPos[1];
                drawnCard.selectableCard.highLightPos = highlightedCardsPos[1];
                break;
            case 2:
                hand.Add(drawnCard);
                drawnCard.selectableCard.heldPos = heldCardsPos[2];
                drawnCard.selectableCard.highLightPos = highlightedCardsPos[2];
                break;
        }
    }
}

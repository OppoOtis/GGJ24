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

    public CharacterManager[] characters;

    // Start is called before the first frame update
    void Start()
    {
        BlackBoard.manager = this;
        allTimeDeck = new List<Card>();
        deck = new List<Card>();
        hand = new List<Card>();

        allTimeDeck.Add(new Card(cardType.moveRight));
        allTimeDeck.Add(new Card(cardType.moveLeft));
        allTimeDeck.Add(new Card(cardType.damageArm));
        allTimeDeck.Add(new Card(cardType.damageLeg));
        allTimeDeck.Add(new Card(cardType.damageHead));
        allTimeDeck.Add(new Card(cardType.damageTorso));
        allTimeDeck.Add(new Card(cardType.healArm));
        allTimeDeck.Add(new Card(cardType.healLeg));
        allTimeDeck.Add(new Card(cardType.healHead));
        allTimeDeck.Add(new Card(cardType.healTorso));

        BlackBoard.eventCounter = 10;

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            BlackBoard.selectedCard = null;
            //BlackBoard.otto.StopTalking();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DrawCard();
        }
    }

    public void StartGame()
    {
        //BlackBoard.otto.LongTalk("Your Turn");
        deck = new List<Card>();
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

        BlackBoard.otto.AssignLookingTarget(Camera.main.transform); 
    }

    public void EndTurn()
    {
        BlackBoard.playerTurn = false;
        StartCoroutine(EndTurnCoroutine());
    }

    public void DiscardCard(Card crd)
    {
        hand.Remove(crd);
        ReOrderCards();
        crd.PlayDeathAnimation();
        //then play a little dying animation or something
        StartCoroutine(DiscardCardAnimation(crd));
    }

    public void DiscardAllCardsInHand()
    {
        foreach(Card crd in hand)
        {
            crd.PlayDeathAnimation();
        }

        while(hand.Count > 0)
        {
            hand.Remove(hand[0]);
        }
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

        if(deck.Count <= 0)
        {
            foreach (Card crd in allTimeDeck)
            {
                deck.Add(crd.Clone());
            }
        }

        //spawn a card and add it to your hand
        drawnCard.visual.transform.SetParent(handVisual);
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

    IEnumerator EndTurnCoroutine()
    {

        //do a number of things in sequence:
        //1: end your turn, do things that are supposed to happen at the end of your turn (currently nothing)
        //2: move the event counter up

        BlackBoard.playerTurn = false;
        BlackBoard.eventCounter--;
        BlackBoard.clock.RotateClock(BlackBoard.eventCounter);
        
        if(BlackBoard.eventCounter > 0)
        {
            string[] possibleLines = new string[] {
            "The timer is ticking...",
            "Just a few more turns...",
            "Look at the number go down..",
            "I'm waiting..."
            };

            int randomInt = Random.Range(0, possibleLines.Length);

            BlackBoard.otto.ShortTalk(possibleLines[randomInt]);
        }

        if(hand.Count > 0)
        {
            //empty the hand
            DiscardAllCardsInHand();
        }

        yield return new WaitForSeconds(5f);

        if(BlackBoard.eventCounter == 1)
        {
            //if cage is the next event, give a warning
        }

        if(BlackBoard.eventCounter == 0)
        {
            //2.1: trigger an event


            //2.2 queue a new event
        }


        //3: move to turn start
        string[] possibleLines2 = new string[] {
            "Your go",
            "Your turn",
            "Play some cards..",
            "Go ahead.."
            };

        int randomInt2 = Random.Range(0, possibleLines2.Length);

        BlackBoard.otto.ShortTalk(possibleLines2[randomInt2]);

        StartTurn();

        yield return null;
    }

    IEnumerator DiscardCardAnimation(Card crd)
    {
        while (crd.selectableCard.playCardParticles.gameObject.activeSelf)
        {
            //wait
            yield return new WaitForEndOfFrame();
        }
        Destroy(crd.visual);
        BlackBoard.selectedCard = null;
    }
}

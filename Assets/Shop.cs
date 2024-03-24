using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject[] cardSlots;
    List<GameObject> shopCards = new List<GameObject>();
    public Animator animator;
    bool bought;


    public void InitiateShop()
    {
        animator.SetBool("Open", true);

        shopCards.Clear();
        //Spawn randomly selected cards
        for(int i = 0; i < 3; i++)
        {
            Card temp = new Card((cardType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(cardType)).Length));
            shopCards.Add(temp.visual);
            temp.visual.transform.parent = cardSlots[i].transform;
            temp.visual.transform.localPosition = Vector3.zero;
        }

        bought = false;
    }

    public void EndShop()
    {
        animator.SetBool("Open", false);
    }

    public void FullyEndShop()
    {
        foreach(GameObject obj in shopCards)
        {
            Destroy(obj);
        }
        BlackBoard.events.EndEvent();
        this.gameObject.SetActive(false);
    }

    public void BuyCard(int cardNum)
    {
        if (bought)
            return;

        bought = true;
        Card boughtCard = shopCards[cardNum].GetComponent<SelectableCard>().myCard;
        BlackBoard.manager.allTimeDeck.Add(boughtCard);
        BlackBoard.manager.deck.Add(boughtCard.Clone());
        boughtCard.PlayDeathAnimation();
        EndShop();
    }
}

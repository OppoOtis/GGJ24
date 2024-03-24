using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCard : MonoBehaviour
{
    public Shop shop;
    public int cardNum;
    private void OnMouseDown()
    {
        shop.BuyCard(cardNum);
    }
}

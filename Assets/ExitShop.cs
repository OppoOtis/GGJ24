using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitShop : MonoBehaviour
{
    public Shop shop;
    private void OnMouseDown()
    {
        shop.EndShop();
    }
}

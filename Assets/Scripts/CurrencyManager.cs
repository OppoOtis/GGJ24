using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public bool spawnCoin;
    public bool deleteCoin;
    public int coinAmount;

    public GameObject coin;
    public Transform spawnPosition;

    public Queue<GameObject> coins;
    public int currency;

    public int coinsToAdd;

    private void Awake()
    {
        BlackBoard.currency = this;
        coins = new Queue<GameObject>();
    }

    private void Update()
    {
        if (spawnCoin)
        {
            spawnCoin = false;
            AddCurrency(coinAmount);
        }
        if (deleteCoin)
        {
            deleteCoin = false;
            RemoveCurrency(coinAmount);
        }
    }

    public void AddCurrency(int amount)
    {
        StartCoroutine(SpawnCoin(amount));
    }

    public void UpdateCoinCount()
    {
        currency = coins.Count;
    }

    public void RemoveCurrency(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if(coins.Count > 0)
            {
                Destroy(coins.Dequeue());
            }
        }
        UpdateCoinCount();
    }

    private IEnumerator SpawnCoin(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject currentCoin = Instantiate(coin, spawnPosition.position, Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            coins.Enqueue(currentCoin);
            yield return new WaitForSeconds(0.3f);
        }
        UpdateCoinCount();
    }
}

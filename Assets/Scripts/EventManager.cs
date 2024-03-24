using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OttoEvent
{
    Nothing,
    Shop,
    Slam,
    Cage
}

public class EventManager : MonoBehaviour
{
    public bool startEvent;
    public bool continueEvent;
    public string eventName;

    public bool eventIsGoing;
    public int currentEventTurn;
    public OttoEvent currentEvent = OttoEvent.Nothing;

    public OttoEvent nextEvent;


    [Header("Event Objects")]
    public GameObject cage;
    public GameObject shadow;
    public GameObject shop;

    private void Awake()
    {
        BlackBoard.events = this;
        nextEvent = OttoEvent.Shop;
    }

    private void Update()
    {
        if (startEvent)
        {
            StartEvent();
            startEvent = false;
        }
        if (continueEvent)
        {
            ContinueEvent();
            continueEvent = false;
        }
    }

    public void StartEvent()
    {
        if (eventIsGoing)
        {
            Debug.Log("There is currently already an event happening!");
        }
        else
        {
            currentEvent = nextEvent;
            eventIsGoing = true;
            Invoke("Run" + currentEvent, 0);
            Debug.Log("Event Started");
        }
    }

    public void ContinueEvent()
    {
        Invoke("Run" + currentEvent, 0);
    }

    public void RunNothing()
    {
        Debug.Log("There is currently no active event happening");
    }

    public int selectedCharacter;

    public void RunCage()
    {
        if (CheckEvent(2))
        {
            if (currentEventTurn == 1)
            {
                
            }
            else if(currentEventTurn == 2)
            {
                GameObject cageObj = Instantiate(cage, Vector3.zero, Quaternion.identity);
                cageObj.GetComponent<Cage>().SetCage(1);
            }
        }
        else
        {
            EndEvent();
        }
    }

    public void RunSlam()
    {
        if (CheckEvent(2))
        {
            if (currentEventTurn == 1)
            {
                //spawnShadow
                Debug.Log("spawned shadow");
            }
            else if (currentEventTurn == 2)
            {
                //slamCage
                Debug.Log("Slammed Hand");
            }
        }
        else
        {
            EndEvent();
        }
    }

    public void RunShop()
    {
        if (CheckEvent(1))
        {
            //show the shop to the player
            shop.SetActive(true);
            shop.GetComponent<Shop>().InitiateShop();
        }
    }

    public bool CheckEvent(int turnsForThisEvent)
    {
        bool eventCanContinue = false;
        currentEventTurn++;
        if(currentEventTurn <= turnsForThisEvent)
        {
            eventCanContinue = true;
        }
        return eventCanContinue;
    }

    public void EndEvent()
    {
        currentEvent = OttoEvent.Nothing;
        eventIsGoing = false;
        currentEventTurn = 0;
        Debug.Log("Event ended");

        BlackBoard.eventCounter = 3;
        BlackBoard.clock.RotateClock(BlackBoard.eventCounter);

        //3: move to turn start
        string[] possibleLines = new string[] {
            "Your go",
            "Your turn",
            "Play some cards..",
            "Go ahead.."
            };

        int randomInt2 = Random.Range(0, possibleLines.Length);

        BlackBoard.otto.ShortTalk(possibleLines[randomInt2]);

        BlackBoard.manager.StartTurn();
    }
}

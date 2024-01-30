using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public bool startEvent;
    public bool continueEvent;
    public string eventName;

    public bool eventIsGoing;
    public int currentEventTurn;
    public string currentEvent = "Nothing";


    [Header("Events")]
    public GameObject cage;

    private void Awake()
    {
        BlackBoard.events = this;
    }

    private void Update()
    {
        if (startEvent)
        {
            StartEvent(eventName);
            startEvent = false;
        }
        if (continueEvent)
        {
            ContinueEvent();
            continueEvent = false;
        }
    }

    public void StartEvent(string eventName)
    {
        if (eventIsGoing)
        {
            Debug.Log("There is currently already an event happening!");
        }
        else
        {
            currentEvent = eventName;
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
        currentEvent = "Nothing";
        eventIsGoing = false;
        currentEventTurn = 0;
        Debug.Log("Event ended");
    }
}

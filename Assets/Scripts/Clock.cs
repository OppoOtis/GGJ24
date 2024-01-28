using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public bool rotateClock;
    public bool eventClock;

    public bool front;
    public GameObject rotator;
    public TextMeshPro frontText;
    public TextMeshPro backText;
    public TextMeshPro frontEventText;
    public TextMeshPro backEventText;

    public Transform to1;
    public Transform to2;

    public float speed = 0.01f;

    private void Awake()
    {
        BlackBoard.clock = this;
    }

    private void Update()
    {
        if (rotateClock)
        {
            RotateClock(1);
            rotateClock = false;
        }

        if (eventClock)
        {
            EventClock();
            eventClock = false;
        }

        if (front)
        {
            rotator.transform.rotation = Quaternion.Slerp(rotator.transform.rotation, to1.rotation, speed * Time.deltaTime);
        }
        else
        {
            rotator.transform.rotation = Quaternion.Slerp(rotator.transform.rotation, to2.rotation, speed * Time.deltaTime);
        }
        

    }

    public void RotateClock(int number)
    {
        if (front)
        {
            front = false;
            frontText.enabled = true;
            frontEventText.enabled = false;
            frontText.text = number.ToString();
        }
        else
        {
            front = true;
            backText.enabled = true;
            backEventText.enabled = false;
            backText.text = number.ToString();
        }
    }

    public void EventClock()
    {
        if (front)
        {
            front = false;
            frontText.enabled = false;
            frontEventText.enabled = true;
        }
        else
        {
            front = true;
            backText.enabled = false;
            backEventText.enabled = true;
        }
    }
}

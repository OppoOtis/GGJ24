using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public string introLine;
    public void QueueThisIntro()
    {
        BlackBoard.otto.LongTalk(introLine);
        BlackBoard.otto.AssignLookingTarget(transform);
        GetComponent<Animator>().SetTrigger("Intro");
    }

    public void StopAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }
}

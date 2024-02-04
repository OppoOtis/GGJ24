using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public Intro[] introAnimations;

    public string[] sentencesAfterAnimation;
    int index;
    private void OnMouseDown()
    {
        //play the next animation, and say the next line
        if (index < introAnimations.Length)
            introAnimations[index].QueueThisIntro();

        index++;
        if (index >= introAnimations.Length && index - introAnimations.Length > 0)
        {
            BlackBoard.otto.LongTalk(sentencesAfterAnimation[index - introAnimations.Length - 1]);
            if (index - introAnimations.Length >= sentencesAfterAnimation.Length)
            {
                BlackBoard.manager.StartTurn();
                gameObject.SetActive(false);
            }
        }
    }
}

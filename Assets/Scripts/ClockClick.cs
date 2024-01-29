using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockClick : MonoBehaviour
{
    Animator animator;
    Clock clock;

    private void Awake()
    {
        clock = GetComponent<Clock>();
        animator = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        if (!BlackBoard.playerTurn)
            return;
        //end your turn
        BlackBoard.manager.EndTurn();
        animator.SetBool("Rotating", false);
    }

    private void OnMouseEnter()
    {
        if (!BlackBoard.playerTurn)
            return;

        animator.SetBool("Rotating", true);
        BlackBoard.otto.AssignLookingTarget(transform);
    }

    private void OnMouseExit()
    {
        animator.SetBool("Rotating", false);
    }
}

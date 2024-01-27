using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        animator.SetBool("Select", true);
        BlackBoard.otto.AssignLookingTarget(transform);
    }

    private void OnMouseExit()
    {
        animator.SetBool("Select", false);
    }

    private void OnMouseDown()
    {
        if(BlackBoard.selectedCard != null)
        {
            //do something
            BlackBoard.selectedCard.UseCard(transform.GetComponent<CharacterManager>());
        }
    }
}

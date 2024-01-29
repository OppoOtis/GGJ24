using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public GameObject statsUI;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        if (BlackBoard.selectedCard != null)
        {
            animator.SetBool("Select", true);
            BlackBoard.otto.AssignLookingTarget(transform);

            statsUI.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        animator.SetBool("Select", false);
        statsUI.SetActive(false);
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

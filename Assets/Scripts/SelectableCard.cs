using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableCard : MonoBehaviour
{
    public Card myCard;
    public Material normalMat;
    public Material highLightMat;

    public MeshRenderer meshRenderer;

    public Transform heldPos, highLightPos, selectedPos;
    bool hightLighted;

    public ParticleSystem playCardParticles;

    private void Update()
    {
        if (myCard.active)
        {
            if (BlackBoard.selectedCard == myCard)
            {
                transform.position = Vector3.Lerp(transform.position, selectedPos.position, 5 * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, selectedPos.rotation, 5 * Time.deltaTime);
            }

            else if (hightLighted)
            {
                transform.position = Vector3.Lerp(transform.position, highLightPos.position, 10 * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, highLightPos.rotation, 10 * Time.deltaTime);
            }

            else
            {
                transform.position = Vector3.Lerp(transform.position, heldPos.position, 10 * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, heldPos.rotation, 10 * Time.deltaTime);
            }
        }
    }

    public void SetCard(Card crd)
    {
        myCard = crd;
    }

    private void OnMouseDown()
    {
        if (!BlackBoard.playerTurn)
            return;

        //select this card
        BlackBoard.selectedCard = myCard;

        //trigger a line
        BlackBoard.otto.StopTalking();
        BlackBoard.otto.LongTalk("Pick a FRIEND..");
    }

    private void OnMouseOver()
    {
        if (!BlackBoard.playerTurn)
            return;

        if (BlackBoard.selectedCard == myCard)
        {
            DeHighLightCard();
            return;
        }

        HighLightCard();
    }

    private void OnMouseExit()
    {
        DeHighLightCard();
    }

    void HighLightCard()
    {
        meshRenderer.material = highLightMat;

        //move the card up
        hightLighted = true;
        BlackBoard.otto.AssignLookingTarget(transform);
    }

    void DeHighLightCard()
    {
        meshRenderer.material = normalMat;
        hightLighted = false;
    }

    public void PlayPlayParticles()
    {
        Debug.Log("Play Particles");
        playCardParticles.Play();
    }
}

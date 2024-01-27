using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableCard : MonoBehaviour
{
    Card myCard;
    public Material normalMat;
    public Material highLightMat;

    MeshRenderer meshRenderer;

    public Transform heldPos, highLightPos, selectedPos;
    bool hightLighted;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        myCard = new Card(transform.gameObject);
    }

    private void Update()
    {
        if(BlackBoard.selectedCard == myCard)
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

    public void SetCard(Card crd)
    {
        myCard = crd;
    }

    private void OnMouseDown()
    {
        //select this card
        BlackBoard.selectedCard = myCard; 
        //move the card to the left
    }

    private void OnMouseOver()
    {
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
}

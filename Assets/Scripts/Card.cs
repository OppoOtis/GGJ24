using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cardType
{
    healHead,
    healArm,
    healLeg,
    healBody,
    damageHead,
    damageArm,
    damageLeg,
    damageBody,
    move
}
public class Card
{
    cardType myType;
    public GameObject visual;
    public SelectableCard selectableCard;

    public bool active;
    public Card(cardType _type)
    {
        //visual = _visual;
        myType = _type;

        switch (myType)
        {
            case cardType.damageHead:
                visual = Object.Instantiate(Resources.Load<GameObject>("DamageHead"));
                break;
        }

        selectableCard = visual.GetComponent<SelectableCard>();
        selectableCard.myCard = this;
        active = false;
    }

    public Card Clone()
    {
        GameObject duplicateObject = Object.Instantiate(visual, visual.transform.parent);

        // Optionally, you can set the position, rotation, and scale of the duplicateObject
        duplicateObject.transform.position = visual.transform.position;
        duplicateObject.transform.rotation = visual.transform.rotation; // No rotation
        duplicateObject.transform.localScale = visual.transform.localScale; // Default scale

        return new Card(myType);
    }

    public void UseCard(CharacterManager target)
    {
        Debug.Log(myType);
        switch (myType)
        {
            case cardType.healArm:
                //heal the selected character
                target.HealArm(1);
                break;
            case cardType.damageHead:
                target.DamageHead(1);
                break;
        }

        BlackBoard.selectedCard = null;
        //if you use it, discard it
        BlackBoard.manager.DiscardCard(this);
    }
}

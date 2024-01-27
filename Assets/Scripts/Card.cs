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
            case cardType.damageArm:
                visual = Object.Instantiate(Resources.Load<GameObject>("DamageArms"));
                break;
            case cardType.damageLeg:
                visual = Object.Instantiate(Resources.Load<GameObject>("DamageLegs"));
                break;
        }

        selectableCard = visual.GetComponent<SelectableCard>();
        selectableCard.myCard = this;
        active = false;
    }

    public Card Clone()
    {
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
            case cardType.damageLeg:
                target.DamageLeg(1);
                break;
            case cardType.damageArm:
                target.DamageArm(1);
                break;
        }

        BlackBoard.selectedCard = null;
        //if you use it, discard it
        BlackBoard.manager.DiscardCard(this);
    }
}

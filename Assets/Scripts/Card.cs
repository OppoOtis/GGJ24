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
    moveLeft,
    moveRight
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
            case cardType.moveLeft:
                visual = Object.Instantiate(Resources.Load<GameObject>("MoveToLeft"));
                break;
            case cardType.moveRight:
                visual = Object.Instantiate(Resources.Load<GameObject>("MoveToRight"));
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
        if (target.dead)
        {
            BlackBoard.otto.ShortTalk("THAT ONE IS USELESS NOW");
            return;
        }
        Debug.Log(myType);
        switch (myType)
        {
            case cardType.healArm:
                //heal the selected character
                target.HealArm(1);
                break;
            case cardType.damageHead:
                target.DamageHead(1);
                BlackBoard.otto.StartLaughing();
                break;
            case cardType.damageLeg:
                target.DamageLeg(1);
                BlackBoard.otto.StartLaughing();
                break;
            case cardType.damageArm:
                target.DamageArm(1);
                BlackBoard.otto.StartLaughing();
                break;
            case cardType.moveLeft:
                if(target == BlackBoard.manager.characters[0])
                {
                    return;
                }

                else
                {
                    //switch the character with the character on the left
                    if(target == BlackBoard.manager.characters[1])
                    {
                        Vector3 leftLoc = BlackBoard.manager.characters[0].transform.parent.position;
                        Vector3 rightLoc = BlackBoard.manager.characters[1].transform.parent.position;

                        CharacterManager temp = BlackBoard.manager.characters[0];
                        BlackBoard.manager.characters[0] = BlackBoard.manager.characters[1];
                        BlackBoard.manager.characters[1] = temp;

                        BlackBoard.manager.characters[0].MoveToLocation(leftLoc);
                        BlackBoard.manager.characters[1].MoveToLocation(rightLoc);
                    }

                    else if (target == BlackBoard.manager.characters[2])
                    {
                        Vector3 leftLoc = BlackBoard.manager.characters[1].transform.parent.position;
                        Vector3 rightLoc = BlackBoard.manager.characters[2].transform.parent.position;

                        CharacterManager temp = BlackBoard.manager.characters[1];
                        BlackBoard.manager.characters[1] = BlackBoard.manager.characters[2];
                        BlackBoard.manager.characters[2] = temp;

                        BlackBoard.manager.characters[1].MoveToLocation(leftLoc);
                        BlackBoard.manager.characters[2].MoveToLocation(rightLoc);
                    }

                    else if (target == BlackBoard.manager.characters[3])
                    {
                        Vector3 leftLoc = BlackBoard.manager.characters[2].transform.parent.position;
                        Vector3 rightLoc = BlackBoard.manager.characters[3].transform.parent.position;

                        CharacterManager temp = BlackBoard.manager.characters[2];
                        BlackBoard.manager.characters[2] = BlackBoard.manager.characters[3];
                        BlackBoard.manager.characters[3] = temp;

                        BlackBoard.manager.characters[2].MoveToLocation(leftLoc);
                        BlackBoard.manager.characters[3].MoveToLocation(rightLoc);
                    }
                }
                break;
            case cardType.moveRight:
                if (target == BlackBoard.manager.characters[3])
                {
                    return;
                }

                else
                {
                    //switch the character with the character on the right
                    if (target == BlackBoard.manager.characters[0])
                    {
                        Vector3 leftLoc = BlackBoard.manager.characters[0].transform.parent.position;
                        Vector3 rightLoc = BlackBoard.manager.characters[1].transform.parent.position;

                        CharacterManager temp = BlackBoard.manager.characters[0];
                        BlackBoard.manager.characters[0] = BlackBoard.manager.characters[1];
                        BlackBoard.manager.characters[1] = temp;

                        BlackBoard.manager.characters[0].MoveToLocation(leftLoc);
                        BlackBoard.manager.characters[1].MoveToLocation(rightLoc);
                    }

                    else if (target == BlackBoard.manager.characters[1])
                    {
                        Vector3 leftLoc = BlackBoard.manager.characters[1].transform.parent.position;
                        Vector3 rightLoc = BlackBoard.manager.characters[2].transform.parent.position;

                        CharacterManager temp = BlackBoard.manager.characters[1];
                        BlackBoard.manager.characters[1] = BlackBoard.manager.characters[2];
                        BlackBoard.manager.characters[2] = temp;

                        BlackBoard.manager.characters[1].MoveToLocation(leftLoc);
                        BlackBoard.manager.characters[2].MoveToLocation(rightLoc);
                    }

                    else if (target == BlackBoard.manager.characters[2])
                    {
                        Vector3 leftLoc = BlackBoard.manager.characters[2].transform.parent.position;
                        Vector3 rightLoc = BlackBoard.manager.characters[3].transform.parent.position;

                        CharacterManager temp = BlackBoard.manager.characters[2];
                        BlackBoard.manager.characters[2] = BlackBoard.manager.characters[3];
                        BlackBoard.manager.characters[3] = temp;

                        BlackBoard.manager.characters[2].MoveToLocation(leftLoc);
                        BlackBoard.manager.characters[3].MoveToLocation(rightLoc);
                    }
                }
                break;
        }

        if (BlackBoard.otto.longTalkBool)
        {
            BlackBoard.otto.StopTalking();
        }
        BlackBoard.selectedCard = null;
        //if you use it, discard it
        BlackBoard.manager.DiscardCard(this);
    }
}

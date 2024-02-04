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
                    for (int i = 1; i < BlackBoard.manager.characters.Length; i++)
                    {
                        if(target == BlackBoard.manager.characters[i])
                        {
                            MoveCharacters(i, i-1);
                            break;
                        }
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
                    for (int i = 0; i < BlackBoard.manager.characters.Length-1; i++)
                    {
                        if (target == BlackBoard.manager.characters[i])
                        {
                            MoveCharacters(i, i + 1);
                            break;
                        }
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

    public void MoveCharacters(int targetChar, int otherChar)
    {

        Vector3 targetLocation = BlackBoard.manager.characters[targetChar].transform.parent.position;
        Vector3 otherLocation = BlackBoard.manager.characters[otherChar].transform.parent.position;

        CharacterManager temp = BlackBoard.manager.characters[targetChar];
        BlackBoard.manager.characters[targetChar] = BlackBoard.manager.characters[otherChar];
        BlackBoard.manager.characters[otherChar] = temp;

        BlackBoard.manager.characters[targetChar].MoveToLocation(targetLocation);
        BlackBoard.manager.characters[otherChar].MoveToLocation(otherLocation);
    }
}

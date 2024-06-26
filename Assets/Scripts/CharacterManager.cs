using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public bool damageTorso = false;
    public bool damageHead = false;
    public bool damageLeg = false;
    public bool damageArm = false;

    public bool healTorso = false;
    public bool healHead = false;
    public bool healLeg = false;
    public bool healArm = false;


    [Header("Damage Settings")]
    public int torsoDamage;
    public int headDamage;
    public int legDamage;
    public bool leftArm;
    public bool rightArm;
    public bool dead;

    [Header("Visual Settings")]

    public bool generateVisual = false;

    public bool standing;

    public int faceValue;
    public bool otherFace;

    public GameObject[] standingTorsoVisuals;
    public GameObject[] standingLimbsVisuals;
    public GameObject[] standingHeadVisuals;
    public GameObject[] crawlingTorsoVisuals;
    public GameObject[] crawlingHeadVisuals;

    public GameObject[] standingFaceVisual;
    public GameObject[] crawlingFaceVisual;

    [Header("UI Settings")]
    public Color healthyColor;
    public Color damagedColor;
    public SpriteRenderer chHead;
    public SpriteRenderer chHeadC;
    public SpriteRenderer chTorso;
    public SpriteRenderer chTorsoC;
    public SpriteRenderer chLL;
    public SpriteRenderer chRL;
    public SpriteRenderer chLA;
    public SpriteRenderer chRA;


    private void Start()
    {
        UpdateVisual();
        UpdateFace();
    }

    private void Update()
    { 
        //damage
        if (damageHead)
        {
            DamageHead(1);
            damageHead = false;
        }
        if (damageTorso)
        {
            DamageTorso(1);
            damageTorso = false;
        }
        if (damageLeg)
        {
            DamageLeg(1);
            damageLeg = false;
        }
        if (damageArm)
        {
            DamageArm(1);
            damageArm = false;
        }
        //heal
        if (healHead)
        {
            HealHead(1);
            healHead = false;
        }
        if (healTorso)
        {
            HealTorso(1);
            healTorso = false;
        }
        if (healLeg)
        {
            HealLeg(1);
            healLeg = false;
        }
        if (healArm)
        {
            HealArm(1);
            healArm = false;
        }

        if(generateVisual ||  damageHead || damageLeg || damageArm || damageTorso || healArm || healHead || healLeg || healTorso)
        {
            UpdateVisual();
            UpdateFace();
        }

        if (generateVisual)
        {
            generateVisual = false;
        }
    }

    public void DamageHead(int amount)
    {
        headDamage += amount;
        if (headDamage >= 1)
        {
            dead = true;
        }
        else
        {
            headDamage += amount;
            if(headDamage > 7)
            {
                headDamage = 8;
            }
        }
        BlackBoard.funny = true;
        UpdateVisual();
    }
    public void DamageTorso(int amount)
    {
        torsoDamage += amount;
        if (torsoDamage >= 2)
        {
            dead = true;
        }
        else
        {

            if (torsoDamage > 7)
            {
                torsoDamage = 8;
                legDamage = 3;
            }
        }
        BlackBoard.funny = true;
        UpdateVisual();
    }
    public void DamageLeg(int amount)
    {
        if(!dead)
        {
            legDamage += amount;
            if (legDamage > 2)
            {
                legDamage = 2;
            }
        }
        BlackBoard.funny = true;
        UpdateVisual();
    }
    public void DamageArm(int amount)
    {
        if(amount == 1)
        {
            if (leftArm)
            {
                leftArm = false;
            }
            else if(rightArm)
            {
                rightArm = false;
            }
        }
        if(amount == 2)
        {
            leftArm = false;
            rightArm = false;
        }
        BlackBoard.funny = true;
        UpdateVisual();
    }

    public void HealHead(int amount)
    {
        headDamage -= amount;
        if (headDamage < 0)
        {
            headDamage = 0;
        }
        UpdateVisual();
    }
    public void HealTorso(int amount)
    {
        torsoDamage -= amount;
        if(torsoDamage < 0)
        {
            torsoDamage = 0;
        }
        if(legDamage > 2)
        {
            legDamage = 2;
        }
        UpdateVisual();
    }
    public void HealLeg(int amount)
    {
        if (torsoDamage < 8)
        {
            legDamage -= amount;
            if (legDamage < 0)
            {
                legDamage = 0;
            }
        }
        UpdateVisual();
    }
    public void HealArm(int amount)
    {
        if (amount == 1)
        {
            if (leftArm == false)
            {
                leftArm = true;
            }
            else if (rightArm == false)
            {
                rightArm = true;
            }
        }
        if (amount == 2)
        {
            leftArm = true;
            rightArm = true;
        }
        UpdateVisual();
    }

    public void MoveToLocation(Vector3 _moveTo)
    {
        StartCoroutine(MoveToLocationCoroutine(_moveTo));
    }

    public IEnumerator MoveToLocationCoroutine(Vector3 _moveTo)
    { 
        while(transform.parent.position != _moveTo)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, _moveTo, 10 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public void UpdateVisual()
    {
        //turn everything off
        foreach  (GameObject obj in standingTorsoVisuals)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in crawlingTorsoVisuals)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in standingHeadVisuals)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in crawlingHeadVisuals)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in standingLimbsVisuals)
        {
            obj.SetActive(false);
        }

        if(legDamage > 0 || dead)
        {
            standing = false;
        }
        else
        {
            standing = true;
        }

        //find and turn on correct model
        if (standing)
        {
            if (leftArm)
            {
                standingLimbsVisuals[0].SetActive(true);
            }
            else
            {
                standingLimbsVisuals[1].SetActive(true);
            }
            if (rightArm)
            {
                standingLimbsVisuals[2].SetActive(true);
            }
            else
            {
                standingLimbsVisuals[3].SetActive(true);
            }
            if (headDamage < 1)
            {
                standingHeadVisuals[headDamage].SetActive(true);
            }
            else
            {
                standingHeadVisuals[7].SetActive(true);
            }
            if(torsoDamage < 2) standingTorsoVisuals[torsoDamage * 4].SetActive(true);
        }
        else
        {
            if (leftArm)
            {
                crawlingTorsoVisuals[4].SetActive(true);
            }
            else
            {
                crawlingTorsoVisuals[5].SetActive(true);
            }
            if (rightArm)
            {
                crawlingTorsoVisuals[6].SetActive(true);
            }
            else
            {
                crawlingTorsoVisuals[7].SetActive(true);
            }

            if (headDamage < 1)
            {
                crawlingHeadVisuals[headDamage].SetActive(true);
            }
            else
            {
                crawlingHeadVisuals[7].SetActive(true);
            }
            crawlingTorsoVisuals[legDamage].SetActive(true);
        }

        if(headDamage >= 1)
        {
            chHeadC.color = damagedColor;
            chHead.color = damagedColor;
        }
        else if(headDamage == 7)
        {
            chHeadC.color = healthyColor;
            chHead.color = damagedColor;
        }
        else
        {
            chHead.color = healthyColor;
            chHeadC.color = healthyColor;
        }

        if (torsoDamage >= 2)
        {
            chTorsoC.color = damagedColor;
            chTorso.color = damagedColor;
        }
        else if (torsoDamage == 7)
        {
            chTorsoC.color = healthyColor;
            chTorso.color = damagedColor;
        }
        else
        {
            chTorsoC.color = healthyColor;
            chTorso.color = healthyColor;
        }

        if (legDamage >= 2)
        {
            chLL.color = damagedColor;
            chRL.color = damagedColor;
        }
        else if(legDamage == 1)
        {
            chLL.color = damagedColor;
            chRL.color = healthyColor;
        }
        else if(legDamage == 0)
        {
            chLL.color = healthyColor;
            chRL.color = healthyColor;
        }

        if (leftArm)
        {
            chLA.color = healthyColor;
        }
        else
        {
            chLA.color = damagedColor;
        }
        if (rightArm)
        {
            chRA.color = healthyColor;
        }
        else
        {
            chRA.color = damagedColor;
        }
        UpdateFace();
    }

    public void UpdateFace()
    {
        if (dead)
        {
            faceValue = 6;
        }
        else
        {
            faceValue = Random.Range(0,6);
        }

        foreach (GameObject obj in standingFaceVisual)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in crawlingFaceVisual)
        {
            obj.SetActive(false);
        }

        int addToFaceValue = 0;
        if (otherFace) addToFaceValue = 7;

        if (standing)
        {
            standingFaceVisual[faceValue+addToFaceValue].SetActive(true);
        }
        else
        {
            crawlingFaceVisual[faceValue+addToFaceValue].SetActive(true);
        }
    }
}

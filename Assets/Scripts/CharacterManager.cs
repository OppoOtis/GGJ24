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

    private void Start()
    {
        UpdateVisual();
        UpdateFace();
    }

    private void Update()
    {
        if (generateVisual)
        {
            UpdateVisual();
            UpdateFace();
            generateVisual = false;
        }
        //damage
        if (damageHead)
        {
            DamageHead(1);
            damageHead = false;
            UpdateVisual();
            UpdateFace();
        }
        if (damageTorso)
        {
            DamageTorso(1);
            damageTorso = false;
            UpdateVisual();
            UpdateFace();
        }
        if (damageLeg)
        {
            DamageLeg(1);
            damageLeg = false;
            UpdateVisual();
            UpdateFace();
        }
        if (damageArm)
        {
            DamageArm(1);
            damageArm = false;
            UpdateVisual();
            UpdateFace();
        }
        //heal
        if (healHead)
        {
            HealHead(1);
            healHead = false;
            UpdateVisual();
            UpdateFace();
        }
        if (healTorso)
        {
            HealTorso(1);
            healTorso = false;
            UpdateVisual();
            UpdateFace();
        }
        if (healLeg)
        {
            HealLeg(1);
            healLeg = false;
            UpdateVisual();
            UpdateFace();
        }
        if (healArm)
        {
            HealArm(1);
            healArm = false;
            UpdateVisual();
            UpdateFace();
        }
    }

    public void DamageHead(int amount)
    {
        if(headDamage >= 8)
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
    }
    public void DamageTorso(int amount)
    {
        if (torsoDamage >= 8)
        {
            dead = true;
        }
        else
        {
            torsoDamage += amount;
            if (torsoDamage > 7)
            {
                torsoDamage = 8;
                legDamage = 3;
            }
        }
    }
    public void DamageLeg(int amount)
    {
        if(torsoDamage < 8)
        {
            legDamage += amount;
            if (legDamage > 2)
            {
                legDamage = 2;
            }
        }
    }
    public void DamageArm(int amount)
    {
        if(amount == 1)
        {
            if (leftArm)
            {
                leftArm = false;
                return;
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
    }

    public void HealHead(int amount)
    {
        headDamage -= amount;
        if (headDamage < 0)
        {
            headDamage = 0;
        }
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
    }
    public void HealArm(int amount)
    {
        if (amount == 1)
        {
            if (leftArm == false)
            {
                leftArm = true;
                return;
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
            if (headDamage < 8)
            {
                standingHeadVisuals[headDamage].SetActive(true);
            }
            else
            {
                standingHeadVisuals[7].SetActive(true);
            }
            if(torsoDamage < 8) standingTorsoVisuals[torsoDamage].SetActive(true);
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

            if (headDamage < 8)
            {
                crawlingHeadVisuals[headDamage].SetActive(true);
            }
            else
            {
                crawlingHeadVisuals[7].SetActive(true);
            }
            crawlingTorsoVisuals[legDamage].SetActive(true);
        }
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
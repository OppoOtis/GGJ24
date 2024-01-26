using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public bool generateVisual = false;

    public int torsoDamage;
    public int headDamage;
    public int legDamage;
    public bool leftArm;
    public bool rightArm;
    public bool standing;

    public GameObject[] standingTorsoVisuals;
    public GameObject[] standingLimbsVisuals;
    public GameObject[] standingHeadVisuals;
    public GameObject[] crawlingTorsoVisuals;
    public GameObject[] crawlingHeadVisuals;

    private void Update()
    {
        if (generateVisual)
        {
            UpdateVisual();
            generateVisual = false;
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
            standingHeadVisuals[headDamage].SetActive(true);
            standingTorsoVisuals[torsoDamage].SetActive(true);
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
            crawlingHeadVisuals[headDamage].SetActive(true);
            crawlingTorsoVisuals[legDamage].SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public Material cageMat;
    public Color startColor;
    public Color endColor;
    public Animator cageAnimator;

    public bool setCage;
    public bool removeCage;
    public bool deleteCage;

    public GameObject[] spawnPos;

    private void Awake()
    {
        cageAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (setCage)
        {
            setCage = false;
            SetCage(0);
        }

        if (removeCage)
        {
            removeCage = false;
            RemoveCage();
        }

        if (deleteCage)
        {
            deleteCage = false;
            DeleteCage();
        }
    }

    public void SetCage(int pos)
    {
        transform.position = spawnPos[pos].transform.position;
        cageMat.color = startColor;
        cageAnimator.SetTrigger("Slam");
    }

    public void RemoveCage()
    {
        cageAnimator.SetTrigger("Remove");
    }

    public void DeleteCage()
    {
        Destroy(this.gameObject);
    }
}

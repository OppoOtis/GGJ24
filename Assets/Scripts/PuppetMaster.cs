using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetMaster : MonoBehaviour
{

    [Header("Mouth")]
    public Transform fullMouth;
    public Transform LowerJamJoint;
    public Vector3 RaisedMouthPos, LoweredMouthPos;
    public float raiseSpeed, lowerSpeed;

    // Start is called before the first frame update
    void Start()
    {

        RaisedMouthPos = transform.position + RaisedMouthPos;
        LoweredMouthPos = transform.position + LoweredMouthPos;

        LowerTheMouth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LowerTheMouth()
    {
        StartCoroutine(LowerMouth());
    }

    public IEnumerator LowerMouth()
    {
        bool done = false;

        while (!done)
        {
            fullMouth.transform.position += (LoweredMouthPos - fullMouth.transform.position).normalized * Time.deltaTime * lowerSpeed;

            if (Vector3.Distance(fullMouth.transform.position, LoweredMouthPos) < 0.1f)
            {
                done = true;
            }

            yield return new WaitForEndOfFrame();

        }
    }
}

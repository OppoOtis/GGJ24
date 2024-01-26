using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetMaster : MonoBehaviour
{
    public Transform rightEye, leftEye;
    public Transform rightLookAt, leftLookAt;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rightEye.transform.LookAt(rightLookAt);
        leftEye.transform.LookAt(leftLookAt);
    }
}

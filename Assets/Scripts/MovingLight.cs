using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
    Rigidbody rb;

    public float addForceWait;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if(timer > addForceWait)
        {
            if(rb.velocity.x > 0)
            {
                //add force to the right
                rb.AddForce(new Vector3(0.2f, 0, 0), ForceMode.Impulse);
            }

            else
            {
                rb.AddForce(new Vector3(-0.2f, 0, 0), ForceMode.Impulse);
            }
            //add force
            timer = 0;
        }
    }
}

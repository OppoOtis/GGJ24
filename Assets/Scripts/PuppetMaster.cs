using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuppetMaster : MonoBehaviour
{
    public Transform rightEye, leftEye;
    public Transform rightLookAt, leftLookAt;

    public TextMeshProUGUI puppetMasterText;
    public GameObject textBottomArrow;
    public float talkSpeed;

    public Animator puppetMasterAnimator;
    public Animator MouthAnimator;

    // Start is called before the first frame update
    void Start()
    {
        BlackBoard.otto = this; 
        Talk("HA HA HA! LOOK AT IT!");
        LowerEyes();
        LowerMouth();
    }

    // Update is called once per frame
    void Update()
    {
        rightEye.transform.LookAt(rightLookAt);
        leftEye.transform.LookAt(leftLookAt);
    }

    public void LowerEyes()
    {
        puppetMasterAnimator.SetTrigger("Lower");
    }

    public void LowerMouth()
    {
        MouthAnimator.SetTrigger("Lower");
    }

    public void RaiseMouth()
    {
        MouthAnimator.SetTrigger("Raise");
    }

    public void StartLaughing() 
    {
        MouthAnimator.SetBool("Laugh", true);
    }

    public void StopLaughing()
    {
        MouthAnimator.SetBool("Laugh", false);
    }

    public void StartFrowning()
    {
        MouthAnimator.SetBool("Frown", true);
    }

    public void StopFrowning()
    {
        MouthAnimator.SetBool("Frown", false);
    }

    public void Talk(string whatToSay)
    {
        puppetMasterText.text = "";
        textBottomArrow.SetActive(true);
        StartCoroutine(TypeTalking(whatToSay));
    }

    public void AssignLookingTarget(Transform target)
    {
        rightLookAt = target;
        leftLookAt = target;
    }

    IEnumerator TypeTalking(string whatToSay)
    {
        char[] textInCharacters = whatToSay.ToCharArray();

        int currentAmount = 0;
        while(currentAmount < whatToSay.Length)
        {
            yield return new WaitForSeconds(talkSpeed);

            puppetMasterText.text = puppetMasterText.text + textInCharacters[currentAmount];
            currentAmount++;
        }

        yield return new WaitForSeconds(3f);
        puppetMasterText.text = "";
        textBottomArrow.SetActive(false);
    }
}

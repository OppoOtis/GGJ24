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

    float talkLength;
    bool longTalkBool;

    // Start is called before the first frame update
    void Start()
    {
        BlackBoard.otto = this;
        //ShortTalk("HA HA HA! LOOK AT IT!");

        LongTalk("Choose a card");
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
        MouthAnimator.SetTrigger("Laugh");
        StopTalking();
        ShortTalk("HAHAHAHA");
    }

    public void StartFrowning()
    {
        MouthAnimator.SetBool("Frown", true);
    }

    public void StopFrowning()
    {
        MouthAnimator.SetBool("Frown", false);
    }

    public void ShortTalk(string whatToSay)
    {
        talkLength = 3f;
        puppetMasterText.text = "";
        textBottomArrow.SetActive(true);
        StartCoroutine(TypeTalking(whatToSay));
    }

    public void LongTalk(string whatToSay)
    {
        longTalkBool = true;
        talkLength = 0;
        puppetMasterText.text = "";
        textBottomArrow.SetActive(true);
        StartCoroutine(TypeTalking(whatToSay));
    }

    public void StopTalking()
    {
        StopAllCoroutines();
        puppetMasterText.text = "";
        textBottomArrow.SetActive(false);
        longTalkBool = false;
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

            if (talkLength > 0)
            {
                yield return new WaitForSeconds(talkLength);
            }

            else
            {
                while (longTalkBool)
                {
                    yield return new WaitForEndOfFrame();
                }
            }

        puppetMasterText.text = "";
        textBottomArrow.SetActive(false);
    }
}

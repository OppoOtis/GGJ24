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
    public bool longTalkBool;

    // Start is called before the first frame update
    void Start()
    {
        BlackBoard.otto = this;
        //ShortTalk("HA HA HA! LOOK AT IT!");

        //LongTalk("Choose a card");
        LowerEyes();
        LowerMouth();
        LongTalk("Welcome");
    }

    // Update is called once per frame
    void Update()
    {
        rightEye.transform.LookAt(rightLookAt);
        leftEye.transform.LookAt(leftLookAt);
    }

    private void OnMouseDown()
    {
        string[] possibleLines = new string[] {
            "DONT TOUCH MEEE",
            "KEEP YOUR HANDS WHERE THEY BELONG",
            "STOP IT",
            "I WILL TAKE YOUR TEETH"
        };

        int randomInt = Random.Range(0, possibleLines.Length);

        ShortTalk(possibleLines[randomInt]);
        rightLookAt = Camera.main.transform;
        leftLookAt = Camera.main.transform;
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
        ShortTalk("HAHAHAHA");
        BlackBoard.currency.AddCurrency(1);
    }
    
    public void LaughAtDead() 
    {
        MouthAnimator.SetTrigger("Laugh");
        ShortTalk("HAHAHAHA ITS DEAD!");
        BlackBoard.currency.AddCurrency(1);
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
        StopTalking();
        talkLength = 3f;
        puppetMasterText.text = "";
        textBottomArrow.SetActive(true);
        StartCoroutine(TypeTalking(whatToSay));
    }

    public void LongTalk(string whatToSay)
    {
        StopTalking();
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

    public IEnumerator LoseGame()
    {
        BlackBoard.playerTurn = false;
        BlackBoard.otto.ShortTalk("Not funny");

        yield return new WaitForSeconds(3);

        BlackBoard.otto.ShortTalk("No more games");

        yield return new WaitForSeconds(3);
        
        BlackBoard.otto.ShortTalk("I'm taking your teeth");

        yield return new WaitForSeconds(3);

        Camera.main.GetComponent<Animator>().SetTrigger("Lost");

        yield return null;
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

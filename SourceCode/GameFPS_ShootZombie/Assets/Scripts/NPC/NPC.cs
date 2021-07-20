using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionNPCDisplay;
    public GameObject ActionNPCText;
    //public GameObject ExtraCross;
    public AudioSource lockedDoor;

    public GameObject screenFade;
    public GameObject questionImg;
    public GameObject questionText;

    public GameObject fakeMap;

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }


    void OnMouseOver()
    {
        if (TheDistance <= 6)
        {
            //ExtraCross.SetActive(true);
            ActionNPCText.GetComponent<Text>().text = "Để nói chuyện";
            ActionNPCDisplay.SetActive(true);
            ActionNPCText.SetActive(true);
        }
        if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 7)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                ActionNPCDisplay.SetActive(false);
                ActionNPCText.SetActive(false);
                //ExtraCross.SetActive(false);
                StartCoroutine(DoorReset());
                fakeMap.SetActive(false);
            }
        }
    }
    void OnMouseExit()
    {
        //ExtraCross.SetActive(false);
        ActionNPCDisplay.SetActive(false);
        ActionNPCText.SetActive(false);

    }
    IEnumerator DoorReset()
    {
        lockedDoor.Play();
        yield return new WaitForSeconds(1f);
        screenFade.SetActive(true);
        questionImg.SetActive(true);
        //questionText.GetComponent<Text>().text = "Hãy tìm 2 mảnh ghép làm key mở cách cửa end game";
        questionText.SetActive(true);
        
        yield return new WaitForSeconds(3);
        screenFade.SetActive(false);
        questionImg.SetActive(false);
        questionText.SetActive(false);
        //lockedDoor.Stop();
        this.GetComponent<BoxCollider>().enabled = true;
       
    }
}

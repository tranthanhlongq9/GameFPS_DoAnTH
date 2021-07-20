using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    //public GameObject ExtraCross;
    public AudioSource saveSound;

    

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }


    void OnMouseOver()
    {
        if (TheDistance <= 3)
        {
            //ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "Save Game";
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
        }
        if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 3)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
                //ExtraCross.SetActive(false);
                
                SavePlayer();
                StartCoroutine(DoorReset());

            }
        }
    }
    void OnMouseExit()
    {
        //ExtraCross.SetActive(false);
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);

    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    IEnumerator DoorReset()
    {

        //    lockedDoor.Play();
           yield return new WaitForSeconds(2f);
       
            this.GetComponent<BoxCollider>().enabled = true;

    }
}

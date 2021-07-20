using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionNPCDisplay;
    public GameObject ActionText;
    public GameObject ImgVictory;

    public AudioSource SongEndGame;

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if (TheDistance <= 5)
        {
            ActionText.GetComponent<Text>().text = "Để nói chuyện";
            ActionNPCDisplay.SetActive(true);
            ActionText.SetActive(true);
        }
        if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 5)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(LoadTheEnd());
            }
        }
    }

    void OnMouseExit()
    {
        //ExtraCross.SetActive(false);
        ActionNPCDisplay.SetActive(false);
        ActionText.SetActive(false);
        

    }

    IEnumerator LoadTheEnd()
    {
        SongEndGame.Play();
        ImgVictory.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(6);
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;


public class LoseWin : MonoBehaviour
{
    public GameObject Player1WinText;
    public GameObject Player2WinText;
    public GameObject Player1WinImage;
    public GameObject Player2WinImage;
    public float PauseTime = 1.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveScript.TimeOut = false;
        Player1WinText.gameObject.SetActive(false);
        Player2WinText.gameObject.SetActive(false);
        StartCoroutine(WinSet());
    }

    IEnumerator WinSet()
    {
        yield return new WaitForSeconds(0.4f);
        if (SaveScript.Player1Health > SaveScript.Player2Health)
        {
            Player1WinText.gameObject.SetActive(true);
            SaveScript.Player1Wins++;
        }
        else if (SaveScript.Player1Health < SaveScript.Player2Health)
        {
            Player2WinText.gameObject.SetActive(true);
            SaveScript.Player2Wins++;
        }

        if (SaveScript.Player1Wins >= 2)
        {
            Player1WinImage.gameObject.SetActive(true);
            SaveScript.TimeOut = true;
        }
        else if (SaveScript.Player2Wins >= 2)
        {
            Player2WinImage.gameObject.SetActive(true);
            SaveScript.TimeOut = true;
        }
        else
        {
            yield return new WaitForSeconds(PauseTime);
            SceneManager.LoadScene(0);
        }
    }
}

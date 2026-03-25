using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBars : MonoBehaviour
{
    public Image Player1Green;
    public Image Player2Green;
    public Image Player1Red;
    public Image Player2Red;
    public Image P1Win1;
    public Image P1Win2;
    public Image P2Win1;
    public Image P2Win2;
    public TextMeshProUGUI TimerText;
    public float LevelTime = 99;
    public GameObject WinCondition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveScript.Round++;
        SaveScript.TimeOut = true;
        if (SaveScript.Player1Wins == 1)
        {
            P1Win1.gameObject.SetActive(true);
        }

        if (SaveScript.Player1Wins == 2)
        {
            P1Win1.gameObject.SetActive(true);
            P1Win2.gameObject.SetActive(true);
        }

        if (SaveScript.Player2Wins == 1)
        {
            P2Win1.gameObject.SetActive(true);
        }

        if (SaveScript.Player2Wins == 2)
        {
            P2Win1.gameObject.SetActive(true);
            P2Win2.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (LevelTime > 0)
        {
            LevelTime -= 1 * Time.deltaTime; 
        }

        if (LevelTime <= 0.1)
        {
            SaveScript.TimeOut = true;
            WinCondition.gameObject.SetActive(true);
            WinCondition.gameObject.GetComponent<LoseWin>();
        }

        TimerText.text = Mathf.Round(LevelTime).ToString();
        
        Player1Green.fillAmount = SaveScript.Player1Health;
        Player2Green.fillAmount = SaveScript.Player2Health;

        if (SaveScript.Player2Timer > 0)
        {
            SaveScript.Player2Timer -= 2.0f * Time.deltaTime;
        }
        if (SaveScript.Player1Timer > 0)
        {
            SaveScript.Player1Timer -= 2.0f * Time.deltaTime;
        }

        if (SaveScript.Player2Timer <= 0)
        {
            if (Player2Red.fillAmount > SaveScript.Player2Health)
            {
                Player2Red.fillAmount -= 0.003f;
            }
        }
        if (SaveScript.Player1Timer <= 0)
        {
            if (Player1Red.fillAmount > SaveScript.Player1Health)
            {
                Player1Red.fillAmount -= 0.003f;
            }
        }
    }
}

using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public static float Player1Health = 1.0f;
    public static float Player2Health = 1.0f;
    public static float Player1Timer = 2.0f;
    public static float Player2Timer = 2.0f;
    public static bool TimeOut = false;
    public static int Player1Wins = 0;
    public static int Player2Wins = 0;
    public static int Round = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player1Health = 1.0f;
        Player2Health = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

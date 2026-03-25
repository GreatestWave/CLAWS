using UnityEngine;

public class P2MoveRestrict : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P1Left"))
        {
            Player2Move.WalkRightP2 = false;
        }
        if (other.gameObject.CompareTag("P1Right"))
        {
            Player2Move.WalkLeftP2 = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("P1Left"))
        {
            Player2Move.WalkRightP2 = true;
        }
        if (other.gameObject.CompareTag("P1Right"))
        {
            Player2Move.WalkLeftP2 = true;
        }
    }
}

using UnityEngine;

public class Player2Trigger : MonoBehaviour
{
    public Collider Col;
    public float DamageAmt = 0.1f;

    public bool EmitFx = false;
    public ParticleSystem Particles;
    
    // Update is called once per frame
    void Update()
    {
        if (Player2Actions.HitsP2 == false)
        {
            Col.enabled = true;
        }
        else
        {
            Col.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            if (EmitFx == true)
            {
                Particles.Play();
            }
            Player2Actions.HitsP2 = true;
            SaveScript.Player1Health -= DamageAmt;
            if (SaveScript.Player1Timer < 2.0f)
            {
                SaveScript.Player1Timer += 2.0f;
            }
        }
    }
}

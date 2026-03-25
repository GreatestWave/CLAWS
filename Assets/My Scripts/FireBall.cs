using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject Fireball;
    public float Speed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        Fireball.transform.Translate(Speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(Fireball.gameObject, 0.5f);
    }
}

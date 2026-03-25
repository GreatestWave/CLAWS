using System.Collections;
using UnityEngine;

public class Player2Move : MonoBehaviour
{
    private Animator Anim;
    public float JumpSpeed = 1.0f;
    public float WalkSpeed = 0.05f;
    public float MoveSpeed;
    private bool IsJumping = false;
    private AnimatorStateInfo Player1Layer0;
    private bool CanWalkLeft = true;
    private bool CanWalkRight = true;
    public GameObject Player1;
    public GameObject Opponent;
    private Vector3 OppPosition;
    public static bool FacingLeftP2 = false;
    public static bool FacingRightP2 = true;
    public static bool WalkLeftP2 = true;
    public static bool WalkRightP2 = true;
    public AudioClip LightPunch;
    public AudioClip HeavyPunch;
    public AudioClip LightKick;
    public AudioClip HeavyKick;
    private AudioSource MyPlayer;
    public GameObject Restrict;
    public Rigidbody RB;
    public Collider BoxCollider;
    public Collider CapsuleCollider;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FacingLeftP2 = false;
        FacingRightP2 = true;
        WalkLeftP2 = true;
        WalkRightP2 = true;
        Anim = GetComponentInChildren<Animator>();
        StartCoroutine(FaceRight());
        MyPlayer = GetComponentInChildren<AudioSource>();
        MoveSpeed = WalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.TimeOut == true)
        {
            Anim.SetBool("Forward", false);
            Anim.SetBool("Backward", false);
            //Facing left or right of the opponent
            if (OppPosition.x > Player1.transform.position.x)
            {
                StartCoroutine(FaceLeft());
            }

            if (OppPosition.x < Player1.transform.position.x)
            {
                StartCoroutine(FaceRight());
            }
        }
        if (SaveScript.TimeOut == false)
        {
            if (Player1Actions.FlyingJumpP1 == true)
            {
                WalkSpeed = JumpSpeed;
            }
            else
            {
                WalkSpeed = MoveSpeed;
            }

            //Check if we are knocked out
            if (SaveScript.Player2Health <= 0)
            {
                Anim.SetTrigger("KnockOut");
                Player1.GetComponent<Player2Actions>().enabled = false;
                StartCoroutine(KnockedOut());
                // this.GetComponent<Player2Move>().enabled = false;
            }
            if (SaveScript.Player1Health <= 0)
            {
                Anim.SetTrigger("Victory");
                Player1.GetComponent<Player2Actions>().enabled = false;
                this.GetComponent<Player2Move>().enabled = false;
            }
            //Listen to the Animator
            Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

            //Cannot exit screen 
            Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);

            if (ScreenBounds.x > Screen.width - 200)
            {
                CanWalkRight = false;
            }

            if (ScreenBounds.x < 200)
            {
                CanWalkLeft = false;
            }
            else if (ScreenBounds.x > 200 && ScreenBounds.x < Screen.width - 200)
            {
                CanWalkRight = true;
                CanWalkLeft = true;
            }

            //Get the opponent's position
            OppPosition = Opponent.transform.position;

            //Facing left or right of the opponent
            if (OppPosition.x > Player1.transform.position.x)
            {
                StartCoroutine(FaceLeft());
            }

            if (OppPosition.x < Player1.transform.position.x)
            {
                StartCoroutine(FaceRight());
            }

            // Walk right and left
            if (Player1Layer0.IsTag("Motion"))
            {
                if (Input.GetAxis("HorizontalP2") > 0)
                {
                    if (CanWalkRight == true)
                    {
                        if (WalkRightP2 == true)
                        {
                            Anim.SetBool("Forward", true);
                            transform.Translate(WalkSpeed, 0, 0);
                        }
                    }
                }

                if (Input.GetAxis("HorizontalP2") < 0)
                {
                    if (CanWalkLeft == true)
                    {
                        if (WalkLeftP2 == true)
                        {
                            Anim.SetBool("Backward", true);
                            transform.Translate(-WalkSpeed, 0, 0);
                        }
                    }
                }

                if (Input.GetAxis("HorizontalP2") == 0)
                {
                    Anim.SetBool("Forward", false);
                    Anim.SetBool("Backward", false);
                }
            }

            //Jumping and crouching 
            if (Input.GetAxis("VerticalP2") > 0)
            {
                if (IsJumping == false)
                {
                    IsJumping = true;
                    Anim.SetBool("Jump", true);
                    StartCoroutine(JumpPause());
                }
            }

            if (Input.GetAxis("VerticalP2") < 0)
            {
                Anim.SetBool("Crouch", true);
            }

            if (Input.GetAxis("VerticalP2") == 0)
            {
                Anim.SetBool("Crouch", false);
            }

            //Resets the restrict
            if (Restrict.gameObject.activeInHierarchy == false)
            {
                WalkLeftP2 = true;
                WalkRightP2 = true;
            }

            if (Player1Layer0.IsTag("Block") || Player1Layer0.IsTag("Swipe"))
            {
                RB.isKinematic = true;
                BoxCollider.enabled = false;
                CapsuleCollider.enabled = false;
            }
            else
            {
                BoxCollider.enabled = true;
                CapsuleCollider.enabled = true;
                RB.isKinematic = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FistLight"))
        {
            Anim.SetTrigger("HeadReact");
            MyPlayer.clip = LightPunch;
            MyPlayer.Play();
        }

        if (other.gameObject.CompareTag("FistHeavy"))
        {
            Anim.SetTrigger("BigReact");
            MyPlayer.clip = HeavyKick;
            MyPlayer.Play();
        }
        if (other.gameObject.CompareTag("KickHeavy"))
        {
            Anim.SetTrigger("BigReact");
            MyPlayer.clip = HeavyKick;
            MyPlayer.Play();
        }
        if (other.gameObject.CompareTag("KickLight"))
        {
            Anim.SetTrigger("HeadReact");
            MyPlayer.clip = LightKick;
            MyPlayer.Play();
        }
    }

IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
    }

    IEnumerator FaceLeft()
    {
        if (FacingLeftP2 == true)
        {
            FacingLeftP2 = false;
            FacingRightP2 = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, -180, 0);
            Anim.SetLayerWeight(1,0);
        }
    }
    
    IEnumerator FaceRight()
    {
        if (FacingRightP2 == true)
        {
            FacingRightP2 = false;
            FacingLeftP2 = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);
            Anim.SetLayerWeight(1,1);
        }
    }
    
    IEnumerator KnockedOut()
    {
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Player2Move>().enabled = false;
    }
}

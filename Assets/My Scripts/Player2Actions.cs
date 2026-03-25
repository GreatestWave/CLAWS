using System.Collections;
using UnityEngine;

public class Player2Actions : MonoBehaviour
{
    public float JumpSpeed = 1.0f;
    public float FlipSpeed = 0.8f;
    public GameObject Player1;
    private Animator Anim;
    private AnimatorStateInfo Player1Layer0;
    public float PunchSlideAmt = 10f;
    private bool HeavyMoving = false;
    private AudioSource MyPlayer;
    public AudioClip PunchWhoosh;
    public AudioClip KickWhoosh;
    public static bool HitsP2 = false;
    public static bool FlyingJumpP2 = false;
    
    private int attackNumberP2 = 0;
    private bool canAttackP2 = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.TimeOut == false)
        {
            //Heavy Punch Slide
            if (HeavyMoving == true)
            {
                if (Player2Move.FacingRightP2 == true)
                {
                    Player1.transform.Translate(PunchSlideAmt * Time.deltaTime, 0, 0);
                }

                if (Player2Move.FacingLeftP2 == true)
                {
                    Player1.transform.Translate(-PunchSlideAmt * Time.deltaTime, 0, 0);
                }
            }

            //Listen to the Animator
            Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

            //Standing attacks
            if (Player1Layer0.IsTag("Motion") || Player1Layer0.IsTag("Block"))
            {
                if (Input.GetButtonDown("Fire1P2"))
                {
                    attackNumberP2 += 5;
                    StartCoroutine(ResetNumber());
                }

                if (Input.GetButtonDown("Fire2P2"))
                {
                    attackNumberP2 += 15;
                    StartCoroutine(ResetNumber());
                }

                if (Input.GetButtonDown("Fire3P2"))
                {
                    attackNumberP2 += 20;
                    StartCoroutine(ResetNumber());
                }

                if (Input.GetButtonDown("JumpP2"))
                {
                    attackNumberP2 += 50;
                    StartCoroutine(ResetNumber());
                }

                if (canAttackP2 == true)
                {
                    canAttackP2 = false;
                    if (attackNumberP2 >= 5 && attackNumberP2 <= 14)
                    {
                        Anim.SetTrigger("LightPunch");
                        Anim.SetTrigger("BlockOff");
                        HitsP2 = false;
                    }

                    if (attackNumberP2 >= 15 && attackNumberP2 <= 19)
                    {
                        Anim.SetTrigger("HeavyPunch");
                        Anim.SetTrigger("BlockOff");
                        HitsP2 = false;
                    }

                    if (attackNumberP2 >= 20 && attackNumberP2 <= 44)
                    {
                        Anim.SetTrigger("LightKick");
                        Anim.SetTrigger("BlockOff");
                        HitsP2 = false;
                    }

                    if (attackNumberP2 == 45)
                    {
                        Anim.SetTrigger("Swipe");
                        Anim.SetTrigger("BlockOff");
                        HitsP2 = false;
                    }

                    if (attackNumberP2 == 50)
                    {
                        Anim.SetTrigger("HeavyKick");
                        Anim.SetTrigger("BlockOff");
                        HitsP2 = false;
                    }

                    if (attackNumberP2 > 50)
                    {
                        Anim.SetTrigger("DropKick");
                        Anim.SetTrigger("BlockOff");
                        HitsP2 = false;
                    }
                }

                if (Input.GetButtonDown("Block"))
                {
                    Anim.SetTrigger("BlockOn");
                }
            }

            if (Player1Layer0.IsTag("Block"))
            {
                if (Input.GetButtonUp("BlockP2"))
                {
                    Anim.SetTrigger("BlockOff");
                }
            }

            //Crouching Attack
            if (Player1Layer0.IsTag("Crouching"))
            {
                if (Input.GetButtonDown("JumpP2"))
                {
                    Anim.SetTrigger("HeavyKick");
                    HitsP2 = false;
                }
            }

            //Aerial moves
            if (Player1Layer0.IsTag("Jumping"))
            {
                if (Input.GetButtonDown("JumpP2"))
                {
                    Anim.SetTrigger("HeavyKick");
                    HitsP2 = false;
                }
            }
        }
    }
    public void JumpUp()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
    }

    public void HeavyPunchMove()
    {
        StartCoroutine(PunchSlide());
    }
    public void FlipUp()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
        FlyingJumpP2 = true;
        // Player1.transform.Translate(0.1f, 0 ,0 );
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
        FlyingJumpP2 = true;
        // Player1.transform.Translate(-0.1f, 0 ,0 );
    }
    
    public void IdleSpeed()
    {
        FlyingJumpP2 = false;
    }

    public void PunchWhooshSound()
    {
        MyPlayer.clip = PunchWhoosh;
        MyPlayer.Play();
    }
    
    public void KickWhooshSound()
    {
        MyPlayer.clip = KickWhoosh;
        MyPlayer.Play();
    }
    
    IEnumerator PunchSlide()
    {
        HeavyMoving = true;
        yield return new WaitForSeconds(0.1f);
        HeavyMoving = false;
    }
    
    IEnumerator ResetNumber()
    {
        yield return new WaitForSeconds(0.3f);
        canAttackP2 = true;
        yield return new WaitForSeconds(0.1f);
        attackNumberP2 = 0;
    }
}

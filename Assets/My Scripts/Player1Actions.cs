using System.Collections;
using UnityEngine;

public class Player1Actions : MonoBehaviour
{
    public float JumpSpeed = 1.0f;
    public GameObject Player1;
    private Animator Anim;
    private AnimatorStateInfo Player1Layer0;
    public float PunchSlideAmt = 2f;
    public float FThrustSlideAmt = 30f;
    private bool HeavyMoving = false;
    private bool FThrustMoving = false;
    private AudioSource MyPlayer;
    public AudioClip PunchWhoosh;
    public AudioClip KickWhoosh;
    public static bool Hits = false;
    public static bool FlyingJumpP1 = false;
    public Transform SpawnFB;
    public GameObject Fireball;

    private int attackNumber = 0;
    private bool canAttack = true;
    
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
            // Heavy punch / F-thrust slide
            if (HeavyMoving)
            {
                float slideAmt = PunchSlideAmt;
                if (Player1Move.FacingRight)
                    Player1.transform.Translate(slideAmt * Time.deltaTime, 0, 0);
                if (Player1Move.FacingLeft)
                    Player1.transform.Translate(-slideAmt * Time.deltaTime, 0, 0);
            }
            else if (FThrustMoving)
            {
                float slideAmt = FThrustSlideAmt;
                if (Player1Move.FacingRight)
                    Player1.transform.Translate(slideAmt * Time.deltaTime, 0, 0);
                if (Player1Move.FacingLeft)
                    Player1.transform.Translate(-slideAmt * Time.deltaTime, 0, 0);
            }

            //Listen to the Animator
            Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

            //Standing attacks
            if (Player1Layer0.IsTag("Motion") || Player1Layer0.IsTag("Block"))
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    attackNumber += 5;
                    StartCoroutine(ResetNumber());
                }

                if (Input.GetButtonDown("Fire2"))
                {
                    attackNumber += 15;
                    StartCoroutine(ResetNumber());
                }

                if (Input.GetButtonDown("Fire3"))
                {
                    attackNumber += 20;
                    StartCoroutine(ResetNumber());
                }

                if (Input.GetButtonDown("Jump"))
                {
                    attackNumber += 50;
                    StartCoroutine(ResetNumber());
                }

                if (canAttack == true)
                {
                    canAttack = false;
                    if (attackNumber >= 5 && attackNumber <= 14)
                    {
                        Anim.SetTrigger("LightPunch");
                        Anim.SetTrigger("BlockOff");
                        Hits = false;
                    }

                    if (attackNumber >= 15 && attackNumber <= 19)
                    {
                        Anim.SetTrigger("HeavyPunch");
                        Anim.SetTrigger("BlockOff");
                        Hits = false;
                    }

                    if (attackNumber == 20)
                    {
                        Anim.SetTrigger("LightKick");
                        Anim.SetTrigger("BlockOff");
                        Hits = false;
                    }

                    if (attackNumber == 25)
                    {
                        Anim.SetTrigger("Hadooken");
                        Anim.SetTrigger("BlockOff");
                        Hits = false;
                    }

                    if (attackNumber == 50)
                    {
                        Anim.SetTrigger("HeavyKick");
                        Anim.SetTrigger("BlockOff");
                        Hits = false;
                    }

                    if (attackNumber > 50)
                    {
                        Anim.SetTrigger("FThrust");
                        Anim.SetTrigger("BlockOff");
                        Hits = false;
                    }
                }

                if (Input.GetButtonDown("Block"))
                {
                    Anim.SetTrigger("BlockOn");
                }
            }

            if (Player1Layer0.IsTag("Block"))
            {
                if (Input.GetButtonUp("Block"))
                {
                    Anim.SetTrigger("BlockOff");
                }
            }

            //Crouching Attack
            if (Player1Layer0.IsTag("Crouching"))
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Anim.SetTrigger("HeavyKick");
                    Hits = false;
                }
            }

            //Aerial moves
            if (Player1Layer0.IsTag("Jumping"))
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Anim.SetTrigger("HeavyKick");
                    Hits = false;
                }
            }
        }
    }

    public void SpawnFBP1()
    {
        Instantiate(Fireball, SpawnFB.position, SpawnFB.rotation);
    }
    public void JumpUp()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
    }

    public void HeavyPunchMove()
    {
        StartCoroutine(PunchSlide());
    }
    
    public void FThrustMove()
    {
        StartCoroutine(FThrustSlide());
    }
    public void FlipUp()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
        FlyingJumpP1 = true;
        // Player1.transform.Translate(0.1f, 0 ,0 );
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
        FlyingJumpP1 = true;
        // Player1.transform.Translate(-0.1f, 0 ,0 );
    }

    public void IdleSpeed()
    {
        FlyingJumpP1 = false;
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

    IEnumerator FThrustSlide()
    {
        FThrustMoving = true;
        yield return new WaitForSeconds(0.1f);
        FThrustMoving = false;
    }

    IEnumerator ResetNumber()
    {
        yield return new WaitForSeconds(0.3f);
        canAttack = true;
        yield return new WaitForSeconds(0.1f);
        attackNumber = 0;
    }
}

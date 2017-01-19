using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    private Animator anim;
    private CharacterMovement movementScript;
    private AudioSource aud;

    public AudioClip hurtSFX;
    public AudioClip strongAttackSFX;
    public AudioClip weakAttackSFX;
    public float attackSpeed = 0.025f;

    public static float STRONGATK_ANIM_DUR = 0.5f;
    public static float WEAKATK_ANIM_DUR = 0.65f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movementScript = GetComponent<CharacterMovement>();
        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        AttackAnimate();

        if (anim.GetBool("IsHurt"))
        {
            aud.clip = hurtSFX;
            aud.Play();
        }
    }

    void AttackAnimate()
    {
        // Left click : Weak attack
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Left click");

            anim.SetBool("DoWeakAttack", true);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsJumping", false);
            anim.SetBool("DoStrongAttack", false);
            anim.SetBool("IsHurt", false);

            aud.clip = weakAttackSFX;
            aud.Play();

            movementScript.enabled = false;
            StartCoroutine(WeakAttackAnim());
        }

        // Right click : Strong attack
        if (Input.GetMouseButtonDown(1))
        {
            // Debug.Log("Right click");

            anim.SetBool("DoStrongAttack", true);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsRunning", false);
            anim.SetBool("DoWeakAttack", false);
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsHurt", false);

            aud.clip = strongAttackSFX;
            aud.Play();

            movementScript.enabled = false;
            StartCoroutine(StrongAttackAnim());
        }
    }

    IEnumerator WeakAttackAnim()
    {
        yield return new WaitForSeconds(WEAKATK_ANIM_DUR);
        movementScript.enabled = true;
    }

    IEnumerator StrongAttackAnim()
    {
        yield return new WaitForSeconds(STRONGATK_ANIM_DUR);
        movementScript.enabled = true;
    }
}

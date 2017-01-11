using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    private Animator anim;
    private CharacterMovement movementScript;

    public float attackSpeed = 0.025f;

    public static float STRONGATK_ANIM_DUR = 0.5f;
    public static float WEAKATK_ANIM_DUR = 0.65f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movementScript = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        AttackAnimate();
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

            movementScript.enabled = false;
            StartCoroutine(WeakAttack());
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

            movementScript.enabled = false;
            StartCoroutine(StrongAttack());
        }
    }

    IEnumerator WeakAttack()
    {
        yield return new WaitForSeconds(WEAKATK_ANIM_DUR);
        movementScript.enabled = true;
    }

    IEnumerator StrongAttack()
    {
        yield return new WaitForSeconds(STRONGATK_ANIM_DUR);
        movementScript.enabled = true;
    }
}

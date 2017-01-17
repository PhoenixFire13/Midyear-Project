using UnityEngine;
using System.Collections;

public class StrongAttack : MonoBehaviour {

    private Animator playerAnim;
    private bool isAttacking;

    private Animator opponentAnim;
    private Rigidbody opponentRB;
    private PlayerHealth opponentHealth;
    private CharacterMovement opponentMovement;

    public static float HURT_ANIM_DUR = 0.05f;

    public LayerMask explosionMask;
    public float knockbackRadius;
    public float knockbackPower;
    public float upwardsModifier;
    public int damage;

    void Awake()
    {
        playerAnim = this.transform.parent.GetComponentInParent<Animator>();
    }

    void Update()
    {
        isAttacking = playerAnim.GetBool("DoStrongAttack");
    }

    private void OnTriggerEnter (Collider other)
    {
        if (isAttacking && other.tag == "Player")
        {
            // Debug.Log(other.name + ": " + other.tag);

            opponentRB = other.GetComponent<Rigidbody>();
            opponentHealth = other.GetComponent<PlayerHealth>();
            opponentMovement = other.GetComponent<CharacterMovement>();
            opponentAnim = other.GetComponent<Animator>();

            DealDamage();
            Knockback();
            Animate();
        }
    }

    private void DealDamage()
    {
        opponentHealth.TakeDamage(damage);
    }

    // --------------- Knockback distance depending on damage already dealt to opponent ---------------
    private void Knockback()
    {
        Debug.Log("Knockback");

        // --------------- Flying Kirby Easter Egg ---------------
        Collider[] colliders = Physics.OverlapSphere(transform.position, knockbackRadius);
        foreach (Collider coll in colliders)
        {
            Rigidbody opponentRB = coll.GetComponent<Rigidbody>();
            if (opponentRB != null)
            {
                opponentRB.isKinematic = false;
                opponentRB.AddExplosionForce(knockbackPower, transform.position, knockbackRadius, upwardsModifier, ForceMode.Impulse);
            }
        }
    }

    private void Animate()
    {
        opponentAnim.SetBool("IsHurt", true);
        opponentAnim.SetBool("IsIdle", false);
        opponentAnim.SetBool("IsRunning", false);
        opponentAnim.SetBool("DoWeakAttack", false);
        opponentAnim.SetBool("IsJumping", false);
        opponentAnim.SetBool("DoStrongAttack", false);

        opponentMovement.enabled = false;
        StartCoroutine(StrongAtk());
    }

    IEnumerator StrongAtk()
    {
        yield return new WaitForSeconds(HURT_ANIM_DUR);

        opponentMovement.enabled = true;
        opponentAnim.SetBool("IsHurt", false);
    }

}

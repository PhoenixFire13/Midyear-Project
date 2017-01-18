using UnityEngine;
using System.Collections;

public class StrongAttack : MonoBehaviour {

    private Animator playerAnim;
    private bool isAttacking;

    private Animator opponentAnim;
    private Rigidbody opponentRB;
    private PlayerHealth opponentHealth;
    private CharacterMovement opponentMovement;
    private StrongAttack opponentSAtk_Script;
    private int damageTaken;

    public static float HURT_ANIM_DUR = 0.05f;

    public LayerMask explosionMask;
    public float knockbackRadius;
    public int damage;

    void Awake()
    {
        playerAnim = this.transform.parent.GetComponentInParent<Animator>();
    }

    void Update()
    {
        isAttacking = playerAnim.GetBool("DoStrongAttack");

        if (opponentHealth != null)
            damageTaken = opponentHealth.GetCurrentHealth();
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
            opponentSAtk_Script = other.GetComponentInChildren<StrongAttack>();

            DealDamage();
            opponentSAtk_Script.Knockback();
            Animate();
        }
    }

    private void DealDamage()
    {
        opponentHealth.TakeDamage(damage);
    }

    private float CalculateKB()
    {
        return damageTaken / 50;
    }

    private float CalculateJM()
    {
        if (damageTaken < 100)
            return 0;
        else
            return damageTaken / 100;
    }

    private Vector3 CalculateExploPos()
    {
        Vector3 exploPos = transform.position;
        exploPos += new Vector3(0f, 1f, 1f);

        return exploPos;
    }

    // --------------- Knockback distance depending on damage already dealt to opponent ---------------
    public void Knockback()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, knockbackRadius);
        foreach (Collider coll in colliders)
        {
            Rigidbody opponentRB = coll.GetComponent<Rigidbody>();
            if (opponentRB != null)
            {
                opponentRB.isKinematic = false;

                // Debug.Log(opponentSAtk_Script + ": " + damageTaken + ", " + CalculateKB() + ", " + CalculateExploPos() + ", " + CalculateJM());
                opponentRB.AddExplosionForce(CalculateKB(), CalculateExploPos(), knockbackRadius, CalculateJM(), ForceMode.Impulse);
            }
        }

        // --------------- Backup in case things don't go well ---------------
        /*
        Collider[] colliders = Physics.OverlapSphere(transform.position, knockbackRadius);
        foreach (Collider coll in colliders)
        {
            Rigidbody opponentRB = coll.GetComponent<Rigidbody>();
            if (opponentRB != null)
            {
                opponentRB.isKinematic = false;

                Vector3 exploPos = transform.position;
                exploPos += new Vector3(0f, 1f, 1f);

                opponentRB.AddExplosionForce(knockbackPower, exploPos, knockbackRadius, upwardsModifier, ForceMode.Impulse);
            }
        }
        */
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

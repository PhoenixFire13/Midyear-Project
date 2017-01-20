using UnityEngine;
using System.Collections;

public class WeakAttack : MonoBehaviour {

    private Animator playerAnim;
    private bool isAttacking;

    private Animator opponentAnim;
    private Rigidbody opponentRB;
    private PlayerHealth opponentHealth;
    private CharacterMovement opponentMovement;
    private WeakAttack opponentWAtk_Script;
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
        isAttacking = playerAnim.GetBool("DoWeakAttack");

        if (opponentHealth != null)
            damageTaken = opponentHealth.GetCurrentHealth();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && other.tag == "Player")
        {
            Debug.Log("ViewID: " + other.gameObject.GetComponentInParent<PhotonView>().viewID);

            opponentRB = other.GetComponentInParent<Rigidbody>();
            opponentHealth = other.GetComponentInParent<PlayerHealth>();
            opponentMovement = other.GetComponentInParent<CharacterMovement>();
            opponentAnim = other.GetComponentInParent<Animator>();
            opponentWAtk_Script = other.GetComponentInChildren<WeakAttack>();

            DealDamage();
            opponentWAtk_Script.Knockback();
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

                // Debug.Log(opponentWAtk_Script + ": " + damageTaken + ", " + CalculateKB() + ", " + CalculateExploPos() + ", " + CalculateJM());
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
        StartCoroutine(WeakAtk());
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(damageTaken);
        }
        else
        {
            opponentHealth.TakeDamage((int)stream.ReceiveNext() - damageTaken);
        }
    }

    IEnumerator WeakAtk()
    {
        yield return new WaitForSeconds(HURT_ANIM_DUR);

        opponentMovement.enabled = true;
        opponentAnim.SetBool("IsHurt", false);
    }

}

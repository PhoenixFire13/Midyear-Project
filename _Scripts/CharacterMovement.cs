using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    private Animator anim;
    private Rigidbody rb;
    private float xzMovement;
    private float yTurn;

    public float moveSpeed = 0.015f;
    public float turnSpeed = 180f;
    public float jumpDistance = 20f;
    public float strongAtkDistance = 25f;

    // Adjust timing
    public static float JUMP_ANIM_DUR = 1.04f;
    public static float STRONGATK_ANIM_DUR = 0.15f;

	void Awake () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}

    void OnEnable()
    {
        rb.isKinematic = false;

        xzMovement = 0f;
        yTurn = 0f;
    }

    void OnDisable()
    {
        rb.isKinematic = true;
    }

    void Update()
    {
        xzMovement = Input.GetAxis("Vertical");
        yTurn = Input.GetAxis("Horizontal");

        Animate();
    }

    void Animate()
    {
        if (xzMovement <= 0.01f || xzMovement >= -0.01f)
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsJumping", false);
            anim.SetBool("DoStrongAttack", false);
            anim.SetBool("DoWeakAttack", false);
        }

        if (xzMovement > 0.01f || xzMovement < -0.01f)
        {
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsJumping", false);
            anim.SetBool("DoStrongAttack", false);
            anim.SetBool("DoWeakAttack", false);
        }

        if (Input.GetKeyDown("space"))
        {
            anim.SetBool("IsJumping", true);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsRunning", false);
            anim.SetBool("DoStrongAttack", false);
            anim.SetBool("DoWeakAttack", false);

            StartCoroutine(Jump());
        }

        // ---------------Clicks are being registered but animations not playing---------------

        /*
        // Left click : Weak attack
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left click");

            anim.SetBool("DoWeakAttack", true);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsJumping", false);
            anim.SetBool("DoWeakAttack", false);
        }

        // Right click : Strong attack
        if (Input.GetMouseButtonDown(3))
        {
            Debug.Log("Right click");

            anim.SetBool("DoWeakAttack", true);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsRunning", false);
            anim.SetBool("DoStrongAttack", false);
            anim.SetBool("IsJumping", false);
        }
        */
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }

    void Move()
    {
        Vector3 movement = transform.forward * xzMovement * moveSpeed;
        rb.MovePosition(rb.position + movement);

        xzMovement = 0f;
    }

    void Turn()
    {
        float turn = yTurn * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        rb.MoveRotation(rb.rotation * turnRotation);

        yTurn = 0f;
    }

    // ---------------Fix buggy jump animation---------------
    IEnumerator Jump()
    {
        yield return new WaitForSeconds(JUMP_ANIM_DUR);

        Vector3 jumpMovement = transform.forward * jumpDistance * moveSpeed;
        rb.MovePosition(rb.position + jumpMovement);
    }

    IEnumerator StrongAttack()
    {
        yield return new WaitForSeconds(STRONGATK_ANIM_DUR);

        Vector3 atkMovement = transform.forward * jumpDistance * moveSpeed;
        rb.MovePosition(rb.position + atkMovement);
    }

}

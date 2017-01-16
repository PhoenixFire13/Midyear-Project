using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    private Animator anim;
    private Rigidbody rb;
    private float xzMovement;
    private float yTurn;

    public float moveSpeed = 0.035f;
    public float turnSpeed = 180f;
    public float jumpDistance = 25f;
    public float jumpHeight = 50f;

    public static float JUMP_ANIM_DUR = 1f;

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

            StartCoroutine(JumpAnim());
        }
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
        Quaternion turnRotation = Quaternion.Euler(0f, yTurn, 0f);
        transform.rotation = Quaternion.LerpUnclamped(transform.rotation, transform.rotation * turnRotation, turnSpeed * Time.deltaTime);

        yTurn = 0f;
    }

    IEnumerator JumpAnim()
    {
        yield return new WaitForSeconds(JUMP_ANIM_DUR);

        Vector3 jumpMovement = new Vector3(jumpDistance, jumpHeight, 0f) * moveSpeed;
        rb.MovePosition(Vector3.Lerp(rb.position, rb.position + jumpMovement, 0.15f));
    }

}

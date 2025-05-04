using UnityEngine;
public class characterController : MonoBehaviour
{
    public float jumpForce = 2.0f;
    public float speed = 1.0f;
    private float moveDirection;
    private bool moving;
    private bool jump;
    private bool grounded = true;
    private bool isAttacking = false;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2d;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>(); 
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (_rigidbody2d.linearVelocity != Vector2.zero)
            moving = true;
        else
            moving = false;

        _rigidbody2d.linearVelocity = new Vector2(speed*moveDirection,_rigidbody2d.linearVelocityY);

        if(jump)
        {
            _rigidbody2d.linearVelocity = new Vector2(_rigidbody2d.linearVelocityX,jumpForce);
            jump = false;
        }
    }
    void Update()
    {
        if (grounded && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection = -1.0f;
                _spriteRenderer.flipX = true;
                anim.SetFloat("speed",speed);
            }
            else
            {
                moveDirection = +1.0f;
                _spriteRenderer.flipX = false;
                anim.SetFloat("speed",speed);
            }
        }
        else if (grounded)
        {
            moveDirection = 0.0f;
            anim.SetFloat("speed",0);
        }
        if (grounded && Input.GetKey(KeyCode.W))
        {
            jump = true;
            grounded = false;
            anim.SetTrigger("jump");
            anim.SetBool("grounded",false);
        }
        if (Input.GetKeyDown(KeyCode.K) && !isAttacking)
        {
            anim.SetTrigger("attack");
            anim.SetBool("isattacking",true);
            isAttacking = true;
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            anim.SetBool("grounded",true);
        }
    }
    public void EndAttack()
    {
        anim.SetBool("isattacking", false);
        isAttacking = false;
    }
}

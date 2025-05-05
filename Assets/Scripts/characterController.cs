using UnityEngine;
public class characterController : MonoBehaviour
{
    public float jumpForce = 2.0f;
    public float speed = 1.0f;
    private float moveDirection;
    private bool moving;
    private bool jump;
    private bool grounded = true;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2d;
    private Animator anim;

    public GameObject ghostPrefab;
    private GhostRecorder ghostRecorder;
    public float ghostRecordTime = 3f;
    private GameObject activeGhost;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>(); 
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ghostRecorder = gameObject.AddComponent<GhostRecorder>();
        ghostRecorder.recordTime = ghostRecordTime;
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
        if (Input.GetKeyDown(KeyCode.E) && ghostPrefab != null && ghostRecorder != null && activeGhost == null)
        {
            activeGhost = Instantiate(ghostPrefab, transform.position, transform.rotation);
            GhostPlayer gp = activeGhost.GetComponent<GhostPlayer>();
            if (gp != null)
            {
                gp.Play(ghostRecorder.GetLastRecords());
            }
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
}

using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    PlayerInput input;

    public float speed;
    public float jump_force;
    [SerializeField] float fall_multiplier;
    [SerializeField] float low_jump_mult;

    public Vector2 move_input = Vector2.zero;

    RaycastHit2D hit = new RaycastHit2D();
    Vector2 raycast_size = new Vector2(0.75f, 0.9f);
    Vector2 raycast_origin = Vector2.zero;
    public bool grounded = false;

    public Attack atk_script;
    public AttackTimming atk_timming_script;
    public bool atacking_timing = false;
    public bool can_timming_atk = false;

    [SerializeField] TurnWeapon[] turn_script;
    [SerializeField] TurnSprite turn_sprite_script;

    public Animator animator;

    public int life = 5;
    public LifeBarScript life_bar;

    #region Singleton

    public static Player Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = rb.GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        atk_script = GetComponentInChildren<Attack>();
        atk_timming_script = GetComponentInChildren<AttackTimming>();
        turn_script = GetComponentsInChildren<TurnWeapon>();
        turn_sprite_script = GetComponentInChildren<TurnSprite>();

        rb.linearVelocity = Vector2.zero;
    }

    void OnMove(InputValue value)
    {
        if (life > 0)
        {
            if (Player.Instance.atk_script.canAtack)
            {
                move_input = value.Get<Vector2>();
            }
            else
            {
                move_input = new Vector2(0, 0);
            }
            
            foreach (var weapon in turn_script)
                weapon.Turn();
            turn_sprite_script.TurnScale();
        }
        else
        {
            move_input = new Vector2(0, 0);
        }
    }

    public void Jump()
    {
        Debug.Log("Pulou metodo");
        grounded = true;
        OnJump();
    }

    void OnJump()
    {
        Debug.Log("Pulou normal");
        if (grounded && life > 0)
        {
            animator.SetTrigger("jumped");
            animator.SetBool("grounded", false);
            rb.linearVelocityY = jump_force;            
        }
    }

    void OnAttack()
    {
        if (life > 0)
        {
            if (!can_timming_atk)
            {
                atk_script.Atk();
            }
            else if (can_timming_atk && !grounded)
            {
                atk_timming_script.Atk();
            }
        }
    }

    void Grounded()
    {
        hit = Physics2D.BoxCast(raycast_origin, raycast_size, 0, Vector2.zero);

        if (hit && hit.collider.CompareTag("ground"))
        {
            grounded = true;
            animator.SetBool("falling", false);
            animator.SetBool("grounded", grounded);
        }
        else
        {
            grounded = false;
            animator.SetBool("grounded", grounded);
        }
    }

    void Animation()
    {
        if(move_input.x > 0 || move_input.x < 0)
        {
            animator.SetBool("running", true);
        } else
        {
            animator.SetBool("running", false);
        }
    }

    void MoveCharacter()
    {
        float result = move_input.x * speed * Time.fixedDeltaTime;
        if (Player.Instance.atk_script.canAtack && life > 0)
            rb.linearVelocityX = result;
    }

    private void Update()
    {
        if (life > 0)
        {
            raycast_origin = new Vector2(transform.position.x, transform.position.y - 0.47f);
            if (life > 0)
            { 
                Grounded();
                Animation();
            }

            // Transition from going up to falling
            if (rb.linearVelocity.y < 0)
            {
                animator.SetBool("falling", true);
            }
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;

    //    Vector2 origin = new Vector2(transform.position.x, transform.position.y - 0.47f);
    //    Vector3 size = new Vector3(0.75f, 0.9f, 0.01f);

    //    Gizmos.DrawCube(origin, size);
    //}

    private void FixedUpdate()
    {
        if(life > 0)
        {
            MoveCharacter();
        }
        else
        {
            rb.linearVelocityX = 0;
        }
            

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fall_multiplier - 1) * Time.fixedDeltaTime;
        } else if (rb.linearVelocityY > 0 && !input.actions["Jump"].IsPressed())
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (low_jump_mult - 1) * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("triggerscene"))
        {
            
        }
    }
}

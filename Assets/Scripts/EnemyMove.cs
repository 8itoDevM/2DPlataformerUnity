using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 2f;
    public Transform player;
    public bool followPlayer = false;
    public float detectionRange = 5f;
    public float atack_range = 1f;
    public LayerMask obstacleMask;

    Vector3 direction;
    SpriteRenderer sprite;
    public Animator animator;

    public int damage = 0;

    private states lastChaseState;

    public enum states { Right, Left, Still }
    int current_state_int = 0;
    public states current_state;

    float time = 1;
    public bool is_edging = false;

    public float attackCooldown;
    private bool isAttacking = false;

    public Vector3 Direction => direction;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = sprite.GetComponent<Animator>();

        player = Player.Instance.transform;

        ChangeState();
        StartCoroutine(ChangeStateRepeat());
    }

    void Update()
    {
        DetectPlayerRay();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        bool withinAttackRange = distanceToPlayer <= atack_range;

        if (followPlayer && !is_edging && !withinAttackRange)
        {
            Patrol(direction, current_state, true);
        }
        else if (followPlayer && is_edging)
        {
            Patrol(direction, states.Still, false);
        }
        else if (!is_edging && !withinAttackRange)
        {
            Patrol(direction, current_state, true);
        }
        else
        {
            Patrol(direction, states.Still, false);
        }
    }



    void Patrol(Vector3 dir, states state, bool shouldMove = true)
    {
        switch (state)
        {
            case states.Left:
                Turn();
                if (shouldMove) transform.position += direction * speed * Time.deltaTime;
                animator.SetInteger("anim", 2);
                Turn(true);
                break;
            case states.Right:
                Turn();
                if (shouldMove) transform.position += direction * speed * Time.deltaTime;
                animator.SetInteger("anim", 3);
                Turn(false);
                break;
            case states.Still:
                animator.SetInteger("anim", 1);
                break;
        }
    }


    void Turn(bool faceLeft)
    {
        Vector3 scale = sprite.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (faceLeft ? -1 : 1);
        sprite.transform.localScale = scale;
    }

    void Turn()
    {
        direction = current_state == states.Right ? Vector3.right : Vector3.left;
    }

    private IEnumerator ChangeStateRepeat()
    {
        ChangeState();
        yield return new WaitForSeconds(time);
        time = Random.Range(0.2f, 2);
        StartCoroutine(ChangeStateRepeat());
    }

    void ChangeState()
    {
        while ((int)current_state == current_state_int)
        {
            current_state_int = Random.Range(0, 3);
        }
        current_state = (states)current_state_int;
    }

    public void EdgingDetected()
    {
        if (direction == Vector3.right)
        {
            current_state = states.Left;
            direction = Vector3.left;
        }
        else if (direction == Vector3.left)
        {
            current_state = states.Right;
            direction = Vector3.right;
        }
    }

    public void DetectPlayerRay()
    {
        if (player == null) return;

        Vector2 dirToPlayer = player.position - transform.position;
        float distance = dirToPlayer.magnitude;

        if (distance <= detectionRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlayer.normalized, distance, obstacleMask);

            if (hit.collider == null)
            {
                followPlayer = true;

                if (distance > atack_range)
                {
                    // Update direction only if not attacking
                    if (dirToPlayer.x < 0)
                    {
                        current_state = states.Left;
                        direction = Vector3.left;
                        lastChaseState = states.Left;
                    }
                    else
                    {
                        current_state = states.Right;
                        direction = Vector3.right;
                        lastChaseState = states.Right;
                    }
                }
                else
                {
                    // Lock direction to last chase direction
                    current_state = lastChaseState;
                }
            }
            else
            {
                followPlayer = false;
            }
        }
        else
        {
            followPlayer = false;
        }

        // Attack if close enough and not already attacking
        if (distance <= atack_range && !isAttacking)
        {
            StartCoroutine(AttackPlayerCoroutine());
        }
    }

    // Coroutine to handle continuous attacks
    IEnumerator AttackPlayerCoroutine()
    {
        isAttacking = true;  // Mark as attacking

        while (true)
        {
            if (Player.Instance != null && Player.Instance.life > 0)
            {
                Patrol(direction, states.Still);

                animator.SetTrigger("attack");

                Player.Instance.life -= damage;
                Player.Instance.life_bar.UpdateHearts();

                // If the player is dead, trigger the "dead" animation and stop the attack loop
                if (Player.Instance.life <= 0)
                {
                    Player.Instance.animator.SetTrigger("dead");
                    break;  // Exit the loop if player is dead
                }
            }

            // Wait for the attack cooldown before attacking again
            yield return new WaitForSeconds(attackCooldown);

            // Exit the loop if player goes out of range
            float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);
            if (distance > atack_range)
            {
                break;
            }
        }

        isAttacking = false;  // Reset the attacking flag when the coroutine finishes
    }
    private Coroutine attackRoutine;


    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRange);

            Vector2 dirToPlayer = player.position - transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)dirToPlayer.normalized * detectionRange);
        }
    }
}

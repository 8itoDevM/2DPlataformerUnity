using UnityEngine;

public class EnemyAvoidEdging : MonoBehaviour
{
    public float edgeCheckDistance = 0.2f;
    public Transform groundCheckPoint;
    public LayerMask groundLayer;

    [SerializeField] Vector2 size = new Vector2(0.5f, 0.3f);

    EnemyMove enemy_move;

    private void Start()
    {
        enemy_move = GetComponent<EnemyMove>();
    }

    void Update()
    {
        if (!IsGroundAhead())
        {
            enemy_move.EdgingDetected();
        }
    }

    bool IsGroundAhead()
    {
        if (enemy_move.current_state == EnemyMove.states.Still)
            return true;

        Vector2 moveDir = enemy_move.Direction.normalized;
        Vector2 offset = moveDir * 0.3f;
        Vector2 origin = (Vector2)groundCheckPoint.position + offset;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, edgeCheckDistance, groundLayer);
        Debug.DrawRay(origin, Vector2.down * edgeCheckDistance, Color.red);

        return hit.collider != null;
    }
}

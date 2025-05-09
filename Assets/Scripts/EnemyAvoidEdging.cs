using UnityEngine;

public class EnemyAvoidEdging : MonoBehaviour
{
    public float edgeCheckDistance = 0.5f;
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
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, edgeCheckDistance, groundLayer);
        return hit.collider != null;
    }
}

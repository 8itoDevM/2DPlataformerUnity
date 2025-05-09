using UnityEngine;

public class BarrierEffect : GetAttacked
{
    public override void Effect()
    {
        base.Effect();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("taco"))
        {
            Effect();
            return;
        }

        if (collision.CompareTag("enemy") && !collision.GetComponent<EnemyManager>().alive)
        {
            Effect();
            return;
        }
    }
}

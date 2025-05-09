using UnityEngine;

public class BoxEffect : GetAttacked
{
    Rigidbody2D rb;

    public override void Effect()
    {
        rb = GetComponent<Rigidbody2D>();
        base.Effect();
        rb.AddForce((new Vector2(0, 1.6f)) * 8, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("taco"))
            Effect();
    }
}

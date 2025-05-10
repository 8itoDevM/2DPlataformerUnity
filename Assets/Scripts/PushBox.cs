using UnityEngine;

public class PushBox : MonoBehaviour
{
    Rigidbody2D box_rb;
    bool being_pushed;
    float normal_speed;

    private void Start()
    {
        box_rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (being_pushed)
        {
            box_rb.freezeRotation = true;
        }
        else
        {
            box_rb.freezeRotation = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            normal_speed = Player.Instance.speed;
            Player.Instance.speed = Player.Instance.speed / 1.7f;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Player.Instance.animator.SetBool("running", true);
            being_pushed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            Player.Instance.speed = normal_speed;
            being_pushed = false;
            
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}

using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public ButtonUse affected_object;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            affected_object.Click();
            return;
        }

        if (collision.CompareTag("enemy") && !collision.GetComponent<EnemyManager>().alive)
        {
            affected_object.Click();
            return;
        }
    }
}

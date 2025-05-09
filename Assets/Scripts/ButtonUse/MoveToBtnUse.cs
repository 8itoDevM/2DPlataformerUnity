using UnityEditor.UIElements;
using UnityEngine;

public class MoveToBtnUse : ButtonUse
{
    public Vector3 to = Vector3.zero;
    public float speed = 0f;

    bool start_moving = false;

    public override void Click()
    {
        base.Click();
        start_moving = true;
    }

    private void Update()
    {
        if (start_moving && transform.position != to)
        {
            Vector3 direction = (to - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, to) < 0.01f)
            {
                transform.position = to;
                start_moving = false;
            }
        }
    }
}

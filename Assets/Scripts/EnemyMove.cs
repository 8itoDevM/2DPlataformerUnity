using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 0;
    Vector3 direction;

    public enum states { Right, Left, Still }

    int current_state_int = 0;
    public states current_state;

    float time = 1;

    bool is_edging = false;

    private void Start()
    {
        ChangeState();
        StartCoroutine(ChangeStateRepeat());
    }

    void Update()
    {
        if (!is_edging)
            Patrol(direction, current_state);
    }

    void Patrol(Vector3 dir, states state)
    {
        switch (state)
        {
            case states.Left:
                Turn();
                transform.position += direction * speed * Time.deltaTime;
                break;
            case states.Right:
                Turn();
                transform.position += direction * speed * Time.deltaTime;
                break;
            case states.Still:
                break;
            default:
                break;
        }
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
        current_state_int = Random.Range(0, 3);
        current_state = (states)current_state_int;
    }

    void Turn()
    {
        if(current_state == states.Right)
        {
            direction = Vector3.right;
        }
        else if (current_state == states.Left)
        {
            direction = Vector3.left;
        }
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
}

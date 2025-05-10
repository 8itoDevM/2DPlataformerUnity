using UnityEngine.SceneManagement;
using UnityEngine;

public class LastTgManager : MonoBehaviour
{
    public Vector3 lastTriggerPosition;
    public string lastTriggerName;
    public string lastTriggerID;
    public Scene last_scene;

    public Vector3? pendingTeleportPosition; 


    // Script meant to make scene transitions make sense
    // Going in a scene to your left, makes you appear on the right
    // going back, makes you appear on the left again
    public void SetTrigger(Collider2D collision, string id)
    {
        lastTriggerPosition = collision.transform.position;
        lastTriggerName = collision.transform.name;
        lastTriggerID = id;
        last_scene = SceneManager.GetActiveScene();
        pendingTeleportPosition = null; 
    }

    public void SetPendingTeleportPosition(Vector3 pos)
    {
        pendingTeleportPosition = pos;
    }
}

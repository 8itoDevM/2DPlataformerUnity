using UnityEngine.SceneManagement;
using UnityEngine;

public class LastTgManager : MonoBehaviour
{
    public Vector3 lastTriggerPosition;
    public string lastTriggerName;
    public string lastTriggerID;
    public Scene last_scene;

    public Vector3? pendingTeleportPosition; 

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

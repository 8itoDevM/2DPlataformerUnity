using UnityEngine.SceneManagement;
using UnityEngine;

public class GoToScene : MonoBehaviour
{
    public string scene_name;
    public string triggerID;

    // Goes inside of the triggers and triggers all the methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("player")) return;

        var lastManager = GameManager.InstanceGameManager.lastTgManager;

        if (lastManager.last_scene.name != scene_name || lastManager.lastTriggerID != triggerID)
        {
            lastManager.SetTrigger(GetComponent<Collider2D>(), triggerID);
            lastManager.SetPendingTeleportPosition(transform.position);

            SceneManager.LoadScene(scene_name);
        }
        else
        {
            GameManager.InstanceGameManager.ChangeSceneAndTeleport(scene_name);
        }
    }
}

using UnityEngine.SceneManagement;
using UnityEngine;

public class GoToScene : MonoBehaviour
{
    public string scene_name;

    // Goes inside of the triggers and triggers all the methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("player")) return;

        SceneManager.LoadScene(scene_name);
    }
}

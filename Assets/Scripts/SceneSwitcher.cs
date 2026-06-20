using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad;
    public float spawnX;
    public float spawnY;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("SpawnX", spawnX);
            PlayerPrefs.SetFloat("SpawnY", spawnY);
            PlayerPrefs.Save();
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
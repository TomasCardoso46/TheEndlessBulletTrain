using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;
    public int level = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        if (NoObjectsWithTag("CRT") && level == 1)
        {
            level = 2; // Update level first before loading the scene
            SceneManager.LoadScene(2);
        }
        else if (NoObjectsWithTag("CRT") && level == 2)
        {
            SceneManager.LoadScene(4);
        }
    }

    private bool NoObjectsWithTag(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag).Length == 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene(3);
    }

    public void BackCredits()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        level = 1; // Reset the level when starting the game
        SceneManager.LoadScene(1);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;

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
        if (NoObjectsWithTag("CRT"))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.LogError("Cannot proceed to the next level. Objects with tag 'CRT' still exist.");
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
        SceneManager.LoadScene(4);
    }

    public void BackCredits()
    {
        SceneManager.LoadScene(0);
    }
}

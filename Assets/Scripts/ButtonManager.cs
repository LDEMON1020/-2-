using UnityEditor;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
               UnityEngine.SceneManagement.SceneManager.LoadScene("Scene Stage_1");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; 
#else
        Application.Quit();
#endif
    }
    public void TitleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title Scene");
    }
    public void Replay()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}

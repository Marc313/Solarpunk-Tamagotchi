using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int index;

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int index)
    {
        this.index = index;
        Invoke(nameof(LoadSceneInvokable), 0.05f);
    }

    public void LoadSceneInvokable()
    {
        SceneManager.LoadScene(index);
    }
}

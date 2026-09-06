using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void quit()
    {
        Application.Quit(); 
    }
}

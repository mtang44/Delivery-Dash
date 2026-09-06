using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static int FinalScore; 
    
   
    public void GameOver(int score)
    {
        // Save the score to the static variable right before switching scenes
        FinalScore = score;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game Over");
    }
}

using UnityEngine;
using System.Collections;
using TMPro;

public class GameOverScore : MonoBehaviour
{
    public GameObject gameOverObject; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      gameOverObject = GameObject.Find("Game Over Manager");
      gameObject.GetComponent<TextMeshProUGUI>().text = "Number of Items Delivered: " + GameOverManager.FinalScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

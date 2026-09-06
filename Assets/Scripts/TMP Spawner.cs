using UnityEngine;
using TMPro; 
using System.Collections;
public class TMPSpawner : MonoBehaviour
{
    public GameObject displayTMP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnUI(string text)
    {
        GameObject newTMP = Instantiate(displayTMP); 
        newTMP.transform.SetParent(gameObject.transform, false);
        newTMP.GetComponent<TextMeshProUGUI>().text = text;
        Destroy(newTMP,3);
    }
   
}

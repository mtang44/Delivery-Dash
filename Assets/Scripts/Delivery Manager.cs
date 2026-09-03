using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// A script to manage the how food orders are created, assigning the corresponding food UI to the directional arrows. 

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] List<Sprite> foodIcons;
    [SerializeField] List<GameObject> arrows; 
    private List<GameObject> currentOrderQueue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // returns a new order with corresponding food icon, delivery location, and delivery timer



    public void GenerateNewOrder()
    {
        foreach(GameObject directionArrow in arrows)
        {
            Debug.Log("Creating new arrow icon");
            Sprite generatedIcon = foodIcons[Random.Range(0,foodIcons.Count)];
            Image foodIcon = directionArrow.GetComponentInChildren<Image>();
            foodIcon.sprite = generatedIcon;
            //currentOrderQueue.add(new order)
        }
      

    }

}

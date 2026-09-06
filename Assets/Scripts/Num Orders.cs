using UnityEngine;
using TMPro;
public class NumOrders : MonoBehaviour
{
    public arrow_manager arrowManager;
    // Update is called once per frame
    
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "" + arrowManager.passed_orders;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class order_manager : MonoBehaviour {
    public OrderableData[] types;

    [System.Serializable]
    public class OrderableData {
        public OrderType typ;
        public Sprite sprite;
    }

}

public enum OrderType {
    Pizza, Sandwich, StirfryNoodles, Soup
}
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField]
    private List<OrderZone> zones;

    [SerializeField]
    private ObjectManager objectManager;

    void Start()
    {
        int i = 0;
        foreach (var item in zones) 
        {
            item.objectManager = objectManager;
            RequestOrder(i,item);
            i++;
        }
    }

    public void RequestOrder(int id,OrderZone order) 
    {
        order.Setup(id,objectManager.GetSprite(id));
    }
}

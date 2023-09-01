
using System.Collections.Generic;
using UnityEngine;


public class InteractableObject : MonoBehaviour
{

    public string ItemName;
    public List<string> addList = new List<string>() { "Bicak", "Bant", "Sopa", "Ip", "Tas", "Kafa" };

    public string GetItemName()
    {

        return ItemName;
    }
    private void Start()
    {



        ForItems();
    }
    public void ForItems()
    {
        //InventorySystem.Instance.AddToInventory("Bicak");
        for (int i = 0; i < 4; i++)
        {



            foreach (var item in addList)
            {

                //InventorySystem.Instance.AddToInventory(item);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            InventorySystem.Instance.AddToInventory("Sopa");
        }


    }

}
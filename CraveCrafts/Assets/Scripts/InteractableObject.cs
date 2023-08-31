using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;
    public string ItemName;
    public List<string> addList = new List<string>() { "Bicak", "Bant", "Sopa", "Ip", "Tas","Kafa"};

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
        
        for (int i = 0; i < 4; i++)
        {
            foreach (var item in addList)
            {
                

                InventorySystem.Instance.AddToInventory(item);
            }
        }
    }

    void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.Mouse0) )
        {  

            if (!InventorySystem.Instance.CheckIfFull())
            {
                Debug.Log("interactable");
                InventorySystem.Instance.AddToInventory(ItemName);

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("inventory is full!");
            }
           
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; 
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

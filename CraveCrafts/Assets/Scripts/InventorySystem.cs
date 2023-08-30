using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public ControllerSO refreshConSO;
    public GameObject ItemInfoUI;

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public GameObject slotsGroupUI;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;
    
    public bool isOpen;

    //public bool isFull;

    //Pickup Popup



    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        

        isOpen = false;
        

        PopulateSlotList();
        
    }

    private void PopulateSlotList()
    {
        Debug.Log("PopulateSlotList");
        foreach(Transform child in slotsGroupUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if(Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);

            if (!CraftingSystem.Instance.isOpen)
            {
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            }

            
            
            isOpen = false;
        }
    }

    public void AddToInventory(string itemName)
    {




        Debug.Log(itemName +" addto");
        Debug.Log("AddToInventory");
        whatSlotToEquip = FindNextEmptySlot();

        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName),whatSlotToEquip.transform.position,whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);
            
        itemList.Add(itemName);
        Debug.Log(itemList);
        Debug.Log(itemList[0]);
        




        ReCalculateList();
        //refreshConSO.RefreshNeededItemsed();
        //CraftingSystem.Instance.RefreshNeededItems();


    }

    // envantere yeni eklenecek eþyanýn sýradaki boþ slotu bulmasýný saðlar
    private GameObject FindNextEmptySlot()
    {
        Debug.Log("FindNextEmptySlot");
        foreach(GameObject slot in slotList) 
        {
            if(slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    // envanterin dolu olup olmadýðýný hesaplar

    public bool CheckIfFull()
    {
        Debug.Log("CheckIfFull");
        int counter = 0;
        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                counter += 1;
            }
            
        }
        if (counter == 21)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // verilen isme karþýlýk gelen eþya envanterden silinr. envanter listesi yeniden hepsaplanýr. craftlama yapmak için gerekli olan eþyalar yeniden hesaplanýr
    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        Debug.Log("RemoveItem");
        int counter = amountToRemove;

        for (var i = slotList.Count - 1; i >= 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
                {
                    DestroyImmediate(slotList[i].transform.GetChild(0).gameObject);
                    counter -= 1;
                }
            }

        }
        ReCalculateList();
       // refreshConSO.RefreshNeededItemsed();
        //CraftingSystem.Instance.RefreshNeededItems();

    }
    

    public void ReCalculateList()
    {
        Debug.Log("ReCalculateList");
        itemList.Clear();

        foreach (GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name; //Stone (Clone)                
                string str2 = "(Clone)";
                string result = name.Replace(str2, "");

                itemList.Add(result);
            }
        }


    }
}

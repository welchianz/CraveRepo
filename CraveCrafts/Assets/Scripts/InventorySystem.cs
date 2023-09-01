
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public GameObject slotsGroupUI;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;
    
    public bool isOpen;

    public Dictionary<string,int> inventDictionarys = new Dictionary<string,int>();


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
        StartCoroutine(Dictio());
        
        

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
            //Debug.Log("i is pressed");
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
       
        Debug.Log("AddToInventory");
        whatSlotToEquip = FindNextEmptySlot();

        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName),whatSlotToEquip.transform.position,whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);
            
        itemList.Add(itemName);
       
        ReCalculateList();

    }

    // envantere yeni eklenecek e�yan�n s�radaki bo� slotu bulmas�n� sa�lar
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

    // envanterin dolu olup olmad���n� hesaplar

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

    // verilen isme kar��l�k gelen e�ya envanterden silinr. envanter listesi yeniden hepsaplan�r. craftlama yapmak i�in gerekli olan e�yalar yeniden hesaplan�r
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


    }
    
    // Envanter listesini s�f�rlar ve tekrardan envanter slotlar�nda bulunan itemleri listeye ekler
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
        StartCoroutine(Dictio());
        StartCoroutine(CraftingSystem.Instance.InteractButton());

    }

    //Envanter listesini s�zl��e d�n��t�r�r
    public IEnumerator Dictio()
    {     
        
        yield return new  WaitForSeconds(0.05f);
        inventDictionarys = TransInvenToDicti(itemList);
    }

    //Listedeki elemanlar� grupland�r�r ve s�zl��e �evirir. Envanter listesini s�zl��e �evirmek i�in kullan�ld�
    public Dictionary<string, int> TransInvenToDicti(List<string> firstList)
    {   
        
        Dictionary<string, int> dicti = new Dictionary<string, int>();
        var groupDic = firstList.GroupBy(first => first);
        foreach (var dic in groupDic)
        {
            dicti.Add(dic.Key, dic.Count());
           
        }
        return dicti;
    }
    
    //Craftlamas� yap�lmak istenen e�ya i�in gerekli itemler envanterde var m� diye bakar
    public bool HasEnoughItems(string itemName, int requiredCount)
    {
        
        if (inventDictionarys.ContainsKey(itemName) && inventDictionarys[itemName] >= requiredCount)
        {
            
            return true;
        }
        return false;
    }


}

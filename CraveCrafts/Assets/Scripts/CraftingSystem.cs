using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class CraftingSystem : MonoBehaviour
{
    //public ControllerSO refreshConSO2;

    
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;

    //ControllerSO sýnýfýndan myItemList listesini almak için
    //public ControllerSO myItemList1;

    // Craft yapabileceðimiz itemlerý saymak için
    public List<int> necessaryItemList1;

    // envanterdeki craft eþyasý sayýsý ile olmasý gereken sayýyý karþýlaþtýrmak içim
    //public Blueprints myItemCountList1;

    public List<string> inventoryItemList1 = new List<string>();
    public List<int> ItemListDiffer = new List<int>();
    //Category Buttons
    Button toolsBTN;

    //Craft Buttons
    Button craftAxeBTN;

    //Requirement Text
    Text AxeReq1, AxeReq2;

    public bool isOpen;

    //All Blueprint
    //public Blueprint AxeBLP = new Blueprint("Axe", 2, "Stone", 3, "Stick", 3);
    
    public static CraftingSystem Instance { get; set; }
    
    //public Blueprints myItemLists;

    public Button crfButtond;
    public Button crfButtonde;
    public GameObject panelMizrak11;
    public GameObject panelMizrak22;
    //public Blueprint AxeBLPD = new Blueprint("Axe", 2, "Stone", 3, "Stick", 3);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        //Debug.Log("AxeBLP: "+AxeBLP);
        isOpen = false;

        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });
        /*
        //AXE
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();
        Debug.Log(AxeReq1);
        Debug.Log(AxeReq2);
        craftAxeBTN = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        //craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(); });
        */

        crfButtond = panelMizrak11.transform.Find("CraftButton").GetComponent<Button>();
        crfButtond.onClick.AddListener(delegate { CraftAnyItem("Mizrak"); });



        crfButtonde = panelMizrak22.transform.Find("CraftButton").GetComponent<Button>();
        crfButtonde.onClick.AddListener(delegate { CraftAnyItem("TasMizrak"); });
    }

    void OpenToolsCategory()
    {
        Debug.Log("OpenToolsCategory");
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    public void CraftAnyItem(string ada)
    {   if(ada == "Mizrak")
        {
            InventorySystem.Instance.AddToInventory("Mizrak");
            InventorySystem.Instance.RemoveItem("Bant", 1);
            InventorySystem.Instance.RemoveItem("Sopa", 1);
            InventorySystem.Instance.RemoveItem("Bicak", 1);
        }
        Debug.Log("CraftAnyItem");
        //InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);
        if (ada == "TasMizrak")
        {
            InventorySystem.Instance.AddToInventory("TasMizrak");
            InventorySystem.Instance.RemoveItem("Tas", 1);
            InventorySystem.Instance.RemoveItem("Sopa", 1);
            InventorySystem.Instance.RemoveItem("Ip", 1);
            
        }
        

        StartCoroutine(calculate());
        //StartCoroutine(CraftPanelSystem.InstanceD.RefreshNeededItemsede());
    }

    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculateList();
       // RefreshNeededItems();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            
            craftingScreenUI.SetActive(true);
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);

            if (!InventorySystem.Instance.isOpen)
            {
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            }
                        
            isOpen = false;
        }
    }

    /*public void RefreshNeededItems()
    {
        //necessaryItemList1.Clear();
        int stone_count = 0;
        int stick_count = 0;
        //int repItemCount = 0; // tekrar eden item sayýyý sýfýrlanýr
        inventoryItemList1 = InventorySystem.Instance.itemList;
        //Debug.Log(inventoryItemList.Count);
       // foreach (string item in myItemList1.myItemLists)
        {   foreach (string itemName in inventoryItemList1) 
            {
 //               if (item == itemName) 
   //             {    
     //               repItemCount +=1;
       //         }
               
                switch (itemName)
                {
                    case "Stone":
                        Debug.Log("Stone listede");
                        stone_count += 1;
                        break;
                    case "Stick":
                        stick_count += 1;
                        Debug.Log("Stick listede");
                        break;

                }
               
            }
            //necessaryItemList1.Add(repItemCount);
            //Debug.Log(item + ":" + Convert.ToString(repItemCount));
        }
        
        //--AXE--//
        //AxeReq1.text = "3 Stone[" + stone_count + "]";
        AxeReq2.text = "3 Stick[" + stick_count + "]";
        
        if (myItemCountList1.myItemCountList == necessaryItemList1)
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);
        }
        
        if (stone_count >= 3 && stone_count >= 3)
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
           craftAxeBTN.gameObject.SetActive(false);
        }
    
    }*/

}

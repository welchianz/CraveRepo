using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class CraftingSystem : MonoBehaviour
{


    
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;


    // Craft yapabileceðimiz itemlerý saymak için
    public List<int> necessaryItemList1;


    //Category Buttons
    Button toolsBTN;


    public bool isOpen;
 
    public static CraftingSystem Instance { get; set; }
    
    //public Blueprints myItemLists;

    public Button crfButtond;
    public Button crfButtonde;
    public GameObject panelMizrak11;
    public GameObject panelMizrak22;

    public GameObject gameParent;
    public List<GameObject> gameChildList = new List<GameObject>();

    public Dictionary<string, int> sozluks;

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




        // þimdi
        //crfButtond = panelMizrak11.transform.Find("CraftButton").GetComponent<Button>();
        //crfButtond.onClick.AddListener(delegate { CraftAnyItem(panelMizrak11); });



        //crfButtonde = panelMizrak22.transform.Find("CraftButton").GetComponent<Button>();
        //crfButtonde.onClick.AddListener(delegate { CraftAnyItem(panelMizrak22); });
        GetChildList();
    }

    public void GetChildList()
    {
        // Sahnedeki "Parent" adlý objeyi bulun

        for (int i = 0; i < gameParent.transform.childCount; i++) // Parent'ýn altýndaki tüm child'larý döngü ile gezin
        {
            GameObject childs = gameParent.transform.GetChild(i).gameObject; // i. indeksteki child'ý alýn
            gameChildList.Add(childs); // Child'ý listeye ekleyin

            Debug.Log("GetChildList:  " + gameChildList[i].ToString());
            Debug.Log("GetChildListCount:  " + gameChildList.Count);
            Button crftsButtons = gameChildList[i].transform.Find("CraftButton").GetComponent<Button>();
            crftsButtons.onClick.AddListener(delegate {
            for (int a = 0; a < gameParent.transform.childCount; a++) // Parent'ýn altýndaki tüm child'larý döngü ile gezin
            { CraftAnyItem(gameChildList[a]); Debug.Log(gameChildList[a].ToString() + "GETchildin"); }
            });
            
        }

    }

    void OpenToolsCategory()
    {
        Debug.Log("OpenToolsCategory");
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    public Dictionary<string, int> TranslatesToDictionary(List<string> firstList)
    {
        Dictionary<string, int> dictio = new Dictionary<string, int>();
        foreach (var item in firstList)
        {
            string[] splitValues = item.Split(',');
            dictio.Add(splitValues[0], Convert.ToInt32(splitValues[1]));
        }

        return dictio;
    }

    public void CraftAnyItem(GameObject ada)

    {
        
        sozluks = TranslatesToDictionary(ada.GetComponent<ControllerSO>().blueprints.myItemList);

        
        InventorySystem.Instance.AddToInventory(ada.GetComponent<ControllerSO>().blueprints.prefabPanelName);
                
        foreach (var item in sozluks)
        {
            InventorySystem.Instance.RemoveItem(item.Key, item.Value);
        }
                
                
            
      

        /*
        if (ada == "Mizrak")
        {
            InventorySystem.Instance.AddToInventory("Mizrak");
            InventorySystem.Instance.RemoveItem("Bant", 1);
            InventorySystem.Instance.RemoveItem("Sopa", 1);
            InventorySystem.Instance.RemoveItem("Bicak", 1);
        }
        Debug.Log("CraftAnyItem");

        if (ada == "TasMizrak")
        {
            InventorySystem.Instance.AddToInventory("TasMizrak");
            InventorySystem.Instance.RemoveItem("Tas", 1);
            InventorySystem.Instance.RemoveItem("Sopa", 1);
            InventorySystem.Instance.RemoveItem("Ip", 1);
            
        }
        */

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

   

}

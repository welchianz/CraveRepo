using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;


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
    public List<GameObject> gameChildList2 = new List<GameObject>();

    public Dictionary<string, int> sozluks;
    public Dictionary<string, int> sozluks2;
    public Dictionary<string, int> sozluks3;

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
        isOpen = false;

        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });
        /*
        //AXE
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();

        craftAxeBTN = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        //craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(); });
        */

        // þimdi
        //crfButtond = panelMizrak11.transform.Find("CraftButton").GetComponent<Button>();
        //crfButtond.onClick.AddListener(delegate { CraftAnyItem(panelMizrak11); });



        //crfButtonde = panelMizrak22.transform.Find("CraftButton").GetComponent<Button>();
        //crfButtonde.onClick.AddListener(delegate { CraftAnyItem(panelMizrak22); });
       StartCoroutine( GetChildList());
    }

    public IEnumerator GetChildList()
    {
        yield return new WaitForSeconds(0.1f);
        // Sahnedeki "Parent" adlý objeyi bulun

        for (int i = 0; i < gameParent.transform.childCount; i++) // Parent'ýn altýndaki tüm child'larý döngü ile gezin
        {
            GameObject childs = gameParent.transform.GetChild(i).gameObject; // i. indeksteki child'ý alýn
            gameChildList.Add(childs); // Child'ý listeye ekleyin

           // Debug.Log("GetChildList:  " + gameChildList[i].ToString());
           // Debug.Log("GetChildListCount:  " + gameChildList.Count);
            Button crftsButtons = gameChildList[i].transform.Find("CraftButton").GetComponent<Button>();
            int index = i;
            
            crftsButtons.onClick.AddListener(delegate {
                
                CraftAnyItem(gameChildList[index]); Debug.Log(gameChildList[index].ToString() + "GETchildin");
                
            });
            crftsButtons.interactable = CheckIfCraftable(childs);
        }
    }
    public IEnumerator InteractButton()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < gameParent.transform.childCount; i++)
        {
            GameObject childs = gameParent.transform.GetChild(i).gameObject; // i. indeksteki child'ý alýn
            gameChildList2.Add(childs);
            Button crftsButtons = gameChildList2[i].transform.Find("CraftButton").GetComponent<Button>();
            crftsButtons.interactable = CheckIfCraftable(gameChildList2[i]);
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

        StartCoroutine(calculate());
       
    }

    public bool CheckIfCraftable(GameObject gameObj)
    {
        
        List<string> itemList = gameObj.GetComponent<ControllerSO>().blueprints.myItemList;
        
        foreach (var item in itemList)
        {
            string[] splitValues = item.Split(',');
            string itemName = splitValues[0];
            int itemCount = Convert.ToInt32(splitValues[1]);
        
            if (!InventorySystem.Instance.HasEnoughItems(itemName, itemCount))
            {   Debug.Log(InventorySystem.Instance.HasEnoughItems(itemName, itemCount));
                return false;
            }
        }
        return true;
    }

public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculateList();
       // RefreshNeededItems();
        
        
    }

  
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

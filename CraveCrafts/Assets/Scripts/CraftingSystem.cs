using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingSystem : MonoBehaviour
{

    public GameObject toolsScreenUI;

    public bool isOpen;
 
    public static CraftingSystem Instance { get; set; }
    
    public GameObject gameParent;
    public List<GameObject> gameChildList = new List<GameObject>();
    public List<GameObject> gameChildList2 = new List<GameObject>();

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
        isOpen = false;

        
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


        StartCoroutine(AddTimer(ada));


        foreach (var item in sozluks)
        {
            InventorySystem.Instance.RemoveItem(item.Key, item.Value);
        }

        calculate();
       
    }

    public IEnumerator AddTimer(GameObject ada)
    {
        yield return new WaitForSeconds(0.1f);
        InventorySystem.Instance.AddToInventory(ada.GetComponent<ControllerSO>().blueprints.prefabPanelName);
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

    public void calculate()
        {
        
            InventorySystem.Instance.ReCalculateList();
     
        }

  
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {

            toolsScreenUI.SetActive(true);
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            
            toolsScreenUI.SetActive(false);

            if (!InventorySystem.Instance.isOpen)
            {
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            }
                        
            isOpen = false;
        }
    }

   

}

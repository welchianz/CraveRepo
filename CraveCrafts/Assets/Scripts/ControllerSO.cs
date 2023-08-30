using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSO : MonoBehaviour
{
    public Blueprints blueprints;
    public List<string> myItemLists;
    public GameObject toolsPanelUI;
    public Text itemDescription;
    public Text craftTitle;
    public Image craftImage;
    public List<string> necessaryItemList;
    public List<string> inventoryItemList = new List<string>();
    public Button craftBTN;
    public List<int> itemCountLists;
    public int a = 0;
    public Dictionary<string,int> inventoryItemDic;
    public Dictionary<string, int> craftItemDic;


    public Dictionary<string, int> itemDictionary;

    void Start()
    {
        itemCountLists = blueprints.myItemCountList;
        myItemLists = blueprints.myItemList;
        craftTitle = toolsPanelUI.transform.Find(blueprints.prefabPanelName).transform.Find(blueprints.titlePanelName).GetComponent<Text>();
        craftTitle.text = blueprints.itemName;
        //Instantiate(CraftPanel, new Vector3(0,0,0), Quaternion.identity);
        itemDescription = toolsPanelUI.transform.Find(blueprints.prefabPanelName).transform.Find(blueprints.descrPanelName).GetComponent<Text>();
        itemDescription.text = blueprints.itemCraftDescr.Replace("düs", "\n");

        craftBTN = toolsPanelUI.transform.Find(blueprints.prefabPanelName).transform.Find(blueprints.buttonPanelName).GetComponent<Button>();      

        craftImage = toolsPanelUI.transform.Find(blueprints.prefabPanelName).transform.Find(blueprints.imagePanelName).GetComponent<Image>();
        craftImage.sprite = blueprints.itemSprite;


        
    }

    public IEnumerator TranslateToDictionary()
    {
        yield return new WaitForSeconds(1f);
      

        /*
        inventoryItemList = InventorySystem.Instance.itemList;
       
        var groups = inventoryItemList.GroupBy(inventory => inventory);
        foreach (var item in myItemLists)
        {
            string[] splitValues = item.Split(',');

            craftItemDic.Add(splitValues[0], Convert.ToInt32(splitValues[1]));
            
        }
        foreach (var group in groups)
        {
            inventoryItemDic.Add(group.Key, group.Count());
                      
        }
        foreach (var group in inventoryItemDic)
        {
            Debug.Log(group.Key + " intdic " + Convert.ToString(group.Value));

        } */
    }

    public void RefreshNeededItemsed()
    {
       // Debug.Log("refresh açýldý");
        /*  necessaryItemList.Clear();
          int repItemCount = 0; // tekrar eden item sayýyý sýfýrlanýr
          inventoryItemList = InventorySystem.Instance.itemList;
          Debug.Log(inventoryItemList.Count);
          foreach (string item in myItemLists)
          {
              foreach (string itemName in inventoryItemList)
              {
                  if (item == itemName)
                  {
                      repItemCount += 1;
                  }


              }
              necessaryItemList.Add(repItemCount);
              Debug.Log(item + ":" + Convert.ToString(repItemCount));


              if (blueprints.myItemCountList == necessaryItemList)
              {
                  craftBTN.gameObject.SetActive(true);
              }
              else
              {
                  craftBTN.gameObject.SetActive(false);
              }

    }*/
    }



}

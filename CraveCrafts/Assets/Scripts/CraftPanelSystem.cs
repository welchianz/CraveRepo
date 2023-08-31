using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;




public class CraftPanelSystem : MonoBehaviour
{
    public static CraftPanelSystem InstanceD { get; set; }
    public GameObject panelMizrak;

    public string myMizrakItemName;
    public List<string> myMizrakNeededList;

    public List<string> inventoryItemList = new List<string>();

    public Dictionary<string, int> sozluk;
    public Dictionary<string, int> sozluk2;

    public Button crfButton;

    
    public List<GameObject> childList = new List<GameObject>();
    public GameObject parent;
    void Start()
    {
         parent =  GameObject.Find("ToolsPanel");
        

        //crfButton = panelMizrak.transform.Find("CraftButton").GetComponent<Button>();

        inventoryItemList = InventorySystem.Instance.itemList;
        myMizrakItemName = panelMizrak.GetComponent<ControllerSO>().blueprints.itemName;
        
        myMizrakNeededList = panelMizrak.GetComponent<ControllerSO>().blueprints.myItemList;
       
        ObjectList();

     
        

    }
    //Craft Panel gameobjectlerini bir listede tutar
    public void ObjectList()
    {
         // Sahnedeki "Parent" adlý objeyi bulun

        for (int i = 0; i < parent.transform.childCount; i++) // Parent'ýn altýndaki tüm child'larý döngü ile gezin
        {
            GameObject child = parent.transform.GetChild(i).gameObject; // i. indeksteki child'ý alýn
            childList.Add(child); // Child'ý listeye ekleyin
                                  
            StartCoroutine(RefreshNeededItemsed(childList[i]));
            Button crftButtons = childList[i].transform.Find("CraftButton").GetComponent<Button>();
        }
       
    }
    public IEnumerator RefreshNeededItemsed(GameObject object1)
    { 
        yield return new WaitForSeconds(0.2f);
        List<string> iteList = object1.GetComponent<ControllerSO>().blueprints.myItemList;
        Button crfButtons = object1.transform.Find("CraftButton").GetComponent<Button>();

        crfButtons.onClick.AddListener(delegate {
            StartCoroutine(RefreshNeededItemsed(object1));
        });

        sozluk = TranslateToDictionary(iteList);
        int b = 0;
        
        sozluk2 = TransInvenToDict(inventoryItemList);
        foreach (var pair in sozluk)
        {
            // Eðer ikinci sözlükte ayný key varsa
            if (sozluk2.ContainsKey(pair.Key))
            {
                // Eðer ikinci sözlükteki value, birinci sözlüktekinden eþit veya büyükse
                if (sozluk2[pair.Key] >= pair.Value)
                {
                    b++;
                    // Ekrana "tamam" yazdýr
                    
                }
            }
            if(sozluk.Count == b)
            {
                crfButtons.gameObject.SetActive(true);
            }
            else
            {
                crfButtons.gameObject.SetActive(false);
            }
        }



    }


    //Bir nesnenin craftlanmasý için gerekli olan listeyi sözlüðe çevirir
    public Dictionary<string, int> TranslateToDictionary(List<string> firstList)
    {
        Dictionary<string, int> dicti = new Dictionary<string, int>();
        foreach (var item in firstList)
        {
            string[] splitValues = item.Split(',');
            dicti.Add(splitValues[0], Convert.ToInt32(splitValues[1]));
        }

                return dicti;
    }

    //Envanterde bulunan item listesini sözlüðe çevirir
    public Dictionary<string, int> TransInvenToDict(List<string> firstList)
    {
        Dictionary<string, int> dicti = new Dictionary<string, int>();
        var groupDic = firstList.GroupBy(first => first);
        foreach (var dic in groupDic)
        {
            dicti.Add(dic.Key, dic.Count());
        }
        return dicti;
    }




}

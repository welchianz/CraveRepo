using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSO : MonoBehaviour
{
    public Blueprints blueprints;

    public GameObject toolsPanelUI;
    public Text itemDescription;
    public Text craftTitle;
    public Image craftImage;
    public List<string> necessaryItemList;
    public List<string> inventoryItemList = new List<string>();


    void Start()
    {
        
        
        craftTitle = toolsPanelUI.transform.Find(blueprints.prefabPanelName).transform.Find(blueprints.titlePanelName).GetComponent<Text>();
        craftTitle.text = blueprints.itemName;
        itemDescription = toolsPanelUI.transform.Find(blueprints.prefabPanelName).transform.Find(blueprints.descrPanelName).GetComponent<Text>();
        itemDescription.text = blueprints.itemCraftDescr.Replace("düs", "\n");

     

        craftImage = toolsPanelUI.transform.Find(blueprints.prefabPanelName).transform.Find(blueprints.imagePanelName).GetComponent<Image>();
        craftImage.sprite = blueprints.itemSprite;


        
    }

}

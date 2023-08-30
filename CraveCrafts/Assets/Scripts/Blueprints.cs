using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Blueprint", menuName = "ScriptableObject/Blueprint")]
public class Blueprints : ScriptableObject
{   // itemin ad� ve sprite'�
    public Sprite itemSprite;
    public string itemName;

    //craft a��klamas�
    public string itemCraftDescr;

    // craftlamak i�in gereken itemlerin isimleri
    public List<string> myItemList;
    
        
    // craftlamak i�in gereken itemlerin say�s�
    public List<int> myItemCountList;



    
    //Paneldeki image ve textleri getcomponent ile bulmak i�in(ControllerSO)
    public string prefabPanelName;
    public string imagePanelName;
    public string titlePanelName;
    public string descrPanelName;
    public string buttonPanelName;





}

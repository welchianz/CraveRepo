using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Blueprint", menuName = "ScriptableObject/Blueprint")]
public class Blueprints : ScriptableObject
{   // itemin adý ve sprite'ý
    public Sprite itemSprite;
    public string itemName;

    //craft açýklamasý
    public string itemCraftDescr;

    // craftlamak için gereken itemlerin isimleri
    public List<string> myItemList;
    
        
    // craftlamak için gereken itemlerin sayýsý
    public List<int> myItemCountList;



    
    //Paneldeki image ve textleri getcomponent ile bulmak için(ControllerSO)
    public string prefabPanelName;
    public string imagePanelName;
    public string titlePanelName;
    public string descrPanelName;
    public string buttonPanelName;





}

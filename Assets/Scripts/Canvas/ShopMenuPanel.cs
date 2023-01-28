using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ShopMenuPanel : MonoBehaviour
{
    RectTransform MyRect;
    [SerializeField] Ease ease;

    [SerializeField] Text slot1Text,slot2Text,slot3Text,coinText;
    [SerializeField] Text slot1LevelText,slot2LevelText,slot3LevelText;
    CoinPanelController coinPanelController;
    Transform PlayerPos;
    [SerializeField] GameObject BuyEffect;
    // Start is called before the first frame update
    void Start()
    {
        MyRect = GetComponent<RectTransform>();
        MyRect.localScale = Vector3.zero;
        MyRect.DOScale(Vector3.one,1f).SetEase(ease).SetDelay(0.5f);
        coinPanelController = GameObject.FindObjectOfType<CoinPanelController>();

        PlayerPrefs.SetInt("Slot1",100);
        PlayerPrefs.SetInt("Slot2",100);
        PlayerPrefs.SetInt("Slot3",100);

        PlayerPrefs.SetFloat("Slot1Value",1);
        PlayerPrefs.SetFloat("Slot2Value",1);
        PlayerPrefs.SetFloat("Slot3Value",1);

        PlayerPrefs.SetInt("Slot1Level",1);
        PlayerPrefs.SetInt("Slot2Level",1);
        PlayerPrefs.SetInt("Slot3Level",1);
        
        slot1Text.text      = PlayerPrefs.GetInt("Slot1").ToString();
        slot2Text.text      = PlayerPrefs.GetInt("Slot2").ToString();
        slot3Text.text      = PlayerPrefs.GetInt("Slot3").ToString();
        slot1LevelText.text = "Level " + PlayerPrefs.GetInt("Slot1Level").ToString();
        slot2LevelText.text = "Level " + PlayerPrefs.GetInt("Slot2Level").ToString();
        slot3LevelText.text = "Level " + PlayerPrefs.GetInt("Slot3Level").ToString();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }

    public void DeActiveFunction(){
        MyRect.DOScale(Vector3.zero,0.66f).OnComplete( () => {
            Destroy(this.gameObject);
        });
    }

    public void OnFunction(RectTransform slotRect){
        slotRect.localScale = Vector3.one * 0.90f;
        string slotName = slotRect.gameObject.name;
        int coin = PlayerPrefs.GetInt("Coin");
        int price;
        int level;
        float value;
        switch(slotName){
            case "Slot1":
                price = PlayerPrefs.GetInt(slotName);
                value = PlayerPrefs.GetFloat(slotName+"Value");
                level = PlayerPrefs.GetInt(slotName.ToString() + "Level");
                if(coin >= PlayerPrefs.GetInt(slotName)){
                    coin -= price;
                    PlayerPrefs.SetInt("Coin",coin);
                    price *= 2;
                    PlayerPrefs.SetInt(slotName,price);
                    value *= 1.1f;
                    PlayerPrefs.SetFloat(slotName+"Value",value);
                    coinText.text = coin.ToString();
                    slot1Text.text = price.ToString();
                    coinPanelController.Coin = coin;
                    level++;
                    PlayerPrefs.SetInt(slotName.ToString() + "Level",level);
                    Instantiate(BuyEffect,PlayerPos.position,Quaternion.identity);
                }
                slot1LevelText.text = "Level " + PlayerPrefs.GetInt(slotName.ToString() + "Level").ToString();
            break;
            case "Slot2":
                price = PlayerPrefs.GetInt(slotName);
                value = PlayerPrefs.GetFloat(slotName+"Value");
                level = PlayerPrefs.GetInt(slotName.ToString() + "Level");
                if(coin >= PlayerPrefs.GetInt(slotName)){
                    coin -= price;
                    PlayerPrefs.SetInt("Coin",coin);
                    price *= 2;
                    PlayerPrefs.SetInt(slotName,price);
                    value *= 1.1f;
                    PlayerPrefs.SetFloat(slotName+"Value",value);
                    coinText.text = coin.ToString();
                    slot2Text.text = price.ToString();
                    coinPanelController.Coin = coin;
                    level++;
                    PlayerPrefs.SetInt(slotName.ToString() + "Level",level);
                    Instantiate(BuyEffect,PlayerPos.position,Quaternion.identity);
                }
                slot2LevelText.text = "Level " + PlayerPrefs.GetInt(slotName.ToString() + "Level").ToString();

            break;
            case "Slot3":
                price = PlayerPrefs.GetInt(slotName);
                value = PlayerPrefs.GetFloat(slotName+"Value");
                level = PlayerPrefs.GetInt(slotName.ToString() + "Level");
                if(coin >= PlayerPrefs.GetInt(slotName)){
                    coin -= price;
                    PlayerPrefs.SetInt("Coin",coin);
                    price *= 2;
                    PlayerPrefs.SetInt(slotName,price);
                    value *= 1.1f;
                    PlayerPrefs.SetFloat(slotName+"Value",value);
                    coinText.text = coin.ToString();
                    slot3Text.text = price.ToString();
                    coinPanelController.Coin = coin;
                    level++;
                    PlayerPrefs.SetInt(slotName.ToString() + "Level",level);
                    Instantiate(BuyEffect,PlayerPos.position,Quaternion.identity);
                }
                slot3LevelText.text = "Level " + PlayerPrefs.GetInt(slotName.ToString() + "Level").ToString();
            break;
        }
    }

    public void UpFunction(RectTransform slotRect){
        slotRect.localScale = Vector3.one;
    }
}

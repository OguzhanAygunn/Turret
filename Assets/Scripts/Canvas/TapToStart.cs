using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TapToStart : MonoBehaviour
{
    [SerializeField] RectTransform textRect;
    Text text;
    [SerializeField] Image background;
    [SerializeField] GameObject joystick;
    bool scaleUp;
    ShopMenuPanel shopMenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        joystick.SetActive(false);
        textRectScaleFunc();
        text = textRect.gameObject.GetComponent<Text>();
        shopMenuPanel = GameObject.FindObjectOfType<ShopMenuPanel>();
    }

    void textRectScaleFunc(){
        textRect.DOScale(Vector3.one*1.2f,0.5f).OnComplete( () =>{
            textRect.DOScale(Vector3.one,0.5f).OnComplete( () => {
                textRectScaleFunc();
            });
        });
    }

    public void onTouchFunction(){
        GameManager.GameStart = true;
        joystick.SetActive(true);
        text.DOColor(Color.clear,0.5f);
        background.DOColor(Color.clear,0.5f).OnComplete( () => {
            Destroy(this.gameObject);
        });
        shopMenuPanel.DeActiveFunction();
    }
}

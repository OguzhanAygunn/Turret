using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSliderPanelController : MonoBehaviour
{
    RectTransform myRect;
    [SerializeField] Ease scaleEase;
    Slider slider;
    Transform playerPos,finishPos;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        myRect = GetComponent<RectTransform>();
        //myRect.DOAnchorPos(targetPos,time).SetDelay(delay);
        myRect.localScale = Vector2.zero;
        myRect.DOScale(Vector2.one,3).SetEase(scaleEase).SetDelay(0.66f);
        slider = transform.GetChild(0).gameObject.GetComponent<Slider>();
        playerPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        finishPos = GameObject.FindGameObjectWithTag("FinishGround").gameObject.transform;
        distance = finishPos.position.z - playerPos.position.z;
        slider.maxValue = distance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sliderOperations();
    }

    void sliderOperations(){
        slider.value = distance - (finishPos.position.z - playerPos.position.z);
    }
}

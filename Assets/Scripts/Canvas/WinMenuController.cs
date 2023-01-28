using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class WinMenuController : MonoBehaviour
{
    [SerializeField] RectTransform RewardButtonRect,DefaultButtonRect,flag,winRect;
    [SerializeField] Ease ButtonEase;
    [SerializeField] Image Background;
    [SerializeField] Color TargetBackgroundColor;
    [SerializeField] Text rewardButtonText,defaultButtonText;
    [SerializeField] float delay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActiveFunction(){
        Background.DOColor(TargetBackgroundColor,0.5f).SetDelay(delay).OnComplete( () => {
            RewardButtonRect.DOScale(Vector3.one,1.1f).SetEase(ButtonEase);
            DefaultButtonRect.DOScale(Vector3.one,1f).SetEase(ButtonEase);
            flag.DOScale(Vector3.one,1f).SetEase(ButtonEase);
            winRect.DOScale(Vector3.one,1f).SetEase(ButtonEase);
        });
        defaultButtonText.text = GameManager.levelCoinScore.ToString();
        rewardButtonText.text = (GameManager.levelCoinScore*2).ToString();
    }

    public void nextLevelFunc(){
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        levelIndex++;
        if(levelIndex >= 5)
            levelIndex = 0;
        SceneManager.LoadScene(levelIndex);
    }
}

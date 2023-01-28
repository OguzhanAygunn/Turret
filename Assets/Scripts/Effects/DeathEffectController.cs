using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DeathEffectController : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] sprites;
    [SerializeField] Color targetColor;
    [SerializeField] float time,delay;
    Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = sprites[0].color;
    }

    public void EffectActive(bool death){
        if(death){
            DOTween.Kill(gameObject);
            foreach(SpriteRenderer sprite in sprites){
                sprite.DOColor(targetColor,time).OnComplete( () => {
                    sprite.DOColor(defaultColor,time).SetDelay(delay);
                });
            }
        }
        else{
            if(!GameManager.GameLose){
                foreach(SpriteRenderer sprite in sprites){
                    sprite.DOColor(targetColor,time/2).OnComplete( () => {
                        sprite.DOColor(defaultColor,time/2);
                    });
                }
            }
        }
    }
}

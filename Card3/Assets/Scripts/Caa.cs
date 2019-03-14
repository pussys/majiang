using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caa : MonoBehaviour {

    Tween HeroTween;
	// Use this for initialization
    public RectTransform HeroCavas;//英雄属性面板
	void Start () 
    {
		 HeroCav();
	
	}
	
	// Update is called once per frame
    void Update()
    {
    } 
 
    public void Come()
    {             
       HeroTween.PlayForward();                          
    }

    public void Go()
    {
        HeroTween.PlayBackwards();
    }
    private void HeroCav()
    {
        HeroTween = HeroCavas.DOLocalMoveX(0, 1).SetEase(Ease.InOutBack).SetLoops(1);
        HeroTween.SetAutoKill(false);
        HeroTween.Pause();
    }
}

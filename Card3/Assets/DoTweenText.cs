using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTweenText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.DOMoveX(500, 1)
            .SetRelative()
            .OnComplete(
            () => transform.DOMoveY(200, 1)
            .SetRelative()
            .OnComplete(
                () => transform.DOMoveX(-300, 1)
                .SetDelay(1)
                )
                );
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}

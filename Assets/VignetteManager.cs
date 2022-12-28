using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class VignetteManager : MonoBehaviour
{
    const float FULL_ALPHA = 1.0f;
    //Settings
    public float fadeInOutDuration;
    // Connections
    public Image vignetteImage;
    Sequence vignetteAlarmSequence;
    // State Variables
    
    // Start is called before the first frame update
    void Start()
    {
        InitConnections();
        //InitState();
    }
    void InitConnections(){
        vignetteAlarmSequence = DOTween.Sequence();
        Tween fadeInTween = vignetteImage.DOFade(FULL_ALPHA, fadeInOutDuration).SetEase(Ease.Linear);
        Tween fadeOutTween = vignetteImage.DOFade(0, fadeInOutDuration);
        vignetteAlarmSequence.Append(fadeInTween);
        vignetteAlarmSequence.Append(fadeOutTween);
        vignetteAlarmSequence.SetLoops(-1);
        vignetteAlarmSequence.Pause();
    }
    void InitState(){
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void StartAlarm()
    {
        vignetteAlarmSequence.Play();
    }

    public void StopAlarm()
    {
        vignetteAlarmSequence.Goto(0);
    }

    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CheaterUnlocker : MonoBehaviour
{
    //Settings
    public int currentImageIndex = 0;
    public float unlockTime = 0.5f;
    float startingPercent = 0;
    float targetPercent = 0;
    float currentPercent = 0;
    // Connections
    public GameObject[] images;
    public TMP_Text percentageText;
    // State Variables
    bool canChange = false;
    float passedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        InitConnections();
        InitState();
    }
    void InitConnections(){
        if (PlayerPrefs.GetInt("isBonusLevel", 0) == 0)
            currentImageIndex = PlayerPrefs.GetInt("currentImageIndex", 0);
    }
    void InitState(){
        if(PlayerPrefs.GetInt("isBonusLevel", 0) == 0)
            images[currentImageIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (canChange)
        {
            ChangeImage();
            passedTime += Time.deltaTime * 2;
        }
        
    }

    public void SetUnlockPercent(float percent)
    {
        startingPercent = PlayerPrefs.GetFloat("cheaterUnlockPercent", 0);
        
        targetPercent = startingPercent + percent;
        canChange = true;
        if(targetPercent >= 100)
        {
            PlayerPrefs.SetFloat("cheaterUnlockPercent", 0);
            PlayerPrefs.SetInt("isBonusLevel", 1);
            Invoke(nameof(changeImageIndex), unlockTime);
        }
        else
        {
            PlayerPrefs.SetFloat("cheaterUnlockPercent", targetPercent);
        }
    }

    void ChangeImage()
    {
        currentPercent = Vector3.Lerp(new Vector3(0, 0, startingPercent), new Vector3(0, 0, targetPercent), passedTime).z;
        int integerCurrent = Mathf.RoundToInt(currentPercent);
        percentageText.text = "%" + integerCurrent;
        currentPercent *= 0.01f;
        images[currentImageIndex].GetComponent<Image>().fillAmount = currentPercent;
    }   
    public void changeImageIndex()
    {
        currentImageIndex += 1;
        currentImageIndex = currentImageIndex % images.Length;
        PlayerPrefs.SetInt("currentImageIndex", currentImageIndex);
    }

}

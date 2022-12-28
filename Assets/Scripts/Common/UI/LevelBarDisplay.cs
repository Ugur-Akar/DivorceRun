using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelBarDisplay : MonoBehaviour
{

	#region Attributes
	// Enter your attributes which are used to specify this single instance of this class
	#endregion
	#region Connections
	public Image fillingImage;
	public TextMeshProUGUI levelText;
	public Transform startPoint;
	public Transform endPoint;

	public Transform runnerWoman;
	public Transform runnerMan;
	#endregion
	#region State Variables
		// Enter your state variables used to store data in your algorithm
	#endregion
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void DisplayProgress(float progress)
    {
		fillingImage.fillAmount = progress;
		runnerWoman.position = Vector3.Lerp(startPoint.position, endPoint.position, progress);
	}

	public void DisplayEnemyProgress(float progress)
    {
		// Set enemy progress
		runnerMan.position = Vector3.Lerp(startPoint.position, endPoint.position, progress);
	}

	

	public void SetLevel(int levelIndex)
    {
		levelText.text = "" + (levelIndex+1);

	}

	#region Init Functions
	void InitState(){
		// Ali veli hasan hüseyin
	}
	void InitConnections(){
	}
	
	#endregion
	
	
}

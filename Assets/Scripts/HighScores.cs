using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    public int number;

    private void Awake() 
    {
        switch(number)
        {
            case 1:
                gameObject.GetComponent<Text>().text = "HIGHSCORE \n" + HighScoreManager.LVL1_HIGHSCORE.ToString();
                break;
            case 2:
                gameObject.GetComponent<Text>().text = "HIGHSCORE \n" + HighScoreManager.LVL2_HIGHSCORE.ToString();
                break;
            case 3:
                gameObject.GetComponent<Text>().text = "HIGHSCORE \n" + HighScoreManager.LVL3_HIGHSCORE.ToString();
                break;
        }
    }
}

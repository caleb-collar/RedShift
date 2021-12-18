using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetFinalScore : MonoBehaviour
{
    private Level thisLevel;
    [SerializeField] private TMP_Text[] scoreTexts;
    void Start()
    {
        thisLevel = GameObject.Find("Level").GetComponent<Level>();
        for (int i=0; i<scoreTexts.Length; i++)
        {
            scoreTexts[i].text = thisLevel.GetScore().ToString();
        }
    }
    
}

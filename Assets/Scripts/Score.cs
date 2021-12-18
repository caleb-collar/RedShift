using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private void Start()
    {
        //this.scoreText.text = Level.instance.score.ToString();
    }
    
}

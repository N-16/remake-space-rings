using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour{
    [SerializeField] private Text text;
    
    public void SetText(string text){
        Debug.Log("Setting text");
        this.text.text = text;
    }
}

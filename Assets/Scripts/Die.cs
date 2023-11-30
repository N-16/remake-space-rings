using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Die : MonoBehaviour
{
    [SerializeField] private GameObject dieUI,pause;

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            dieUI.SetActive(true);
            pause.SetActive(false);
        }
    }
    bool Wait(float time, float currentTime)
    {
        currentTime += Time.deltaTime;
        if (currentTime > time)
            return true;
        else return false;
    }
}

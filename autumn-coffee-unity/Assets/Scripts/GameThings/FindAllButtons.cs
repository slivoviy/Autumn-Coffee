using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindAllButtons : MonoBehaviour
{
    void Start()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() => AudioManager.instance.ButtonClick());
        }
    }
    
}

using UnityEngine;
using TMPro;
using System;
using System.Reflection.Emit;

public class DashIconBehavior : MonoBehaviour
{
     TextMeshProUGUI tmp;
     PinBehavior cooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>(); 
        // float cooldown;
        // float cooldownRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(PinBehavior.cooldown > 0.0f){
            String message = string.Format("0:0:0", PinBehavior.cooldown);

            // label.SetText(message);
            tmp.text = message;
    }
        }
        
}

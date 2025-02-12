using UnityEngine;
using TMPro;
using System;
using System.Reflection.Emit;
using UnityEngine.UI;

public class DashIconBehavior : MonoBehaviour
{
    //  TextMeshProUGUI tmp;
     public TextMeshProUGUI label;
    float fill;
     public Image overlay;
    //  PinBehavior cooldown;
    public float  cooldownRate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // tmp = GetComponent<TextMeshProUGUI>(); 
        label = GetComponent<TextMeshProUGUI>();
        Image[] images = GetComponentsInChildren<Image>();
        for(int i  =0; i < images.Length; i++){
            if(images[i].tag == "Overlay"){
                overlay = images[i];
            }

        }

        cooldownRate = PinBehavior.cooldownRate;
        overlay.fillAmount =0.0f;
        // float cooldown;
        // float cooldownRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(PinBehavior.cooldown > 0.0f){
            
            float cooldown = PinBehavior.cooldown;
            float cooldownRate = PinBehavior.cooldownRate;
            String message = "";

            message = string.Format("0:0:0", PinBehavior.cooldown);

            if(cooldown > 0.0){
                float fill = cooldown / cooldownRate;
            }

            label.SetText(message);
            overlay.fillAmount = fill;
            // tmp.text = message;
    }
        }
        
}

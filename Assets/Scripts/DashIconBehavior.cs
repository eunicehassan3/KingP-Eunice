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
    float  cooldownRate;
    float cooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();

        Image[] images = GetComponentsInChildren<Image>();
        for(int i  = 0; i < images.Length; i++){
            if(images[i].tag == "Overlay"){
                overlay = images[i];
            }

        }

        cooldownRate = PinBehavior2.cooldownRate;
        overlay.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("cooldown Rate:" + cooldownRate);
        if(PinBehavior2.cooldown > 0.0f){
            cooldown = PinBehavior2.cooldown;
            string message = "";

            if(cooldown > 0.0){
                
                float fill = cooldown / cooldownRate;
                message = string.Format("{0:0.0}", cooldown);
                overlay.fillAmount = fill;
            }else{
                cooldown = 0.0f;
            }
            label.text = message;
        }       
    }
        
}

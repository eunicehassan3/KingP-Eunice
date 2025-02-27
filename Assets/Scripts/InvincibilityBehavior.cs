using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InvincibilityBehavior : MonoBehaviour
{
    //  TextMeshProUGUI tmp;
    public TextMeshProUGUI label;
     
    float fill;
     public Image overlay;
    float  invinciblityDuration;
    float invincibilityTime;
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

        invinciblityDuration = PinBehavior2.invincibilityDuration;
        overlay.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(PinBehavior2.invincibilityTime > 0.0f){
            invincibilityTime = PinBehavior2.invincibilityTime;
            string message = "";

        if(invincibilityTime > 0.0){
                
                float fill = invincibilityTime / invinciblityDuration;
                message = string.Format("{0:0.0}", invincibilityTime);
                overlay.fillAmount = fill;
            }else{
                fill = 0.0f;
                overlay.fillAmount = fill;
            }
            label.text = message;
        }       
    }
}

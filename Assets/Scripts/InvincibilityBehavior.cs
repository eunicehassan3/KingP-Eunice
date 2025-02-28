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
    public float invincibiltyCooldown;
    public float invincibilityCooldownRate;
    public float cooldownRate;
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
        invincibilityCooldownRate = PinBehavior2.invincibilityCooldownRate;
        overlay.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(PinBehavior2.invincibiltyCooldown > 0.0f){
            invincibiltyCooldown = PinBehavior2.invincibiltyCooldown;
            string message = "";

        if(invincibilityTime > 0.0){
                
                // float fill = invincibilityTime / invinciblityDuration;
                float fill = invincibiltyCooldown / invincibilityCooldownRate;
                message = string.Format("{0:0.0}", invincibiltyCooldown);
                overlay.fillAmount = fill;
            }else{
                overlay.fillAmount = 0.0f;
            }
            label.text = message;
        }       
    }
}

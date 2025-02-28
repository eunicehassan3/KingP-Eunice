using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinBehavior2 : MonoBehaviour
{
    public float baseSpeed = 2.0f;
    public float dashSpeed = 8.0f;
    public float currentSpeed;
    public float dashDuration = 0.3f;
    public float dashTimeStart;
    
    public static float cooldownRate = 5.0f;
    public static float cooldown = 0.0f;
    public float timeLastDashEnded;

    public bool dashing;
    private Camera cam;
    private Vector3 mousePosG;
    public bool invincible;
    public static float invincibilityDuration = 10.0f;
    public float invincibleTimeStart;
    public float invincibleTimeLastEnded;
    public static float invincibilityTime;
    public static float invincibiltyCooldown;
    public static float invincibilityCooldownRate = 10.0f;
    public SpriteRenderer spriteRend;
    
    void Start()
    {
        cam = Camera.main;
        currentSpeed = baseSpeed;
        dashing = false;
        cooldown = 0.0f;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
       
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 newPosition = Vector2.MoveTowards(transform.position, mousePosG, currentSpeed * Time.deltaTime);
        transform.position = newPosition;

        Invincible();  
        Dash();
        
    }

    public void Dash()
    {
        if (dashing)
        {
            
            if (Time.time - dashTimeStart > dashDuration)
            {
                dashing = false;
                currentSpeed = baseSpeed;
                timeLastDashEnded = Time.time;
                cooldown = cooldownRate;
                // Color white = new Color(255,255,255, 255);
                spriteRend.color = Color.white;
                Debug.Log("Dash ended, cooldown started.");
            }
        }
        else 
        {
            float timeSinceLastDash = Time.time - timeLastDashEnded;
            cooldown = cooldown - Time.deltaTime;
            if(cooldown <0.0){
                cooldown = 0.0f;
            }
            if (cooldown == 0.0 && Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse Click Detected!"); 

                if (timeSinceLastDash > cooldownRate)
                {
                    Debug.Log("Dashing Started!");
                    dashing = true;
                    // Color purple = new Color(155,30,215, 255);
                    // gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                    spriteRend.color = Color.magenta;
                    currentSpeed = dashSpeed;
                    dashTimeStart = Time.time;
                }
                else
                {
                    Debug.Log("Cooldown Active. Cannot Dash Yet.");
                }
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        string collided = collision.gameObject.tag;
        Debug.Log("Collided with " + collided);

        if((collided == "Ball" && !invincible) || (collided == "Wall" && !invincible)){
            StartCoroutine(WaitForsoundAndTransition());
            SceneManager.LoadScene("GameOver");
            Debug.Log("Game Over");
        }
    }

    private IEnumerator WaitForsoundAndTransition(){
        AudioSource src = GetComponent<AudioSource>();
        src.Play();
        yield return new WaitForSeconds(src.clip.length);

    }

    public void Invincible(){
        invincibiltyCooldown -= Time.deltaTime;
        if(invincibiltyCooldown < 0.0f){
            invincibiltyCooldown = 0.0f;
        }
        //  float timeSinceLastInvincibility = Time.time - invincibleTimeLastEnded; 

        // && timeSinceLastInvincibility >= cooldownRate
        if(Input.GetKeyDown(KeyCode.Space) && cooldown==0.0f){
            invincible = true;
            spriteRend.color = Color.blue;
            invincibleTimeStart = Time.time;
            invincibiltyCooldown = invincibilityCooldownRate + invincibilityDuration;
        }

        if(invincible){
            invincibilityTime = Time.time - invincibleTimeStart;
            if(invincibilityTime >= invincibilityDuration){
                invincible = false;
                spriteRend.color = Color.white;
    

            }
        }

        
    }
}


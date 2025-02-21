using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PinBehavior : MonoBehaviour
{
    public float baseSpeed = 2.0f;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;
    public float dashSpeed = 8.0f;
    public float currentSpeed;
    public bool dashing;
    public float dashDuration = 0.3f;
    public float dashTimeStart;

    public static float cooldownRate = 5.0f;
    public static float cooldown;
    public float timeLastDashEnded;
    public AudioSource[] audioSources;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        dashing = false;
        audioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, baseSpeed * Time.fixedDeltaTime);
        transform.position = newPosition;

        Dash();

    }
    
    private void OnCollisionEnter2D(Collision2D collision){
        string collided = collision.gameObject.tag;
        Debug.Log("Collided with " + collided);

        if(collided == "Ball" || collided == "Wall"){
            Debug.Log("Game Over");
        }
    }

    public void Dash(){
          if(dashing == true){
            float howLong = Time.time - dashTimeStart;
            currentSpeed = dashSpeed;
            if(howLong > dashDuration){
                dashing = false;
                currentSpeed = baseSpeed;
                timeLastDashEnded = Time.time;
                Debug.Log("Dash ended, cooldown started.");
                cooldownRate = cooldown;      
            }else{
                cooldown = cooldown - Time.deltaTime;
                 if(cooldown < 0.0f){
                    cooldown = 0.0f;
                }
                if(Input.GetMouseButtonDown(0) == true && cooldown == 0.0f){
                    dashing = true;
                    currentSpeed = dashSpeed;
                    dashTimeStart = Time.time;
                }
        }
            
        }
        
    }
}

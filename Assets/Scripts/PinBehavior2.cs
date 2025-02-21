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
    
    public static float cooldownRate = 1.0f;
    public float timeLastDashEnded;

    public bool dashing;
    private Camera cam;
    private Vector3 mousePosG;
    
    void Start()
    {
        cam = Camera.main;
        currentSpeed = baseSpeed;
        dashing = false;
    }

    void Update()
    {
       
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 newPosition = Vector2.MoveTowards(transform.position, mousePosG, currentSpeed * Time.deltaTime);
        transform.position = newPosition;

        
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
                Debug.Log("Dash ended, cooldown started.");
            }
        }
        else 
        {
            float timeSinceLastDash = Time.time - timeLastDashEnded;

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse Click Detected!"); 

                if (timeSinceLastDash > cooldownRate)
                {
                    Debug.Log("Dashing Started!");
                    dashing = true;
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

        if(collided == "Ball" || collided == "Wall"){
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
}



using UnityEngine;

public class BallBehavior2 : MonoBehaviour
{
    public float minX = -6.31f;
    public float maxX= 6.22f;
    public float minY = -4.3f;
    public float maxY = 3.7f;
    public GameObject target;
    public float minSpeed;
    public float maxSpeed;
    public int secondsToMaxSpeed;
    public bool launching;
    public float launchDuration = 3.0f;  // How long the ball chases the pin
    public float cooldown = 5.0f;  // Cooldown before next launch
    private float timeLaunchStart;
    private float timeLastLaunch;

    public Vector2 targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minSpeed = 1.0f;
        maxSpeed = 10.0f;
        secondsToMaxSpeed = 30;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currPos = gameObject.GetComponent<Transform>().position;
        float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, getDifficultyPercentage());

        if(onCooldown() == false && launching == false){
            launch();
        }

        if(launching){
            // currentSpeed *= 2.0f;
            if (Time.time - timeLaunchStart > launchDuration){
                startCoolDown();
            }
        }

        if(launching && !onCooldown()){
            targetPosition = (Vector2)target.transform.position;
        }
        else{
             targetPosition = getRandomPosition();
            // targetPosition = currPos;
            
        }

        Vector2 newPosition = Vector2.MoveTowards(currPos, targetPosition, currentSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    private float getDifficultyPercentage(){
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
    }

    private void launch()
    {
        launching = true;
        timeLaunchStart = Time.time;
        Debug.Log("Launching towards the pin!");
    }

    // **Cooldown logic after launching ends**
    private void startCoolDown()
    {
        launching = false;
        timeLastLaunch = Time.time;
        Debug.Log("Cooldown started...");
    }

    private bool onCooldown()
    {
        return (Time.time - timeLastLaunch) < cooldown;
    }

    Vector2 getRandomPosition(){
        float RandomX = Random.Range(minX, maxX);
        float RandomY = Random.Range(minY, maxY);
        
        Vector2 pos = new Vector2(RandomX,RandomY);
        return pos;
    }
}
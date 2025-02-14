
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

    Rigidbody2D body;
    public bool rerouting;

    public Vector2 targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition();
        minSpeed = 1.0f;
        maxSpeed = 10.0f;
        secondsToMaxSpeed = 30;
    }

    void Update(){

    }

    public void initialPosition(){
        body = GetComponent<Rigidbody2D>();
        transform.position = getRandomPosition();
        // body.position = getRandomPosition();
        targetPosition = getRandomPosition();

        launching = false;
        rerouting = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {   
        // Vector2 currPos = gameObject.GetComponent<Transform>().position;
        body = GetComponent<Rigidbody2D>();
        Vector2 currPos = body.position;
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
        body.MovePosition(newPosition);
        // transform.position = newPosition;
    }

    private float getDifficultyPercentage(){
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
    }

    private void launch()
    {
        Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
        targetPosition = targetBody.position;
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

    public void Reroute(Collision2D collision){
        GameObject otherBall = collision.gameObject;
        if(rerouting == true){
            otherBall.GetComponent<BallBehavior2>().rerouting = false;
            Rigidbody2D ballBody = otherBall.GetComponent<Rigidbody2D>();
            Vector2 contact = collision.GetContact(0).normal;
            targetPosition = Vector2.Reflect(targetPosition, contact).normalized;

            launching = false;
            float seperationDistance = 0.1f;
            ballBody.position += contact * seperationDistance;
        }
        else{
            rerouting = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Wall"){
            targetPosition = getRandomPosition();
        }
        if(collision.gameObject.tag == "Ball"){
            Reroute(collision);
        }
            
        Debug.Log(this + " Collided with: "+ collision.gameObject.name);
    }

    public void setBounds(float miX, float maX, float miY, float maY){
        minX = miX;
        maxX = maX;
        minY = miY;
        maxY = maY;
    }

    public void setTarget(GameObject pin){
        target = pin;
}

}
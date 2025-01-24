using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class BallBehavior : MonoBehaviour
{
    public float minX = -6.31f;
    public float maxX= 6.22f;
    public float minY = -4.3f;
    public float maxY = 3.7f;
    public float minSpeed;
    public float maxSpeed;
    public Vector2 targetPosition;
    public int secondsToMaxSpeed;
    // for launching to target pin
    public GameObject target; 
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float minTimeToLaunch;
    public float maxTimeToLaunch;
    public float cooldown;
    public bool launching;
    public float launchDuration;
    public float TimeLaunchStart;
    public float timeLastLaunch;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secondsToMaxSpeed = 30;
        minLaunchSpeed = 6;
        maxLaunchSpeed = 20;
        // cooldown = 5;
        // minSpeed = .1;
        // maxSpeed = 5;
        targetPosition = getRandomPosition();

    }

    // Update is called once per frame
    void Update()
    {
        //to change position of the balls
        Vector2 currPos = gameObject.GetComponent<Transform>().position;
        float distance = Vector2.Distance(currPos, targetPosition);
        if(launching == false &&  onCooldown() == false){
            launch();
        }
        if(distance > 0.1){
            float difficulty = getDifficultyPercentage();
            float currentSpeed;
            if(launching == true){
                float launchingForHowLong = Time.time - TimeLaunchStart;
                if(launchingForHowLong > launchDuration){
                    startCoolDown();
                }
                currentSpeed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed, difficulty) * Time.deltaTime; 
                Vector2 newPosition = Vector2.MoveTowards(currPos, targetPosition, currentSpeed);
                transform.position = newPosition;
            }
        }
        

        else{
            if(launching == true){
                startCoolDown();
            }
            targetPosition = getRandomPosition();
        }

        // //for the launching
        // float timeLaunching = Time.time - timeLastLaunch;
        // if(timeLaunching > launchDuration){
        //     startCoolDown();
        // }else{
        //     launch();
        // }
       
    }

    Vector2 getRandomPosition(){
        float RandomX = Random.Range(minX, maxX);
        float RandomY = Random.Range(minY, maxY);
        
        return new Vector2(RandomX,RandomY);
    }

    private float getDifficultyPercentage(){
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
    }

    public void launch(){
        launching = true;
        targetPosition = target.transform.position;
        float TimeLaunchStart = Time.time;
        if(launching == false){
            launching = true;
        
        }
    }

    public bool onCooldown(){
        bool res = false;

        float TimeSinceLastLauch = Time.time - timeLastLaunch;
        if(TimeSinceLastLauch < cooldown){
            res = true;
        }
        
        return res;
    }

    public void startCoolDown(){
        timeLastLaunch = Time.time;
    }
}

using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class BallBehavior : MonoBehaviour
{
    public float minX = -6.31f;
    public float maxX= 6.22f;
    public float minY = -4.3f;
    public float maxY = 3.7f;
    public float minSpeed;
    public float maxSpeedX;
    public Vector2 targetPostions;
    public int secondsToMaxSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secondsToMaxSpeed = 30;
        minSpeed = 0.75f;
        maxSpeedX = 2.0f;
        targetPostions = getRandomPosition();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currPos = gameObject.GetComponent<Transform>().position;

        if(targetPostions != currPos){
            float currentSpeed = minSpeed; 
            Vector2 newPosition = Vector2.MoveTowards(currPos, targetPostions, currentSpeed);
            transform.position = newPosition;
        }
        else{
            targetPostions = getRandomPosition();
        }
       
    }

    Vector2 getRandomPosition(){
        float RandomX = Random.Range(minX, maxX);
        float RandomY = Random.Range(minY, maxY);
        
        return new Vector2(RandomX,RandomY);
    }
}

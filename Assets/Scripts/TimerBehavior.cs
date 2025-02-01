using TMPro;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

public class TimerBehavior : MonoBehaviour
{
    private TextMeshProUGUI textField; 
    private float timer;
    public int secondsToMaxSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       textField =  GetComponent<TextMeshProUGUI>();

       if(textField == null){
        Debug.Log("No Components Found");
       }
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time;

        int minutes = (int) timer / 60;
        int second = (int) timer % 60;

        // string message = "Time: "+ minutes + ":" + second;

        string message = string.Format("<color=black>Timer: {0:00}:{1:00}", minutes, second);

        textField.text = message;

        // Debug.Log(timer);
    }
}

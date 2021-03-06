using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePassed : MonoBehaviour
{
    private float time=0f;
    private bool isTimeStopped = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Text>().text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeStopped) return;
        time += Time.deltaTime;
        gameObject.GetComponent<Text>().text = time.ToString("F")+" sec";
    }

    public float getTime()
    {
        return time;
    }

    public  void stopTime()
    {
        isTimeStopped = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateTimeController : MonoBehaviour {

    public static DateTimeController instance;

    private const int MINUTES_IN_A_DAY = 1440;

    public int timeInMinutes;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        timeInMinutes = 0;
        StartCoroutine(testUpdateTime());
	}

    IEnumerator testUpdateTime()
    {
        while (true) {
            yield return new WaitForSeconds(1.0f);
            timeInMinutes += 60;
            Debug.Log("time: " + timeInMinutes);
        }
    }

    public void updateTimeInHour(int hour)
    {
        updateTime(60 * hour);
    }

    public void updateTime(int minute)
    {
        timeInMinutes += minute;
    }

    public int getDate()
    {
        return (timeInMinutes / MINUTES_IN_A_DAY) + 1;
    }

    public void setTimeTo(int day, int hour, int minute)
    {
        timeInMinutes = day * MINUTES_IN_A_DAY + hour * 60 + minute;
    }

    public void setTimeTo(int day, string time)
    {
        timeInMinutes = day * MINUTES_IN_A_DAY;
        if (time.Equals("Midnight"))
            timeInMinutes += 0;
        else if (time.Equals("Dawn"))
            timeInMinutes += 6 * 60;
        else if (time.Equals("Morning"))
            timeInMinutes += 8 * 60;
        else if (time.Equals("Afternoon"))
            timeInMinutes += 12 * 60;
        else if (time.Equals("Evening"))
            timeInMinutes += 16 * 60;
        else if (time.Equals("Night"))
            timeInMinutes += 18 * 60;
    }

    public string getTime()
    {
        int currentTime = getcurrentTimeInDay();
        if (currentTime < 6)
            return "Night";
        else if (currentTime < 8)
            return "Dawn";
        else if (currentTime < 12)
            return "Morning";
        else if (currentTime < 16)
            return "Afternoon";
        else if (currentTime < 18)
            return "Evening";
        else if (currentTime < 24)
            return "Night";
        else
            return "Unknown Time and Space";
    }

    public int getcurrentTimeInDay()
    {
        return timeInMinutes % MINUTES_IN_A_DAY / 60;
    }
}

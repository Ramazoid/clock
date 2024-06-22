using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StartUP : MonoBehaviour
{
    private TimeLoader loader;

    void Start()
    {
        string url = "https://yandex.com/time/sync.json";

        loader = GetComponent<TimeLoader>();
        loader.GetTime(url, Parse);
    }

    private void Parse(string res)
    {
        double t = double.Parse(res);
        TimeSpan time = TimeSpan.FromMilliseconds(t);

        Clock.SetMinutes(time.Minutes);
        Clock.SetHours(time.Hours + 5);
        Clock.SetSeconds(time.Seconds);
        Clock.StartTime();
    }

}

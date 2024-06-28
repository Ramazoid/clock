using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Clock : MonoBehaviour
{
    public Transform MinutesArrowPivot;
    public Transform HoursArrowPivot;
    public Transform SecondsArrowPivot;
    public IconCwitching switcher;
    private static Clock IN;
    public int Minutes;
    
    public bool clockRunning;
    private static Coroutine runner;
    public int Seconds;
    public int Hours;
    public TextMeshPro clockText;
    public static bool editMode;
    public ScreenInput HoursInput;
    public ScreenInput MinutesInput;
    public ScreenInput SecondsInput;

    private void Awake()
    {
        IN = this;
        switcher.SetTo(false);
    }

    public static void SetMinutes(int minutes)
    {
        
        print("new minutes = "+minutes);
        IN.Minutes = minutes;
        float angle = 6 * minutes;
        IN.MinutesArrowPivot.eulerAngles=new Vector3(0, angle,0);
      
    }

       internal static void SetHours(int hours)
    {
        IN.Hours = hours;
        // if (hours > 12) hours -= 12;
        float angle = 30 * hours+30*IN.Minutes/60;
        print($"-----hours of [ {hours} ] correspond angle [ {angle} ]]");
        IN.HoursArrowPivot.eulerAngles = new Vector3(0, angle, 0);
    }

    void Start()
    {
        MinutesArrowPivot.GetComponentInChildren<ARotate>().UpdateValue = SetMinutes;
        HoursArrowPivot.GetComponentInChildren<ARotate>().UpdateValue = SetHours;
    }

    // Update is called once per frame
    void Update()
    {
            clockText.text = Hours + ":" + Minutes + ":" + Seconds;
        if (editMode)         return;

        if (Seconds==60)
        {
            Seconds = 0;SetSeconds(Seconds);
            Minutes++;SetMinutes(Minutes);
            if(Minutes==60)
            {
                Minutes = 0;SetMinutes(Minutes);
                Hours++;SetHours(Hours);
                TimeLoader.GetTime();
                if (Hours==24)
                {
                    Hours = 0;SetHours(Hours);
                    
                }
            }
        }
    }

    private char onInput(string input,char c)
    {
        if (((c >= '0' && c <= '9')||c==':')&& input.Length<9)
        {
            return c;
        }
        else return '\0';
    }

    

    public void SetFromInput(string who)
    {
        print("fromINPUT" + who);
        switch (who)
        {
            case "HOU":SetHours(int.Parse(HoursInput.value));break;
            case "MIN":SetMinutes(int.Parse(MinutesInput.value));break;
            case "SEC":SetSeconds(int.Parse(SecondsInput.value));
                HoursInput.gameObject.SetActive(false);
               MinutesInput.gameObject.SetActive(false);
                SecondsInput.gameObject.SetActive(false);
                EditMode(); switcher.SetTo(false);
                break;
        }
    }
    
    internal static void SetSeconds(int seconds)
    {
        IN.Seconds = seconds;
        float angle =6 * seconds;
        
        IN.SecondsArrowPivot.eulerAngles = new Vector3(0, angle, 0);
    }

    internal static void StartTime()
    {
        IN.clockRunning = true;
        runner = IN.StartCoroutine(IN.Runner());
}
    public IEnumerator Runner()
    {
        while(clockRunning)
        {
            yield return new WaitForSeconds(1);
            IN.Seconds++;
            
            SetSeconds(IN.Seconds);
        }
    }
    public void EditMode()
    {
        editMode = !editMode;
        if (editMode)
        {
            if (runner != null) StopCoroutine(runner);
            HoursInput.Activate();
            //clockText.gameObject.SetActive(false);
            HoursInput.done = SetFromInput;
            MinutesInput.done = SetFromInput;
            SecondsInput.done = SetFromInput;
        }
        else
        {
            HoursInput.gameObject.SetActive(false);
            MinutesInput.gameObject.SetActive(false);
            SecondsInput.gameObject.SetActive(false);
            runner = StartCoroutine(Runner());
            clockText.gameObject.SetActive(true);
        }
    }

    internal static void SetFromInputValue(string inputName)
    {
        IN.SetFromInput(inputName); 
    }
}

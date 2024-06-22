using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class IconCwitching : MonoBehaviour
{
    public RectTransform tip;
    public Sprite OffSprite;
    public Sprite OnSprite;
    public string tipONText;
    public string tipOFFText;
    public bool tipShowed;
    public UnityEvent Switch;
    public bool IsON;
    public Color OnColor;
    public Color OffColor;
    public TextMeshProUGUI tipText;
    private RectTransform rt;

    void Start()
    {

        //tipText = tip.GetComponentInChildren<TextMeshPro>();
        rt = GetComponent<RectTransform>();
        
    }

    public void OnOver()
    {
       tip.gameObject.SetActive(true);
        tipShowed = true;
    }
    public void OnOut()
    {
        tip.gameObject.SetActive(false);
        tipShowed = false;
    }
    public void OnPress()
    {
        IsON = !IsON;
        
        if (Switch!=null) Switch.Invoke();
    }
    
    void Update()
    {
        if(tipShowed)
        {
            Vector2 mousepos2 = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            tip.position = mousepos2;
        }
        rt.ImgSetColor(IsON ?OnColor : OffColor);
        rt.ImgSetSprite(IsON ?OnSprite : OffSprite);
        tipText.text = (IsON ? tipOFFText : tipONText);
    }

    internal void SetTo(bool state)
    {
        IsON = state;
    }
}

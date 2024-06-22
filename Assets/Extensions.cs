using UnityEngine;
using UnityEngine.UI;
using System;
using static UnityEngine.Networking.UnityWebRequest;


public static class StringExtensions
{
    
    public static string Extract(this string s,char c)
    {
        int i = s.IndexOf(c);
        return s.Substring(0, i);
    }
    public static string Sub(this string s, int n, int l)
    {
        return s.Substring(n, l);
    }

    public static string Between(this string s,string c1,string c2)
    {
        int n1 = s.IndexOf(c1)+c1.Length;
        int n2 = s.IndexOf(c2,n1);
        return s.Substring(n1,n2-n1-1);
    }
    public static string UpTo(this string s, char c)
    {
        return s.Substring(0, s.IndexOf(c));
    }
}
public static class GameObjectExtensions
{
    public static T Child<T>(this GameObject g, string nam)
    {
        return g.transform.Find(nam).GetComponent<T>();
    }
    public static Vector3 Vector3X0Z(this Vector3 v)
    {
        return new Vector3(v.x, 0, v.z);
    }
    public static Vector3 MUltiply(this Vector3 v, float x, float y, float z)
    {
        return new Vector3(v.x * x, v.y * y, v.z * z);
    }
}
public static class RecttransformExtensions
{
    public static void ImgSetColor(this RectTransform t, Color c)
    {
        t.GetComponent<Image>().color = c;
    }
    public static void ImgSetSprite(this RectTransform t, Sprite sprite)
    {
        t.GetComponent<Image>().sprite = sprite;
    }
}

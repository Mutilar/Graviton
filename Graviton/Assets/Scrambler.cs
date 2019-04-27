using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scrambler : MonoBehaviour
{
    public float rate = 20f;// n letter reveals/second
    float last = 0f;//time since last reveal
    public string known;
    string revealed, scrambled;
    int unknown = 0;
    string possibleSymbolsList = "abcdefghijklmnopqrstuvqxyzABCDEFGHIJKLMNOQRSTUVWXYZ11223344556677889900!@#$%^&*";
    public string display;
    public void addToString(string str)
    {
        known += str;
        unknown += str.Length;
        for (int i = 0; i != 5; i++)
        {
            scrambled = scrambled + possibleSymbolsList.ToCharArray()[(int)(Random.value * possibleSymbolsList.Length)].ToString();
        }
    }
    void Update()
    {
        //this.GetComponent<Text>().text = "";
        //string[] splitted = display.Split('\n');
       // for (int i = 0; i < splitted.Length; i++)
       // {
        //    this.GetComponent<Text>().text += "<size=" + ((int)((i+1) * 50 / splitted.Length) + 5) + ">" + splitted[i] + "</size>\n";
       // }
       // while (display.Split('\n').Length > 5)  display = display.Substring(display.IndexOf("\n") + 1);
        this.GetComponent<Text>().text = display;
        if (unknown == 0 && scrambled == null)
        {
            unknown = known.Length;
            scrambled = "1234567";//length of buffer
            for (int i = 0; i != scrambled.Length; i++)
            {
                scrambled = scrambled + possibleSymbolsList.ToCharArray()[(int)(Random.value * possibleSymbolsList.Length)].ToString();
                scrambled = scrambled.Substring(1);
            }
        }
        if (unknown > 0)
            if (1 / rate > last)
                last += Time.deltaTime;
            else
            {
                int descrambles = (int)(last * rate);
                last = last % (1 / rate);
                if (unknown > descrambles)
                    last -= 1 / rate;
                revealed += known.Substring(0, descrambles);
                known = known.Substring(descrambles);
                unknown -= descrambles;
                if (scrambled.Length > unknown)
                {
                    scrambled = scrambled.Substring(scrambled.Length - unknown);
                }
                for (int i = 0; i != scrambled.Length; i++)
                    if (Random.value > .4f || i >= scrambled.Length - descrambles)//frequency of scrambles
                    {
                        scrambled = scrambled + possibleSymbolsList.ToCharArray()[(int)(Random.value * possibleSymbolsList.Length)].ToString();
                        scrambled = scrambled.Substring(1);
                    }
                display = revealed + scrambled;
                last -= (int)(last / rate);
            }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
    public GameObject captwords;
    public GameObject otherwords;
	// Use this for initialization
	void Start () {
		
	}
    float counter = 0;
    int line = 0;
	// Update is called once per frame
	void Update ()
    {
        //GameObject.Find("Text (1)").GetComponent<Text>().text = captwords;
        //GameObject.Find("Text (2)").GetComponent<Text>().text = otherwords;
        captwords.GetComponent<RectTransform>().Translate(new Vector2(0, .2f));
        otherwords.GetComponent<RectTransform>().Translate(new Vector2(0, .2f));
        counter += Time.deltaTime;
		if ( Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("World");
        }
        if (counter >= 1.2f && line == 0)
        {
            otherwords.GetComponent<Scrambler>().addToString("Captain! The radar scanner indicates two shining points. They're approaching rapidly.\n");
            line++;
        }
        if (counter >= 7.5f && line == 1)
        {
            line++;
            captwords.GetComponent<Scrambler>().addToString("\n\nAll crews, to your stations!\n");
        }
        if (counter >= 10.5f && line == 2)
        {
            line++;
            otherwords.GetComponent<Scrambler>().addToString("\nWhat's happening captain?\n");
        }
        if (counter >= 12.25f && line == 3)
        {
            line++;
            captwords.GetComponent<Scrambler>().addToString("\nOur radar's picking up two strange metallic bodies. I presume they're spaceships.\n");
        }
        if (counter >= 17f && line == 4)
        {
            line++;
            otherwords.GetComponent<Scrambler>().addToString("\n\nSpaceships, in that part of space? They could be asteroids, they've got to be asteroids!\n");
        }
        if (counter >= 22f && line == 5)
        {
            line++;
            captwords.GetComponent<Scrambler>().addToString("\n\nNo asteroids could vary their speed like that. They're being controlled somehow.\n");
        }
        if (counter >= 26f && line == 6)
        {
            line++;
            otherwords.GetComponent<Scrambler>().addToString("\n\nCaptain, try to find out what's happening. But remember, they're not terrestrial spaceships- that is if they are spaceships.\n");
        }
        if (counter >= 33f && line == 7)
        {
            line++;
            captwords.GetComponent<Scrambler>().addToString("\n\n\nThere's a way to get an answer to that. Activate the scanners.\n");
        }
        if (counter >= 36f && line == 8)
        {
            line++;
            otherwords.GetComponent<Scrambler>().addToString("\n\nExecuted.\n");
        }
        if (counter >= 41f && line == 9)
        {
            line++;
            captwords.GetComponent<Scrambler>().addToString("\nThe two objects are now in our cameras.\n");
        }
        if (counter >= 43f && line == 10)
        {
            line++;
            otherwords.GetComponent<Scrambler>().addToString("\nAnd they're really spaceships. There just isn't any other possibilty.\n");
        }
        if (counter >= 47.5f && line == 11)
        {
            line++;
            captwords.GetComponent<Scrambler>().addToString("\n\nEstablish contact.");//The two objects are now in our cameras.";
        }
        if (counter >= 49.5f && line == 12)
        {
            line++;
            otherwords.GetComponent<Scrambler>().addToString("\nNegative contact.\n");
        }
        if (counter >= 51f && line == 13)
        {
            line++;
            otherwords.GetComponent<Scrambler>().addToString("Hamiliton, try again. Try with all possible ways of communication. Try with a radio too.\n");
        }
        if (counter >= 57f && line == 14)
        {
            line++;
            captwords.GetComponent<Scrambler>().addToString("\n\n\n\nHallah, interrupt contact with base. Analyse the spaceship on our computer.\n");//The two objects are now in our cameras.";
        }
        if (counter >= 63f && line == 15)
        {
            line++;
            otherwords.GetComponent<Scrambler>().addToString("\n\nUnmanned spaceships armed with long-ranged disintegrators.\n");
        }
        if (counter >= 69f && line == 16)
        {
            line++;
            captwords.GetComponent<Scrambler>().addToString("\n\nBattlespeed.\n");//Hallah, interrupt contact with base. Analyse the spaceship on our computer.";//The two objects are now in our cameras.";
        }
        if (counter >= 70f && line == 17)
        {
            line++;
            otherwords.GetComponent<Scrambler>().addToString("\nBattlespeed.\n");//Hallah, interrupt contact with base. Analyse the spaceship on our computer.";//The two objects are now in our cameras.";
        }
        if (counter >= 71f && line == 18)
        {
            line++;
            captwords.GetComponent<Scrambler>().addToString("\nActivate disintegrators, coordinate direction rays, activate laser! Best range, 50 miles. Approaching range....\n");//B..attlespeed";//Hallah, interrupt contact with base. Analyse the spaceship on our computer.";//The two objects are now in our cameras.";
        }
        if (counter >= 82f)
        {
            SceneManager.LoadScene("World");
        }
    }
}

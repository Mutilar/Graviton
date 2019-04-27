using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSplash : MonoBehaviour {
    public GameObject[] ships = new GameObject[1000];
    int ship_counter = 0;

    public Sprite[] ship_sprites = new Sprite[14];
    // Use this for initialization
    float counter = 0;
    public void play()
    {
        Destroy(GameObject.Find("Button").transform.GetChild(0).gameObject, .8f);// = new Color(255, 255, 255, 1 - counter);
        Destroy(GameObject.Find("Button (1)").transform.GetChild(0).gameObject, .8f);//.GetComponent<Image>().color = new Color(255, 255, 255, 1 - counter);
        Destroy(GameObject.Find("Button (2)").transform.GetChild(0).gameObject, .8f);//.GetComponent<Image>().color = new Color(255, 255, 255, 1 - counter);

        counter = .01f;
     //   SceneManager.LoadScene("World");
    }
    public void settings()
    {
        GameObject.Find("info").GetComponent<Text>().enabled = true;
        GameObject.Find("info").GetComponent<Scrambler>().enabled = true;
        GameObject.Find("info").GetComponent<Scrambler>().addToString(GameObject.Find("info").GetComponent<Text>().text);
        GameObject.Find("info").GetComponent<Text>().text = "";

    }
    public void exit()
    {
        Application.Quit();
    }
    void Start () {
        for (int i = 0; i < ships.Length; i++)
        {
            ships[i] = Instantiate(Resources.Load("ship"), new Vector2(0, 5), this.transform.rotation) as GameObject;
            ships[i].transform.SetParent(this.transform);
            ships[i].SetActive(false);
        }
    }
    float timer = 0;
	// Update is called once per frame
	void Update ()
    {
        if (counter != 0)
        {
            timer += Time.deltaTime;
            counter += counter *Time.deltaTime * 5;
            GameObject.Find("fade").GetComponent<Image>().color = new Color(0, 0, 0, counter);
            GameObject.Find("Button").GetComponent<Image>().color = new Color(255, 255, 255, 1-counter);
            GameObject.Find("Button (1)").GetComponent<Image>().color = new Color(255, 255, 255,1- counter);
            GameObject.Find("Button (2)").GetComponent<Image>().color = new Color(255, 255, 255, 1-counter);
            
            if (timer > 1)
            {
                SceneManager.LoadScene("Intro");
            }
        }
        for (int i = 0; i < 1000; i++)
        {
            if (ships[i].activeSelf)
            {
                ships[i].transform.Translate(new Vector2(0, float.Parse(ships[i].name) * Time.deltaTime));
                if (ships[i].transform.position.y < -5f) ships[i].SetActive(false);
            }
        }
        if (Random.value < .025f)
        {
            float val = (Random.value * 11f);
            val *= Random.value;

            make_ship((int)val);
        }
	}
    public void make_ship(int type)
    {

        ships[ship_counter].GetComponent<SpriteRenderer>().sprite = ship_sprites[type];
        ships[ship_counter].transform.localScale = new Vector3(ValuesManager.getScale(type), ValuesManager.getScale(type), 1);
        ships[ship_counter].transform.position = new Vector3(Random.value * 5f - 2.5f, 5f);
        ships[ship_counter].SetActive(true);
        //ships[ship_counter].GetComponent<ShipManager>().speed = ValuesManager.getSpeed(type) / 10f;
        ships[ship_counter].name = ""+ValuesManager.getSpeed(type) / 10f;// "" + (ValuesManager.getSpeed(type) * 100) / 1;
                                                                         // ships[ship_counter].AddComponent<BoxCollider2D>();
        ships[ship_counter].GetComponent<ShipManager>().enabled = false;
        ships[ship_counter].transform.rotation = Quaternion.Euler(0, 0, 180);//new Vector3(Random.value * 5f - 2.5f, 5f);
        ship_counter++;
        if (ship_counter == ships.Length) ship_counter = 0;
    }
}

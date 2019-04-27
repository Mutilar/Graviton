using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool hint_shown = false;
    int level = 0;

    // const Vector2 GAMESPACE = new Vector2(3,)
    public Vector2[] path_points;// = Vector2[14]();
    GameObject[] path_points_markers;
    GameObject[] path_points_curves;
    GameObject[] path_points_lines;
    public GameObject path_start_quarter;
    public GameObject path_end_quarter;
    public float direction_multiplier = 1f;
    bool startstart = false;

    public GameObject[] ships = new GameObject[1000];
    int ship_counter = 0;

    public Sprite[] ship_sprites = new Sprite[14];

    public GameObject circle;
    public GameObject levelll;
    public GameObject levell;
    public List<GameObject> satellites = new List<GameObject>();

    public List<Vector2> orders;
    float game_time;
    public Text game_timer;

    public GameObject sat_UI;
    public GameObject sat_purchase_button;
    bool purchased_sat = false;
    int chosen_satellite;
    public Sprite[] station_types = new Sprite[3];
    public Text money_display;
    public int money = 35;
    public GameObject highlighter;
    /* DAVID */

    bool just_clicked = false;

    // Use this for initialization
    void Start()
    {

        //Pooling ships
        for (int i = 0; i < ships.Length; i++)
        {
            ships[i] = Instantiate(Resources.Load("ship"), new Vector2(0,5), this.transform.rotation) as GameObject;
            ships[i].transform.SetParent(this.transform);
            ships[i].SetActive(false);
        }
        orders = ValuesManager.getLevel(0);
        draw_path();


    }
    int chosen = -1;
    public void draw_path()
    {
        direction_multiplier = 1f;
     //   if (Random.value < .5f) direction_multiplier = -1f;

        path_points_markers = new GameObject[16];
        path_points_curves = new GameObject[10];
        path_points_lines = new GameObject[7];
        //Pooling path sprites
        for (int i = 0; i < path_points_markers.Length; i++)
        {
            path_points_markers[i] = Instantiate(Resources.Load("Marker"), this.transform.position, this.transform.rotation) as GameObject;
            path_points_markers[i].transform.SetParent(this.transform);
            path_points_markers[i].SetActive(false);
        }
        for (int i = 0; i < path_points_curves.Length; i++)
        {
            path_points_curves[i] = Instantiate(Resources.Load("halfcircle"), this.transform.position, this.transform.rotation) as GameObject;
            path_points_curves[i].transform.SetParent(this.transform);
            path_points_curves[i].SetActive(false);
        }
        for (int i = 0; i < path_points_lines.Length; i++)
        {
            path_points_lines[i] = Instantiate(Resources.Load("pathline"), this.transform.position, this.transform.rotation) as GameObject;
            path_points_lines[i].transform.SetParent(this.transform);
            //path_points_lines[i].SetActive(false);
        }

        path_points = new Vector2[15];
      //  path_points[0] = new Vector2(.5f * direction_multiplier, 3);
        path_points[14] = new Vector2(0, -4);
        path_points[13] = new Vector2(-.5f * direction_multiplier, -3);

        int point_counter = 0;
        int curve_counter = 0;
        int line_counter = 0;
        float size_of_line = 0;
        for (int y = 3; y > -3; y--)
        {
            //PLOT NEXT TWO POINTS
            if (point_counter == 0 ) path_points[0] = new Vector2(.5f * direction_multiplier, 3);
            else path_points[point_counter] = new Vector2(path_points[point_counter - 1].x, y); /*change 6 for difficulty, greater = );*/
            float value = 0;//path_points[point_counter - 1].x;
            value += direction_multiplier * ((Random.value * 2.25f));
            value = Mathf.Clamp(value, -2.25f, 2.25f);
            if (y == 3 || y == -2)
            {
                if (Mathf.Abs(value) < .5f)
                {
                    if (value > 0f) value += .5f;
                    else value -= .5f;
                }
                    //value -= .5f;
                //if (value > .5f) value += .5f;
            }
            path_points[point_counter + 1] = new Vector2(value, y);//(((Random.value) * 4) + .75f) * direction_multiplier, y);
            //ADD LINE
            path_points_lines[line_counter].transform.position = new Vector3((path_points[point_counter + 1].x + path_points[point_counter].x) / 2, y, 10f);
            size_of_line = Mathf.Abs(path_points[point_counter + 1].x - path_points[point_counter].x);
            path_points_lines[line_counter].transform.localScale = new Vector3(size_of_line * 100f, 5, 1);
            path_points_lines[line_counter].SetActive(true);
            //ADD CURVE
            path_points_curves[curve_counter].transform.position = new Vector3(path_points[point_counter + 1].x + (.25f * direction_multiplier), path_points[point_counter + 1].y + .02f, 10f);
            path_points_curves[curve_counter].transform.localScale = new Vector3(direction_multiplier, 1.04f, 1);
            path_points_curves[curve_counter].SetActive(true);

            line_counter += 1;
            curve_counter += 1;
            point_counter += 2;
            direction_multiplier *= -1f;
        }
        path_points[12] = new Vector2(path_points[11].x, -3);
        path_points_lines[line_counter].transform.position = new Vector3((path_points[point_counter + 1].x + path_points[point_counter].x) / 2, path_points[point_counter + 1].y, 10f);
        size_of_line = Mathf.Abs(path_points[point_counter + 1].x - path_points[point_counter].x);
        path_points_lines[line_counter].transform.localScale = new Vector3(size_of_line * 100f, 5, 1);
        path_points_lines[line_counter].SetActive(true);
        for (int i = 0; i < path_points.Length; i++)
        {
            path_points_markers[i].transform.position = path_points[i];
            path_points_markers[i].GetComponent<SpriteRenderer>().color = new Color(i / 15f, i / 15f, i / 15f);
            //  path_points_markers[i].SetActive(true);
        }

        path_end_quarter.transform.localScale = new Vector3(direction_multiplier, 1, 1);
        path_end_quarter.transform.position = new Vector3(direction_multiplier * -.22f,-3.225f, 10);
        path_start_quarter.transform.localScale = new Vector3(-direction_multiplier, -1, 1);
        path_start_quarter.transform.position= new Vector3(direction_multiplier * .22f, 3.225f, 10);

    }
    public void make_ship(int type)
    {

        ships[ship_counter].GetComponent<SpriteRenderer>().sprite = ship_sprites[type];
        ships[ship_counter].transform.localScale = new Vector3(ValuesManager.getScale(type)/4, ValuesManager.getScale(type)/3, 1);
        ships[ship_counter].transform.position = new Vector3(0, 5.5f);
        ships[ship_counter].SetActive(true);
        ships[ship_counter].GetComponent<ShipManager>().speed = ValuesManager.getSpeed(type)/10f;
        ships[ship_counter].GetComponent<ShipManager>().health = ValuesManager.getLife(type);
        //ships[ship_counter].name = "" + (ValuesManager.getSpeed(type) * 100) / 1;
        ships[ship_counter].AddComponent<BoxCollider2D>();

        ships[ship_counter].GetComponent<ShipManager>().scale_x = ValuesManager.getScale(type);

        ships[ship_counter].GetComponent<ShipManager>().scale_z = 1;// = (ValuesManager.getScale(type);
        ships[ship_counter].GetComponent<ShipManager>().scale_y = ValuesManager.getScale(type);//e = new Vector3(ships[ship_counter].GetComponent<ShipManager>().scale_x / 4, ships[ship_counter].GetComponent<ShipManager>().scale_y * 3, ships[ship_counter].GetComponent<ShipManager>().scale_z);
        ships[ship_counter].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        //ships[ship_counter].transform.Translate(new Vector2(0, 2f));
        ship_counter++;
       // if (ship_counter == ships.Length) ship_counter = 0;
    }


    public void make_satellite(Vector2 pos)
    {
        GameObject sat = Instantiate(Resources.Load("satellites/satellite"), pos, this.transform.rotation) as GameObject;
        sat.transform.SetParent(this.transform);
        satellites.Add(sat);
    }
    public void modify_satellite(int choice)
    {
        if (chosen >= 0)
        {
            levell.SetActive(false); levelll.SetActive(false);
            switch (choice)
            {
                case 1:
                    if (satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType == "" && money >= 6)
                    {
                        startstart = true;
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType = "ConventionalRifle";
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().rateOfFire = 2;
                        money -= 6;
                        satellites[chosen].GetComponent<SpriteRenderer>().sprite = station_types[0];

                        sat_UI.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Cannon\n10$";
                        sat_UI.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Damage\n4$";
                        sat_UI.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Range\n4$";

                    }
                    else if (satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType == "ConventionalRifle" && money >= 10)
                    {
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType = "ConventionalCannon";
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().rateOfFire = 10;
                        money -= 10;

                    }
                    else if (satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType == "PlasmaRifle" && money >= 16)
                    {
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType = "PlasmaCannon";
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().rateOfFire = 12;
                        money -= 16;

                    }
                    else if (satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType == "LaserBeam" && money >= 20)
                    {
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType = "LaserCannon";
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().rateOfFire = 14;
                        money -= 20;


                    }
                    break;
                case 2:
                    if (satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType == "" && money >= 10)
                    {
                        startstart = true;
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType = "PlasmaRifle";
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().rateOfFire = 3;
                        money -= 10;
                        satellites[chosen].GetComponent<SpriteRenderer>().sprite = station_types[1];

                        sat_UI.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Cannon\n16$";
                        sat_UI.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Damage\n4$";
                        sat_UI.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Range\n4$";
                    }
                    else
                    {
                        if (money >= 4)
                        {
                            money -= 4;
                            satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().damage_multiplier++;
                        }
                    }
                    break;
                case 3:
                    if (satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType == "" && money >= 14)
                    {
                        startstart = true;
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType = "LaserBeam";
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().rateOfFire = 4;
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().range++;
                        money -= 14;
                        satellites[chosen].GetComponent<SpriteRenderer>().sprite = station_types[2];

                        sat_UI.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Cannon\n20$";
                        sat_UI.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Damage\n4$";
                        sat_UI.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Range\n4$";
                    }
                    else if (money >= 4)
                    {
                        money -= 4;
                        satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().range++;
                        circle.transform.localScale = satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().range * new Vector2(1, 1);// * 100f;


                    }
                    break;
            }

        }
        else
        {
           
        }

    }

    float tap_duration = 0;
    public bool clickOn(float xMin, float xMax, float yMin, float yMax, Vector2 mousePosition, Vector2 otherPosition)
    {
        if (mousePosition.x + xMin < otherPosition.x && mousePosition.x + xMax > otherPosition.x) if (mousePosition.y + yMin < otherPosition.y && mousePosition.y + yMax > otherPosition.y) return true;
        return false;
    }
    public void exit()
    {
            SceneManager.LoadScene("Menu");
        
    }
    public void buy_satellite()
    {
        chosen = -1;
        if (money >= 5)
        {
            if (money == 25)
            {
                levell.SetActive(false); levelll.SetActive(false);
            }
            money -= 5;
            purchased_sat = true;
            just_clicked = true;
            GameObject.Find("SatText").GetComponent<Text>().text = "Click Anywhere";
        }
    }
    float counter = .01f;
    // Update is called once per frame
    bool ships_disabled = false;
    public bool end_game = false;
    float end_counter = .01f;
    void Update()
    {
        if (end_game)
        {
            end_counter += .1f * Time.deltaTime;
            GameObject.Find("Button").GetComponent<Image>().color = new Color(255, 255, 255, 1 - end_counter);
            GameObject.Find("Button").transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 1 - end_counter);
            GameObject.Find("Button (1)").GetComponent<Image>().color = new Color(255, 255, 255, 1 - end_counter);
            GameObject.Find("Image").GetComponent<Image>().color = new Color(255, 255, 255, 1 - end_counter);
            GameObject.Find("Image (5)").GetComponent<Image>().color = new Color(255, 255, 255, 1 - end_counter);
            GameObject.Find("Image (5)").transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 1 - end_counter);
            levell.SetActive(true); levelll.SetActive(true);
            levelll.GetComponent<Text>().color = new Color(49/255f  , 49/255f,49/255f, end_counter);
            levell.GetComponent<Text>().color = new Color(194/255f, 194/255f, 194/255f, end_counter);
            levelll.GetComponent<Text>().text = "The Scientists got the Graviton Accelerator running, repelling the Alien Invasion!";
            levell.GetComponent<Text>().text = "The Scientists got the Graviton Accelerator running, repelling the Alien Invasion!";
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("Menu");
            }
            //for(int i=0;i!=100;i+=20)
            if (!ships_disabled)
            {
                for (int i = 0; i != ships.Length; i++)
                    ships[i].GetComponent<ShipManager>().enabled = false;
                ships_disabled = true;
                Instantiate(Resources.Load("wave"), new Vector2(0,-5f), this.transform.rotation);
            }
            for (int i = 0; i < 1000; i++)
            {
                if (ships[i].activeSelf)
                {
                    if (ships[i].transform.position.y < GameObject.Find("wave(Clone)").GetComponent<Wave>().top)
                        ships[i].transform.position = new Vector2(ships[i].transform.position.x, ships[i].transform.position.y + 2f * Time.deltaTime);
                    else if (ships[i].transform.position.y > 10)
                        Destroy(ships[i]);
                }
            }
        }
        if (orders.Count == 0)
        {
            int number_of_ships_alive = 0;
            for (int i = 0; i < 100; i++) if (ships[i].activeSelf) number_of_ships_alive++;
            if (number_of_ships_alive == 0)
            {
                level++;
                orders = ValuesManager.getLevel(level);
                game_time = 0;
                //finished
            }
        }
        //timer += Time.deltaTime;

        if (counter < 10)
        {
            counter += counter * Time.deltaTime * 5;
            GameObject.Find("fade").GetComponent<Image>().color = new Color(0, 0, 0, 1 - counter);
            
        }
        else
        {
            if (GameObject.Find("fade") != null) Destroy(GameObject.Find("fade"));
        }
            //print(chosen_satellite);
            money_display.text = money + "$";
        if (chosen >= 0)
        {
            highlighter.transform.Rotate(new Vector3(0, 0, .1f));
            highlighter.transform.position = satellites[chosen].transform.position;
            highlighter.SetActive(true);
            circle.SetActive(true);
            circle.transform.position = satellites[chosen].transform.position;// * 100f;
            circle.transform.localScale = satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().range * new Vector2(1, 1);// * 100f;
        }
        else
        {
            circle.SetActive(false);
            highlighter.SetActive(false);
            sat_UI.SetActive(false);
        }
        //temp
        if (Input.GetMouseButtonDown(0))
        {
        }
        if (Input.GetMouseButton(0))
        {
            tap_duration += Time.deltaTime;
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!just_clicked)
            {
                bool missed = true;

                if (true)
                {
                    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
  
                    if (purchased_sat)
                    {

                        GameObject.Find("SatText").GetComponent<Text>().text = "Launch Satellite; 5$";
                        purchased_sat = false;
                        // Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        pos.y = Mathf.Round(pos.y - .5f) + .5f;
                        GameObject rocket = Instantiate(Resources.Load("Rocket"), new Vector2(-1.95f + (Random.value / 2.5f), -4.75f), Quaternion.Euler(0, 0, -11f)) as GameObject;
                        rocket.GetComponent<RocketManagement>().turret_targetPoint = pos;

                    }
                    if (sat_UI.activeSelf)
                    {
                        //print(Input.mousePosition);
                        if (clickOn(-225, 255, -50, 50, Input.mousePosition, sat_UI.transform.GetChild(0).gameObject.GetComponent<RectTransform>().position ))
                        {
                            modify_satellite(1);
                            missed = false;
                        }
                        if (clickOn(-255, 255, -50, 50, Input.mousePosition, sat_UI.transform.GetChild(1).gameObject.GetComponent<RectTransform>().position))
                        {
                            modify_satellite(2);
                            missed = false;
                        }
                        if (clickOn(-255, 255, -50, 50, Input.mousePosition, sat_UI.transform.GetChild(2).gameObject.GetComponent<RectTransform>().position))
                        {
                            modify_satellite(3);
                            missed = false;
                        }
                        if (clickOn(-20, 20, -20, 20, Input.mousePosition, sat_UI.transform.GetChild(6).gameObject.GetComponent<RectTransform>().position))
                        {
                            //sat_UI.SetActive(false); circle.SetActive(false);
                            chosen = -1;
                        }
                    }
                    
                    for (int i = 0; i < satellites.Count; i++)
                    {
                        float distance = Mathf.Sqrt(Mathf.Pow(pos.x - satellites[i].transform.position.x, 2) + Mathf.Pow(pos.y - satellites[i].transform.position.y, 2));
                        if (distance < .4f) //IF OVER A SATELLITE.... is only needed thing
                        {
                            missed = false;
                            sat_UI.SetActive(true);
                            if (satellites[i].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().firingType == "")
                            {
                                sat_UI.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Gun\n6$";
                                sat_UI.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Plasma\n10$";
                                sat_UI.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Laser\n$14";
                            }
                            chosen = i;
                            //print("yes");
                            circle.SetActive(true);
                            //circle.transform.SetParent(satellites[i].transform);
                            circle.transform.position = satellites[chosen].transform.position;// * 100f;
                            circle.transform.localScale = satellites[chosen].transform.GetChild(0).gameObject.GetComponent<SatelliteManager>().range * new Vector2(1, 1);// * 100f;
                            //Vector2 adjusted = GUIUtility.ScreenToGUIPoint(satellites[i].transform.position);
                            //sat_UI.GetComponent<RectTransform>().position = new Vector2(adjusted.x + 100, adjusted.y);
                            //open ui for satellite
                            // satellites[i].SetActive(false);
                        }
                    }
                    if (missed)
                    {
                        //sat_UI.SetActive(false); circle.SetActive(false);
                        chosen = -1;
                    }






                }
                tap_duration = 0;
            }
            else
            {
                just_clicked = false;
            }
        }

        //send ships
        if (satellites.Count > 0)
        {
            if (money == 20)
            {
                levell.SetActive(true); levelll.SetActive(true);
                if (hint_shown == false)
                {
                    levelll.GetComponent<Text>().text = "(Click on Satellite to upgrade)";
                    levell.GetComponent<Text>().text = "(Click on Satellite to upgrade)";
                    hint_shown = true;
                }
            }
            if (startstart)
            {
                game_time += Time.deltaTime;
                if (game_time < 1)
                {
                    levelll.SetActive(true);
                    levell.SetActive(true);

                    levelll.GetComponent<Text>().text = "Wave " + (level + 1);
                    levell.GetComponent<Text>().text = "Wave " + (level + 1);
                }
                if (game_time > 1 && game_time < 2)
                {
                    levelll.SetActive(false);
                    levell.SetActive(false);
                }
                if (level == 4 && game_time >= 25)
                {
                    end_game = true;
                }
                if (((int)(game_time * 10f)) % 10 == 0) game_timer.text = ((int)(game_time * 10f)) / 10f + ".0";
                else game_timer.text = ((int)(game_time * 10f)) / 10f + "";//).ToString("E");
                if (orders.Count > 0)
                {
                    while (orders[0].x <= game_time)
                    {
                        make_ship((int)orders[0].y);
                        orders.RemoveAt(0);
                        if (orders.Count == 0) break;
                    }
                }
            }
        }
    }


    public GameObject findTarget(Vector2 satellite_position, float range)
    {
        float closestShipDistance = 100f;
        int closestShipID = -1;
        for (int i = 0; i < ships.Length; i++)
        {
            if (ships[i].activeSelf)
            {
                float distance = Mathf.Sqrt(Mathf.Pow(satellite_position.x - ships[i].transform.position.x, 2) + Mathf.Pow(satellite_position.y - ships[i].transform.position.y, 2));
                if (distance < range)
                {
                    if (distance < closestShipDistance)
                    {
                        closestShipDistance = distance;
                        closestShipID = i;
                    }
                }
            }
        }
        if (closestShipID != -1) return ships[closestShipID];
        else return null;
    }

    /*  public static GameObject findTarget(Vector2 shipLocation, float leftAimAngle, float rightAimAngle, int team)
       {

           GameObject temp = GameObject.Find("CodeBase");
           float closestShipDistance = 100000f;
           int closestShipID = -1;
           int closestShipTeam = -1;
           for (int otherTeams = 0; otherTeams < 10; otherTeams++)
           {
               if (TeamManager.getStanding(team, otherTeams) == -1)
               {
                   List<GameObject> possibleTargets = TeamManager.team_lists[otherTeams];
                   for (int i = 0; i < possibleTargets.Count; i++)
                   {
                       if (possibleTargets[i] != null)
                       {
                           float angle = findAngle(shipLocation, possibleTargets[i].transform.position);

                           if (((rightAimAngle < leftAimAngle) && (angle < leftAimAngle && angle > rightAimAngle)) || ((rightAimAngle > leftAimAngle) && (angle < leftAimAngle || angle > rightAimAngle)))
                           {
                               float distance = Mathf.Sqrt(Mathf.Pow(shipLocation.x - possibleTargets[i].transform.position.x, 2) + Mathf.Pow(shipLocation.y - possibleTargets[i].transform.position.y, 2));
                               if (distance < closestShipDistance)
                               {
                                   closestShipDistance = distance;
                                   closestShipID = i;
                                   closestShipTeam = otherTeams;
                               }
                           }
                       }
                   }
               }
           }
           if (closestShipID != -1) return TeamManager.team_lists[closestShipTeam][closestShipID];
           else return null;
       } */

    public  float findAngle(Vector2 item, Vector2 target)
    {
        float angle = Mathf.Rad2Deg * Mathf.Atan((target.y - item.y) / (target.x - item.x));
        //Configuring produced angle to 0-360
        if (target.y < item.y)
        {
            if (angle < 0) angle += 180f;
            angle += 180f;
        }
        else if (angle < 0) angle += 180f;
        return angle;

    }


    public  float GetAngle(float X1, float Y1, float X2, float Y2)
    {
        if (Y2 == Y1)
        {
            return (X1 > X2) ? 180 : 0;
        }
        if (X2 == X1)
        {
            return (Y2 > Y1) ? 90 : 270;
        }
        float tangent = (X2 - X1) / (Y2 - Y1);
        double ang = (float)Mathf.Atan(tangent) * 57.2958;
        if (Y2 - Y1 < 0) ang -= 180;
        return (float)ang;

    }


}



















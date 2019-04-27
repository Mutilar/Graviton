using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShipManager : MonoBehaviour
{
    public int current_point = 0;
    public float speed = 2f;
    //public Vector2[] path_points;
    public int is_travellingRight = -1;//1 = right, -1 = left
    public bool is_changingLine = true;
    bool is_started = true, end = false;
    float line_offset = 1f;
    public float semicircle_distanceTravelled = 0f;
    Vector2[] path_points;

    Vector3 offset;




    public float scale_counter = 0;
    public float health = 20;
    float originalHealth = -1;
    bool dying;
    float deathCounter = 0;
    public float scale_x, scale_y, scale_z;
    public bool warping = true;
    // Use this for initialization
    void Start()
    {

        float offset_amount = .2f;
        offset = new Vector3(Random.value * offset_amount - offset_amount / 2, Random.value * offset_amount - offset_amount / 2, 0);

        path_points = this.transform.parent.GetComponent<GameManager>().path_points;
        is_travellingRight = -1 * (int)this.transform.parent.GetComponent<GameManager>().direction_multiplier;


        this.transform.position += offset;
    }
    public void hurt(float amount)
    {
        if (originalHealth == -1) originalHealth = health;
        health -= amount;
        this.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        if (health <= 0) delete();

    }
    public void delete()
    {

        dying = true;

        //Destroy(this.gameObject, 2f);
        //Destroy(this, 2f);
    }
    void FixedUpdate()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position -= offset;
        if (warping)
        {
            if (this.transform.position.y > 3.5f) this.transform.Translate(new Vector2(0, .15f ));
            else this.transform.position = new Vector2(0, 3.5f);
            scale_counter += .2f;
            if (this.transform.localScale.x == scale_x)
            {
                warping = false;
            }
            else
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x + scale_counter * Time.deltaTime, this.transform.localScale.y - 4 * scale_counter * Time.deltaTime, scale_z);
                if (this.transform.localScale.x > scale_x)
                {
                    this.transform.localScale = new Vector3(scale_x, scale_y, scale_z);
                }
            }
        }
             //   if (ships[i].activeSelf && ships[i].GetComponent<ShipManager>().warping)
            //{
                //ships[i].GetComponent<ShipManager>().scale_counter += .2f;
                //if (ships[i].GetComponent<ShipManager>().warping && this.transform.localScale.x == ships[i].GetComponent<ShipManager>().scale_x)
               // {

         //           this.transform.localScale = new Vector3(ships[i].GetComponent<ShipManager>().scale_x / 4, ships[i].GetComponent<ShipManager>().scale_y * 4, ships[i].GetComponent<ShipManager>().scale_z);
          //          ships[i].GetComponent<ShipManager>().warping = false;
          //      }
            //    if (ships[i].GetComponent<ShipManager>().scale_counter < 4)
             //   {
              //      if (this.transform.localScale.x > ships[i].GetComponent<ShipManager>().scale_x)
               //         this.transform.localScale = new Vector3(ships[i].GetComponent<ShipManager>().scale_x, ships[i].GetComponent<ShipManager>().scale_y, ships[i].GetComponent<ShipManager>().scale_z);
                //    else
                 //       this.transform.localScale = new Vector3(this.transform.localScale.x + ships[i].GetComponent<ShipManager>().scale_counter * Time.deltaTime, this.transform.localScale.y - 4 * ships[i].GetComponent<ShipManager>().scale_counter * Time.deltaTime, ships[i].GetComponent<ShipManager>().scale_z);

//                }
  //              else
    //            {
      ////              this.transform.localScale = new Vector3(ships[i].GetComponent<ShipManager>().scale_x, ships[i].GetComponent<ShipManager>().scale_y, ships[i].GetComponent<ShipManager>().scale_z);
          //      }

            
        
        if (!warping)
        {
            //print(warp_tick_duration);
            if (dying)
            {
                if (Random.value > .8f)
                {
                    ProjectileManager.getExplosion(this.transform.position, this.GetComponent<SpriteRenderer>().sprite.bounds.size, Random.value * 2f * deathCounter);
                    if (Random.value > .75f)
                    {
                        deathCounter += .15f;
                        if (deathCounter > .5f)
                        {
                            this.transform.parent.GetComponent<GameManager>().money += 1;
                            this.gameObject.SetActive(false);
                            warping = true;
                        }
                    }

                }
            }

            if (end)
            {
                //LOSE LIFE!!!!!!!!!!!1
                this.gameObject.SetActive(false);
                warping = true;
            }
            if (!end)
            {
                // GameObject.Find("DEBUG").GetComponent<Text>().text = path_points[current_point].x + " " + path_points[current_point].y ;
                if (is_changingLine)
                {
                    float semicircle_distance = Mathf.Abs(line_offset * Mathf.PI / 2);//distance around semicircle
                    if (is_started)
                    {
                        semicircle_distanceTravelled = semicircle_distance / 2;
                        is_started = false;
                    }

                    float new_angle = (semicircle_distanceTravelled + speed * Time.deltaTime) / semicircle_distance * Mathf.PI;//angle in radians of object around circle
                    if (is_travellingRight == -1) this.transform.rotation = Quaternion.Euler(0, 0, is_travellingRight * -90 + Mathf.Rad2Deg * new_angle);
                    else this.transform.rotation = Quaternion.Euler(0, 0, -90 + Mathf.Rad2Deg * -new_angle);
                    //GameObject.Find("DEBUG").GetComponent<Text>().text = new_angle + " ";
                    if (this.transform.position.y < -3.45f)
                    {
                        end = true;
                    }
                    if (new_angle > Mathf.PI || current_point == path_points.Length - 1 && new_angle > Mathf.PI / 2)
                    {
                        if (current_point != 0)
                            current_point++;
                        this.transform.position = new Vector3(path_points[current_point].x, path_points[current_point].y, 0);
                        is_changingLine = false;
                        semicircle_distanceTravelled = 0;
                        is_travellingRight *= -1;
                        semicircle_distance = 0;
                    }
                    this.transform.position = new Vector3(this.transform.position.x + (is_travellingRight) * speed * Time.deltaTime * Mathf.Cos(new_angle), this.transform.position.y - speed * Time.deltaTime * Mathf.Sin(new_angle), 0);
                    semicircle_distanceTravelled += speed * Time.deltaTime;
                }
                else//Mathf.Abs(Mathf.Abs(this.transform.position.x) - Mathf.Abs(path_points[current_point + 1].x)) < Time.deltaTime * speed)//speed*Time.deltaTime)
                {
                    // GameObject.Find("DEBUG").GetComponent<Text>().text += " " + is_travellingRight + " ";//  path_points[current_point].x + " " + path_points[current_point].y;
                    this.transform.rotation = Quaternion.Euler(0, 0, is_travellingRight * -90);

                    this.transform.Translate(0, speed * Time.deltaTime, 0);
                    if (is_travellingRight == 1)
                    {
                        if (this.transform.position.x > path_points[current_point + 1].x)
                        {
                            //   GameObject.Find("DEBUG").GetComponent<Text>().text += "\n" + transform.position.x + " " + path_points[current_point + 1] ;//  path_points[current_point].x + " " + path_points[current_point].y;

                            current_point += 1;
                            this.transform.position = path_points[current_point];
                            is_changingLine = true;
                        }
                    }
                    else
                    {
                        if (this.transform.position.x < path_points[current_point + 1].x)
                        {
                            current_point += 1;
                            this.transform.position = path_points[current_point];
                            is_changingLine = true;
                        }
                    }
                }
            }
         
        }
        this.transform.position += offset;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketManagement : MonoBehaviour {


    public Vector2 turret_targetPoint;
    public Vector2 rocket_spawnPoint, rocket_straightFlightTarget, rocket_rotateTarget;
    Quaternion rocket_startRotation;
    float x_boundRight = 5.625f;
    float rocket_speed = .25f;
    public int rocket_stage = 1;
    float waitTimer = 0f;
    bool turret_isDropped = false;
    public float spawn_count = .1f;
    void Update()
    {
        if (rocket_stage != 0)
            rocket_move();
    }
    void Start()
    {
        rocket_spawnPoint = this.transform.position;
        rocket_straightFlightTarget = new Vector2(rocket_spawnPoint.x + 4 * Mathf.Cos(this.transform.rotation.eulerAngles.z), rocket_spawnPoint.y - 4 * Mathf.Sin(this.transform.rotation.eulerAngles.z));
        rocket_startRotation = this.transform.rotation;
        this.transform.localScale = new Vector2(0,0);

    }
    void rocket_move()
    {
        if (rocket_stage == 1)
        {
            if (this.transform.localScale.x < 1.5f)
            {
                spawn_count += spawn_count * Time.deltaTime * 4f;
                this.transform.localScale = new Vector2(spawn_count, spawn_count*1);
            }
            else
                this.transform.localScale = new Vector2(1.5f,1.5f);



            this.transform.Translate(new Vector2(0, rocket_speed * Time.deltaTime));
                if (this.transform.position.y > -4f)
                rocket_stage++;

        }
        else if (rocket_stage == 2)
        {
            if (this.transform.rotation.eulerAngles.z < 180f || this.transform.rotation.eulerAngles.z > 270f)
                this.transform.Rotate(0, 0, -70f * Time.deltaTime);
            else
                rocket_stage++;

            this.transform.Translate(new Vector2(0, rocket_speed * Time.deltaTime));

        }
        else if (rocket_stage == 3)
        {
            this.transform.position = new Vector3(this.transform.position.x + rocket_speed * Time.deltaTime, this.transform.position.y + rocket_speed * Time.deltaTime * .05f, 0);
            if (this.transform.position.x > x_boundRight)
            {
                rocket_stage++;
                this.transform.Translate(10f, 10f, 0);
            }

        }
        else if (rocket_stage == 4)
        {
            if (waitTimer < 1.5f)
                waitTimer += Time.deltaTime;
            else
            {
                this.transform.position = new Vector3(-x_boundRight, turret_targetPoint.y, 0);
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270f));
                rocket_stage++;
            }
        }
        else
        {
            rocket_speed = 3f;
            this.transform.Translate(0, rocket_speed * Time.deltaTime, 0);
            if (this.transform.position.x >= turret_targetPoint.x && !turret_isDropped)
            {
                GameObject.Find("CodeBase").GetComponent<GameManager>().make_satellite(turret_targetPoint);
                turret_isDropped = true;
            }
            if (this.transform.position.x > x_boundRight)
            {
                Destroy(this.gameObject);
                Destroy(this);
                this.transform.position = rocket_spawnPoint;
                this.transform.rotation = rocket_startRotation;
                rocket_stage = 0;
                rocket_speed = .15f;
                waitTimer = 0;
            }
        }
        rocket_speed += 1 * Time.deltaTime;
    }
}
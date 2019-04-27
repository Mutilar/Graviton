using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteManager : MonoBehaviour {

    
    //private float leftAngle;
    bool clockwiseTurning = true;
    //private float rightAngle;
    bool counterClockwiseTurning = true;
    //private int numberAngle;
    //Is the thing limited by degrees of rotation (Or is it free to move around in any direction
   // private bool angleLimit = false;
    //Is the thing able to turn, move around (Or is it static, straight forward
    public bool turnable = true;
    //Is the thing able to shoot, fire objects
    public bool shooting = false;

    public float damage_multiplier = 1;
    public float range = 1;
    public int ammoAmount = 1000000;
    public bool ammo = false;
    //How fast does the object fire
    public float rateOfFire = 5;
    public float turnSpeed;
    //What object does the thing shoot
    public string firingType = "";
    public string weaponType = "";

    public GameObject target;
    Vector3 aimPoint;
    int targetIndex = -1;
    float cooldown;

    bool onTarget = false;
   
    //public ProjectieFireCharacteristicsDataWrapper ProjectileFireCharacteristics;
    bool firingClip;
    int current_shot;
    public float spawn_count = .1f;
    void Start()
    {
        aimPoint = new Vector3(0, 0, 0);
        rateOfFire = rateOfFire * 4;
        shooting = true;
        //transform.Rotate(0, 0, this.GetComponentInParent<Hardpoint>().mainAngle - 90);
        //}
        this.transform.parent.localScale = new Vector2(0, 0);
    }



    public bool is_orbitting = false;
    float orbit_speed = 1f;
    float timer = 0;
    void Update()
    {
        if (timer < 0)
        {
            timer = 0;
            this.transform.parent.position = new Vector2(-3f, this.transform.parent.position.y);
        }
        if (timer == 0)
        {
            if (this.transform.parent.localScale.x < 1)
            {
                spawn_count += spawn_count *  Time.deltaTime * 10f;
                this.transform.parent.localScale = new Vector2(spawn_count, spawn_count);
            }
            else
                this.transform.parent.localScale = new Vector2(1, 1);

            if (target != null && target.activeSelf == false)
            {
                chanceReaim();
            }
            if (turnable) rotate();
            if (shooting && ammoAmount > 0)
            {

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    firingClip = true;
                }

                if (cooldown <= 0f)
                {
                    //cooldown = item.coefficients[Values.FIRERATE];
                    cooldown = rateOfFire;
                    shoot();
                    ammoAmount--;
                }
                cooldown -= Time.deltaTime * 20;
            }
            if (is_orbitting)
            {
                if (this.transform.parent.position.x > 3)
                {
                    timer = 4f;
                    this.transform.parent.Translate(100f, 0, 0);
                }
                this.transform.parent.position = new Vector2(this.transform.parent.position.x + orbit_speed * Time.deltaTime, this.transform.parent.position.y);
            }
        }
        else
            timer -= Time.deltaTime;
    }

    private void chanceReaim(float chance)
    {
        if (Random.value < chance)
        {
            chanceReaim();
        }
    }
    private void chanceReaim()
    {
        target = this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<GameManager>().findTarget(this.transform.position, range);
    }

    private void rotate()
    {
        Vector3 turretPoint = this.GetComponentInChildren<Transform>().position;
        Vector2 clickPoint;

        if (target != null)
        {
            /*Correction for leading targets*/
            float projectileSpeed = 10;//item.coefficients[Values.VELOCITY];//Values.getProjectileElements(firingType).Projectile_Speed * Values.getEquipmentElements(weaponType).Projectile_SpeedModifier ;// GetComponent<projectileFuntion>().speed;
            float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - target.transform.position.x, 2) + Mathf.Pow(transform.position.y - target.transform.position.y, 2));

            float time = distance / (projectileSpeed);
            float enemyShipSpeed = target.GetComponent<ShipManager>().speed;   //keep in mind this assumes the ships stay at a constant speed

            float enemyShipMovement = time * enemyShipSpeed;
            float enemyShipRotation = (target.GetComponentInParent<Transform>().rotation.eulerAngles.z + 90) % 360;

            float xModifier = Mathf.Cos(enemyShipRotation * Mathf.Deg2Rad) * enemyShipMovement;
            float yModifier = Mathf.Sin(enemyShipRotation * Mathf.Deg2Rad) * enemyShipMovement;

            clickPoint.x = target.transform.position.x + xModifier;
            clickPoint.y = target.transform.position.y + yModifier;

            {
                transform.up = new Vector2(clickPoint.x - turretPoint.x, clickPoint.y - turretPoint.y);
                shooting = true;
                onTarget = true;
            }

        }
        else
        {
         //   if (angleLimit) shooting = false;
            chanceReaim(.1f);
        }
    }


    private bool shootMechanics()
    {
       
        return true;
    }

    private void shoot()
    {
        if (target != null)
        {
            if (shootMechanics())
            {
                float distance = Mathf.Sqrt(Mathf.Pow(this.transform.position.x - target.transform.position.x, 2) + Mathf.Pow(this.transform.position.y - target.transform.position.y, 2));
                if (distance > range)
                {
                    target = null;
                    chanceReaim();
                }
                else
                {
                    //  for (int i = 0; i < item.coefficients.Length; i++)
                    // {
                    //    print(i + " " + item.coefficients[i]);
                    // }
                    Vector3 rotation_value = this.transform.rotation.eulerAngles;
                    //rotation_value.z += item.coefficients[Values.ACCURACY] * (Random.value - .5f);
                    if (firingType != "")
                    {
                        ProjectileManager.getProjectile(firingType, this.transform.position, Quaternion.Euler(rotation_value), damage_multiplier);
                        ProjectileManager.getPlayer(firingType, this.transform.position);
                    }
                }
              
            }
        }
        else
        {
            //firingClip = false;
        }


    }

}



//current = this.transform.rotation.eulerAngles.z + 90f;

//current += 360;
//current = current % 360f;
//  print(current + " " + leftAngleTemp + " " + rightAngleTemp);
// current += 360;
// leftAngleTemp += 360;
// rightAngleTemp += 360;


/*   else if (current > leftAngleTemp)
   {
       transform.rotation = Quaternion.Euler(0, 0, leftAngleTemp-90);
   }
   else if(current < rightAngleTemp)
   {
       transform.rotation = Quaternion.Euler(0, 0, rightAngleTemp - 90);
   }
  */

//Checking to see if it is at the edge of its range, disabling it if it is
/*  if (angleLimit)
  {
      counterClockwiseTurning = true;
      clockwiseTurning = true;
      shooting = true;
      //"5" is a buffer to insure it isn't referring to the other side
      if (current > leftAngleTemp && current < leftAngleTemp + 2.1f * Time.deltaTime * 100 * turnSpeed)
      {
          counterClockwiseTurning = false;
          shooting = false;
          chanceReaim();
      }
      if (current > rightAngleTemp && current < rightAngleTemp + 2.1f * Time.deltaTime * 100 * turnSpeed)
      {
          clockwiseTurning = false;
          shooting = false;
          chanceReaim();
      }
  }*/

/*	float reverseDirectionMultiplier = 1f;
    //Rotating if not close enough
    if (Mathf.Abs (current - angle) > 1f) 
    { 
        onTarget = false;
        //3f
        //Finding fastest way to desired angle
        if (Mathf.Abs (current - angle) > 180f && Mathf.Abs (current - angle) < 360f)
            reverseDirectionMultiplier = -1f;
        //Rotating in desired direction
        if (current > angle)
            reverseDirectionMultiplier *= -1f;
        //3
    //	if (!angleLimit)
    //		turnSpeed = .5f;//8f*Random.value;
        if ((reverseDirectionMultiplier > 0 && counterClockwiseTurning) || (reverseDirectionMultiplier < 0 && clockwiseTurning))
            transform.Rotate (0, 0, Time.deltaTime * 100 * turnSpeed * reverseDirectionMultiplier);	
    }*/
//else onTarget = true;


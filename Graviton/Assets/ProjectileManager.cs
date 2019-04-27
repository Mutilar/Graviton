using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    /*How many max of each type there are */
    private static int PlasmaRifleAmount = 250;
    private static int PlasmaCannonAmount = 25;
    private static int PlasmaRocketAmount = 50;
    private static int PlasmaMissileAmount = 50;
    private static int PlasmaClusterMissileAmount = 25;
    private static int ConventionalRifleAmount = 250;
    private static int ConventionalCannonAmount = 25;
    private static int ConventionalFlakCannonAmount = 25;
    private static int ConventionalRocketAmount = 50;
    private static int ConventionalClusterMissileAmount = 25;
    private static int ConventionalMissileAmount = 100;
    private static int LaserBeamAmount = 50;
    private static int LaserCannonAmount = 25;
    private static int missileAmount;
    private static int missileLargeAmount;
    private static int missileClusterAmount;
    /* GameObjects of each type */
    public static GameObject[] PlasmaRifle = new GameObject[PlasmaRifleAmount];
    public static GameObject[] PlasmaCannon = new GameObject[PlasmaCannonAmount];
    public static GameObject[] PlasmaRocket = new GameObject[PlasmaRocketAmount];
    public static GameObject[] PlasmaMissile = new GameObject[PlasmaMissileAmount];
    public static GameObject[] PlasmaClusterMissile = new GameObject[PlasmaClusterMissileAmount];
    public static GameObject[] ConventionalRifle = new GameObject[ConventionalRifleAmount];
    public static GameObject[] ConventionalCannon = new GameObject[ConventionalCannonAmount];
    public static GameObject[] ConventionalFlakCannon = new GameObject[ConventionalFlakCannonAmount];
    public static GameObject[] ConventionalRocket = new GameObject[ConventionalRocketAmount];
    public static GameObject[] ConventionalClusterMissile = new GameObject[ConventionalClusterMissileAmount];
    public static GameObject[] ConventionalMissile = new GameObject[ConventionalMissileAmount];
    public static GameObject[] LaserBeam = new GameObject[LaserBeamAmount];
    public static GameObject[] LaserCannon = new GameObject[LaserBeamAmount];
    public static GameObject[] missile = new GameObject[100];
    public static GameObject[] missileLarge = new GameObject[50];
    public static GameObject[] missileCluster = new GameObject[20];
    /* Which GameObject is to be used next */
    public static int PlasmaRifleCount;
    public static int PlasmaCannonCount;
    public static int PlasmaRocketCount;
    public static int PlasmaMissileCount;
    public static int PlasmaClusterMissileCount;
    public static int ConventionalRifleCount;
    public static int ConventionalCannonCount;
    public static int ConventionalFlakCannonCount;
    public static int ConventionalRocketCount;
    public static int ConventionalClusterMissileCount;
    public static int ConventionalMissileCount;
    public static int LaserBeamCount;
    public static int LaserCannonCount;
    public static int missileCount;
    public static int missileLargeCount;
    public static int missileClusterCount;


    public static GameObject[] audioPlayer = new GameObject[50];
    public static int audioPlayerCount;

    public static GameObject[] explosions = new GameObject[50];
    public static int explosionsCount;

    public static RuntimeAnimatorController[] explosion_variations = new RuntimeAnimatorController[6];

    void Start()
    {
        for (int i = 0; i < audioPlayer.Length; i++)
        {
            audioPlayer[i] = Instantiate(Resources.Load("AudioPlayer"), this.transform.position, this.transform.rotation) as GameObject;
            audioPlayer[i].transform.SetParent(this.transform);
            audioPlayer[i].SetActive(false);
        }
        for (int i = 0; i < explosion_variations.Length; i++)
        {
            explosion_variations[i] =  (Resources.Load("animations/explosion" + (int)Random.Range(1, 5)) as GameObject).GetComponent<ControllerHolder>().controller;
        }
        for (int i = 0; i < explosions.Length; i++)
        {
            explosions[i] = Instantiate(Resources.Load("Explosion"), this.transform.position, this.transform.rotation) as GameObject;
            explosions[i].transform.SetParent(this.transform);
            explosions[i].SetActive(false);
        }

        for (int i = 0; i < PlasmaRifle.Length; i++)
        {
            PlasmaRifle[i] = Instantiate(Resources.Load("projectiles/PlasmaRifle"), this.transform.position, this.transform.rotation) as GameObject;
            PlasmaRifle[i].transform.SetParent(this.transform);
            PlasmaRifle[i].SetActive(false);
        }
        for (int i = 0; i < PlasmaCannon.Length; i++)
        {
            PlasmaCannon[i] = Instantiate(Resources.Load("projectiles/PlasmaCannon"), this.transform.position, this.transform.rotation) as GameObject;
            PlasmaCannon[i].transform.SetParent(this.transform);
            PlasmaCannon[i].SetActive(false);
        }
        for (int i = 0; i < PlasmaRocket.Length; i++)
        {
            PlasmaRocket[i] = Instantiate(Resources.Load("projectiles/PlasmaRocket"), this.transform.position, this.transform.rotation) as GameObject;
            PlasmaRocket[i].transform.SetParent(this.transform);
            PlasmaRocket[i].SetActive(false);
        }
        for (int i = 0; i < PlasmaMissile.Length; i++)
        {
            PlasmaMissile[i] = Instantiate(Resources.Load("projectiles/PlasmaMissile"), this.transform.position, this.transform.rotation) as GameObject;
            PlasmaMissile[i].transform.SetParent(this.transform);
            PlasmaMissile[i].SetActive(false);
        }
        for (int i = 0; i < PlasmaClusterMissile.Length; i++)
        {
            PlasmaClusterMissile[i] = Instantiate(Resources.Load("projectiles/PlasmaClusterMissile"), this.transform.position, this.transform.rotation) as GameObject;
            PlasmaClusterMissile[i].transform.SetParent(this.transform);
            PlasmaClusterMissile[i].SetActive(false);
        }
        for (int i = 0; i < ConventionalRifle.Length; i++)
        {
            ConventionalRifle[i] = Instantiate(Resources.Load("projectiles/ConventionalRifle"), this.transform.position, this.transform.rotation) as GameObject;
            ConventionalRifle[i].transform.SetParent(this.transform);
            ConventionalRifle[i].SetActive(false);
        }
        for (int i = 0; i < ConventionalCannon.Length; i++)
        {
            ConventionalCannon[i] = Instantiate(Resources.Load("projectiles/ConventionalCannon"), this.transform.position, this.transform.rotation) as GameObject;
            ConventionalCannon[i].transform.SetParent(this.transform);
            ConventionalCannon[i].SetActive(false);
        }
        for (int i = 0; i < ConventionalFlakCannon.Length; i++)
        {
            ConventionalFlakCannon[i] = Instantiate(Resources.Load("projectiles/ConventionalFlakCannon"), this.transform.position, this.transform.rotation) as GameObject;
            ConventionalFlakCannon[i].transform.SetParent(this.transform);
            ConventionalFlakCannon[i].SetActive(false);
        }
        for (int i = 0; i < ConventionalRocket.Length; i++)
        {
            ConventionalRocket[i] = Instantiate(Resources.Load("projectiles/ConventionalRocket"), this.transform.position, this.transform.rotation) as GameObject;
            ConventionalRocket[i].transform.SetParent(this.transform);
            ConventionalRocket[i].SetActive(false);
        }
        for (int i = 0; i < ConventionalClusterMissile.Length; i++)
        {
            ConventionalClusterMissile[i] = Instantiate(Resources.Load("projectiles/ConventionalClusterMissile"), this.transform.position, this.transform.rotation) as GameObject;
            ConventionalClusterMissile[i].transform.SetParent(this.transform);
            ConventionalClusterMissile[i].SetActive(false);
        }
        for (int i = 0; i < ConventionalMissile.Length; i++)
        {
            ConventionalMissile[i] = Instantiate(Resources.Load("projectiles/ConventionalMissile"), this.transform.position, this.transform.rotation) as GameObject;
            ConventionalMissile[i].transform.SetParent(this.transform);
            ConventionalMissile[i].SetActive(false);
        }
        for (int i = 0; i < LaserBeam.Length; i++)
        {
            LaserBeam[i] = Instantiate(Resources.Load("projectiles/LaserBeam"), this.transform.position, this.transform.rotation) as GameObject;
            LaserBeam[i].transform.SetParent(this.transform);
            LaserBeam[i].SetActive(false);
        }
        for (int i = 0; i < LaserCannon.Length; i++)
        {
            LaserCannon[i] = Instantiate(Resources.Load("projectiles/LaserCannon"), this.transform.position, this.transform.rotation) as GameObject;
            LaserCannon[i].transform.SetParent(this.transform);
            LaserCannon[i].SetActive(false);
        }
    }

    public static void getPlayer(string weaponInput, Vector2 position)
    {
        GameObject item = audioPlayer[audioPlayerCount++];
        if (audioPlayerCount == 50)
            audioPlayerCount = 0;
        //Ammo type + weapon type

        //  ProjectileDataWrapper projectileInfo = Values.getProjectileElements(input);
        // EquipmentDataWrapper equipmentInfo = Values.getEquipmentElements(weaponInput);
        string inputInitials = "" ;// = input.Substring(0, 1) + weaponInput.Substring(0, 1);// + input.Substring(2, 1);
        switch (weaponInput)
        {
            case "ConventionalRifle":
                inputInitials = "CR";
                break;

            case "ConventionalCannon":
                inputInitials = "CC";
                break;
            case "PlasmaRifle":
                inputInitials = "PR";
                break;

            case "PlasmaCannon":
                inputInitials = "PC";
                break;
            case "LaserBeam":
                inputInitials = "LR";
                break;

            case "LaserCannon":
                inputInitials = "LC";
                break;
        }//  print(inputInitials + "_" + input);
                                                                                   //print(inputInitials);
        if ((Resources.Load("" + inputInitials) as GameObject) == null)
        {

        }
        else
        {
            item.GetComponent<AudioSource>().clip = (Resources.Load( inputInitials) as GameObject).GetComponent<AudioClipHolder>().GetItem();

            item.GetComponent<AudioSource>().volume = .5f;//(projectileInfo.Projectile_Visuals.Transform_Scale.y * equipmentInfo.Projectile_SizeModifier) / 25f - .125f;  //1.25f + (Random.value/2f);
             //                                                                                                                                                      //if (input.Contains("Cannon")) item.GetComponent<AudioSource> ().volume = Random.value/4 + .25f;
          //  item.GetComponent<AudioSource>().pitch = 10f / (projectileInfo.Projectile_Visuals.Transform_Scale.y * equipmentInfo.Projectile_SizeModifier);  //1.25f + (Random.value/2f);
                                                                                                                                                           //if (input != "LaserCannon" && input.Contains ("Cannon") || input.Contains("Rocket"))
                                                                                                                                                           //	item.GetComponent<AudioSource> ().pitch = 1f;
                                                                                                                                                           //if (input.Contains("Cannon")) item.GetComponent<AudioSource>().volume = .10f;

            item.SetActive(true);

            item.GetComponent<AudioSource>().Play();
        }
    }
    public static void getExplosion(Vector2 position, Vector3 shipSize, float damage)
    {
        GameObject item = explosions[explosionsCount++];
        if (explosionsCount == 50)
            explosionsCount = 0;
        float x = (Random.value > .5f) ? (shipSize.x) * Random.value : (shipSize.x) * -Random.value;
        float y = (Random.value > .5f) ? (shipSize.y) * Random.value : (shipSize.y) * -Random.value;
        item.transform.position = new Vector2(position.x + x / 4, position.y + y / 4);
        item.GetComponent<Animator>().runtimeAnimatorController = explosion_variations[(int)Random.Range(0, 5)]; // (Resources.Load("animations/explosion" + (int)Random.Range(1, 5)) as GameObject).GetComponent<ControllerHolder>().controller;

        item.transform.localScale = new Vector2(damage * shipSize.x * 1f, damage * shipSize.x * 1f);

        item.SetActive(true);
    }

    public static void getProjectile(string firingType, Vector3 position, Quaternion rotation, float damage_multiplier)
    {
        //if (firingType == "PlasmaRifle")
        //	firingType = "laser";
        GameObject projectile = PlasmaRifle[0];
        if (firingType == "PlasmaRifle")
        {
            projectile = PlasmaRifle[PlasmaRifleCount++];
            if (PlasmaRifleCount == PlasmaRifleAmount) PlasmaRifleCount = 0;
        }
        if (firingType == "PlasmaCannon")
        {
            projectile = PlasmaCannon[PlasmaCannonCount++];
            if (PlasmaCannonCount == PlasmaCannonAmount) PlasmaCannonCount = 0;
        }
        if (firingType == "PlasmaRocket")
        {
            projectile = PlasmaRocket[PlasmaRocketCount++];
            if (PlasmaRocketCount == PlasmaRocketAmount) PlasmaRocketCount = 0;
        }
        if (firingType == "PlasmaMissile")
        {
            projectile = PlasmaMissile[PlasmaMissileCount++];
            if (PlasmaMissileCount == PlasmaMissileAmount) PlasmaMissileCount = 0;
        }
        if (firingType == "PlasmaClusterMissile")
        {
            projectile = PlasmaClusterMissile[PlasmaClusterMissileCount++];
            if (PlasmaClusterMissileCount == PlasmaClusterMissileAmount) PlasmaClusterMissileCount = 0;
        }
        if (firingType == "ConventionalRifle")
        {
            projectile = ConventionalRifle[ConventionalRifleCount++];
            if (ConventionalRifleCount == ConventionalRifleAmount) ConventionalRifleCount = 0;
        }
        if (firingType == "ConventionalCannon")
        {
            projectile = ConventionalCannon[ConventionalCannonCount++];
            if (ConventionalCannonCount == ConventionalCannonAmount) ConventionalCannonCount = 0;
        }
        if (firingType == "ConventionalFlakCannon")
        {
            projectile = ConventionalFlakCannon[ConventionalFlakCannonCount++];
            if (ConventionalFlakCannonCount == ConventionalFlakCannonAmount) ConventionalFlakCannonCount = 0;
        }
        if (firingType == "ConventionalRocket")
        {
            projectile = ConventionalRocket[ConventionalRocketCount++];
            if (ConventionalRocketCount == ConventionalRocketAmount) ConventionalRocketCount = 0;
        }
        if (firingType == "ConventionalClusterMissile")
        {
            projectile = ConventionalClusterMissile[ConventionalClusterMissileCount++];
            if (ConventionalClusterMissileCount == ConventionalClusterMissileAmount) ConventionalClusterMissileCount = 0;
        }
        if (firingType == "ConventionalMissile")
        {
            projectile = ConventionalMissile[ConventionalMissileCount++];
            if (ConventionalMissileCount == ConventionalMissileAmount) ConventionalMissileCount = 0;
        }

        if (firingType == "LaserBeam")
        {
            projectile = LaserBeam[LaserBeamCount++];
            if (LaserBeamCount == LaserBeamAmount) LaserBeamCount = 0;
        }
        if (firingType == "LaserCannon")
        {
            projectile = LaserCannon[LaserCannonCount++];
            if (LaserCannonCount == LaserCannonAmount) LaserCannonCount = 0;
        }
        if (firingType == "missile")
        {
            projectile = missile[missileCount++];
            if (missileCount == 100) missileCount = 0;
        }
        if (firingType == "missileLarge")
        {
            projectile = missileLarge[missileLargeCount++];
            if (missileLargeCount == 50) missileLargeCount = 0;
        }
        if (firingType == "missileCluster")
        {
            projectile = missileCluster[missileClusterCount++];
            if (missileClusterCount == 20) missileClusterCount = 0;
        }
        projectile.transform.position = position;
        projectile.transform.rotation = rotation;
        projectile.GetComponent<projectileFunction>().life = 0f;
        projectile.GetComponent<projectileFunction>().damage *= damage_multiplier;
        projectile.SetActive(true);
    }

}

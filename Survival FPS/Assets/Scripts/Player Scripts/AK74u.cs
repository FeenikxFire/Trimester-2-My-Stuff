using UnityEngine;
using System.Collections;

public class AK74u : MonoBehaviour
{

    private Transform myTransform;

    //Player reference
    Player player;

    //Weapon Variables
    private bool fireReady = true;
    private float fireTime;
    private float fireRate = 0.1f;      //This needs to match the firing animation length
    public int clipSize = 30;
    private float dmg = 5;
    public int shotsFired;

    //Muzzle + Raycast
    public GameObject muzzle;
    public GameObject rayCastStart;

    //Sound Clips
    public GameObject fireSound;
    public GameObject reloadSound;
    public GameObject hitSound;

    //Visual Effects
    public GameObject muzzleBlast;
    public GameObject bulletHit;

    // Use this for initialization
    void Start()
    {

        myTransform = this.transform;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        rotateMuzzle();

        firingCheck();
    }


    //rotate muzzle to face focusPoint
    void rotateMuzzle()
    {
        muzzle.transform.LookAt(player.focusPoint.transform.position);
        rayCastStart.transform.LookAt(player.focusPoint.transform.position);
    }


    //Play Weapon Fire Controls - Animation, Audio and Projectiles
    void firingCheck()
    {

        if (Input.GetMouseButton(0) && player.currentAmmo > 0 &&
            GetComponent<Animation>().IsPlaying("AK74u reload") == false)
        {

            //Play Animation
            GetComponent<Animation>().Play("AK74u fire");

            //Fire weapon
            if (Time.time > fireTime)
            {
                //Reduce current player ammo
                player.currentAmmo--;

                //Add to shots fired
                shotsFired++;

                //Spawn effects
                GameObject thisMuzzleFlare = Instantiate(muzzleBlast, muzzle.transform.position, muzzle.transform.rotation) as GameObject;
                thisMuzzleFlare.transform.parent = transform;
                GameObject thisSound = Instantiate(fireSound, muzzle.transform.position, muzzle.transform.rotation) as GameObject;
                thisSound.transform.parent = transform;

                //Raycast bullet at player focuspoint from raycast start
                RaycastHit hit;
                if (Physics.Raycast(rayCastStart.transform.position, rayCastStart.transform.forward, out hit, 2500.0f))
                {
                    Debug.DrawLine(rayCastStart.transform.position, hit.point, Color.red);

                    hit.transform.SendMessage("takeDamage", dmg, SendMessageOptions.DontRequireReceiver);

                    //Spawn visual/audio hit effects
                    Instantiate(bulletHit, hit.point, hit.transform.rotation);
                    Instantiate(hitSound, hit.point, hit.transform.rotation);
                }
                fireTime = Time.time + fireRate;
            }
        }
        else if (Input.GetKeyDown("r") || player.currentAmmo <= 0)
        {
            if (player.currentAmmo == 30)
            {
                return;
            }
            if (player.reserveAmmo > 0)
            {
                GetComponent<Animation>().Play("AK74u reload");

                //Play reload sound
                GameObject thisSound = Instantiate(reloadSound, transform.position, transform.rotation) as GameObject;
                thisSound.transform.parent = transform;

                //Refill current ammo with maxclipsize
                player.reserveAmmo -= shotsFired;
                player.currentAmmo += shotsFired;
                shotsFired = 0;
                if (shotsFired >= player.reserveAmmo)
                {
                    shotsFired = player.reserveAmmo;
                    player.currentAmmo += player.reserveAmmo;
                    player.reserveAmmo -= shotsFired;
                    shotsFired = 0;
                }
            }
        }
    }
}
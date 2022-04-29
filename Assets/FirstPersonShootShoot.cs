using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor.UI;

public class FirstPersonShootShoot : MonoBehaviour
{
    // bullet (in my case it'll be the word woof)
    public GameObject bullet;

    // bullet force
    public float shootForce, upwardForce;

    // Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    // bools
    bool shooting, readyToShoot, reloading;

    // reference
    public Camera fpsCam;
    public GameObject player;
    public Transform attackPoint;

    public bool allowInvoke = true;

    private void Awake()
    {
        // filling mag
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        //Checks if player can hold down the button to shoot
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        // Reload automatically when trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        // Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            // Set bullets shot to 0
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        // Find the hit position using raycasting
        Ray ray = fpsCam.ViewportPointToRay( new Vector3(0.5f, 0.5f, 0)); //This is the ray that goes through the middle of the screen
        RaycastHit hit;

        // check if rays hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Meant to be a point away from the player
        
        // Calculate direction from attackPoint to targetPoint
        // Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
        Vector3 directionWithoutSpread = targetPoint - player.transform.position;
        Debug.Log(targetPoint);

        // Calculate Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to the one without spread

        // Instantiate the bullet/projectile
        GameObject currentBullet = Instantiate(bullet, player.transform.position, Quaternion.identity); //Quaternion.identity is meant to remove rotation?
        // Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        // Adds forces to bullet
        // currentBullet.GetComponent<Rigidbody>().AddForce(player.transform.position.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        // currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse); //Simlulates the arc on granades


        bulletsLeft --;
        bulletsShot ++;

        // Invoke the reset function 
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        // if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        // Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading =true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    // Custom Editor
    // #if UNITY_EDITOR
    // [CustomEditor(typeof(FirstPersonShootShoot)), InitializeOnLoadAttribute]
    // public class FirstPersonShootShootEditor : Editor
    // {
        
    // }
    // #endif
}

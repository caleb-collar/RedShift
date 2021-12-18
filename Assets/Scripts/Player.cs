using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Player | Caleb A. Collar | 10.7.21
public class Player : MonoBehaviour

{
    [SerializeField] private float moveSpeed = 10f,
        padding = 0.3f,
        shotDelay = 2f,
        laserSpd = 10f,
        health = 500;
    [SerializeField] private GameObject projectileChoice;
    [SerializeField] private Slider healthBar, shieldBar;
    private float xMin, xMax, yMin, yMax, sinceLastShot, maxHealth = 0f;
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private AudioClip shootSound;
    private Level thisLevel;

    void Start()
    {
        thisLevel = GameObject.Find("Level").GetComponent<Level>();
        maxHealth = health;
        sinceLastShot = shotDelay + 1f;
        SetupWorldBoundaries();
    }
    
    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var xPos = transform.position.x + deltaX;
        var yPos = transform.position.y + deltaY;
        xPos = Mathf.Clamp(xPos, xMin, xMax);
        yPos = Mathf.Clamp(yPos, yMin, yMax);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }

    void Shoot()
    {
        sinceLastShot += Time.deltaTime;
        if (Input.GetAxis("Fire1") > 0)
        {
            if (sinceLastShot > shotDelay)
            {
                GameObject laser = (GameObject)Instantiate(projectileChoice,transform.position,Quaternion.identity);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, laserSpd);
                sinceLastShot = 0f;
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
            }
        }
    }

    /*
    IEnumerator FireContinuously()
    {
        GameObject laser = (GameObject)Instantiate(projectileChoice,transform.position,Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, laserSpd);
        yield return new WaitForSeconds(shotDelay);
    }
    */

    void SetupWorldBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x +padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x -padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y +padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y -padding -1;

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null && other.gameObject.CompareTag("EnemyP"))
        {
            ProcessHit(damageDealer);
        }
    }
    
    private void ProcessHit(DamageDealer damageDealer)
    {
        if (shieldBar.value <= 0)
            health -= damageDealer.GetDamage();
            healthBar.value = health/maxHealth;
            damageDealer.Hit();
            if (health <= 0)
            {
                AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);
                TriggerDestroyVFX();
                Destroy(gameObject);
            }
            else
            {
                AudioSource.PlayClipAtPoint(hitSounds[UnityEngine.Random.Range(0, hitSounds.Length)],Camera.main.transform.position);
            }
    }

    private void TriggerDestroyVFX()
    {
        GameObject breakVFXObj = Instantiate(destroyVFX, transform.position, transform.rotation);
        Destroy(breakVFXObj, 3f);
    }
}

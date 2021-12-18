using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Shields : MonoBehaviour
{
    [SerializeField] private int health = 1000;
    [SerializeField] private GameObject shieldFXObj;
    [SerializeField] private Slider shieldBar;
    private float fade = 0.15f;
    private float maxHealth = 0;
    [SerializeField] private AudioClip shieldDownSound;
    [SerializeField] private AudioClip shieldUpSound;
    [SerializeField] private AudioClip[] hitSounds;

    private void Start()
    {
        maxHealth = health;
        AudioSource.PlayClipAtPoint(shieldUpSound, Camera.main.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null && other.gameObject.CompareTag("EnemyP"))
        {
            ProcessHit(damageDealer);
            //Debug.Log("Player hit");
        }
    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        GameObject shieldHitFX = (GameObject) Instantiate(shieldFXObj, transform.position, Quaternion.identity);
        shieldHitFX.transform.parent = gameObject.transform;
        health -= damageDealer.GetDamage();
        shieldBar.value = health/maxHealth;
        damageDealer.Hit();
        Destroy(shieldHitFX, fade);
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(shieldDownSound, Camera.main.transform.position);
            Destroy(gameObject);
        }
        else
        {
            AudioSource.PlayClipAtPoint(hitSounds[UnityEngine.Random.Range(0, hitSounds.Length)],Camera.main.transform.position);
        }
    }
}

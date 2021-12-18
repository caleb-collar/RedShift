using System;
using UnityEngine;
using Random = UnityEngine.Random;

//Enemy | Caleb A. Collar | 10.7.21
public class Enemy : MonoBehaviour
{
   [SerializeField] private int health = 100;
   [SerializeField] private float minTimeBetweenShots = 0.2f, maxTimeBetweenShots = 2f, laserSpd = -10f;
   [SerializeField] private GameObject projectileChoice;
   [SerializeField] private int pointsForEnemy = 10;
   [SerializeField] private AudioClip destroySound;
   [SerializeField] private AudioClip[] hitSounds;
   [SerializeField] private AudioClip shootSound;
   [SerializeField] private GameObject destroyVFX;
   private float sinceLastShot = 0f;
   private Level thisLevel;

   private void Start()
   {
      thisLevel = GameObject.Find("Level").GetComponent<Level>();
      sinceLastShot = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
   }

   private void Update()
   {
      CountDownFire();
   }

   private void CountDownFire()
   {
      sinceLastShot -= Time.deltaTime;
      if (sinceLastShot <= 0)
      {
         Fire();
         sinceLastShot = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
      }
   }

   private void Fire()
   {
      GameObject laser = (GameObject)Instantiate(projectileChoice,transform.position,Quaternion.identity);
      laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, laserSpd);
      sinceLastShot = 0f;
      AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
      if (damageDealer != null && other.gameObject.CompareTag("PlayerP"))
      {
         ProcessHit(damageDealer);
      }
   }

   private void ProcessHit(DamageDealer damageDealer)
   {
      health -= damageDealer.GetDamage();
      damageDealer.Hit();
      if (health <= 0)
      {
         Destroy(gameObject);
         AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);
         TriggerDestroyVFX();
         thisLevel.ScoreUpdate(pointsForEnemy);
      }
      else
      {
         //AudioSource.PlayClipAtPoint(hitSounds[UnityEngine.Random.Range(0, hitSounds.Length)],Camera.main.transform.position);
      }
   }
   
   private void TriggerDestroyVFX()
   {
      GameObject breakVFXObj = Instantiate(destroyVFX, transform.position, transform.rotation);
      Destroy(breakVFXObj, 3f);
   }
}

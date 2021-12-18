using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Damage Dealer | Caleb A. Collar | 10.7.21
public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private AudioClip optionalSFX;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
        if (optionalSFX != null)
        {
            AudioSource.PlayClipAtPoint(optionalSFX, Camera.main.transform.position);
        }
    }
}

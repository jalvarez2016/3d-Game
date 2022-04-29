using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public int EnemyHealth;
    public int DamagePerHit;
    public GameObject Bullet;

   private void OnCollisionEnter(Collision other) {
    // check if bullet is the same as other (don't want squirrel taking damage on terrain)
       Debug.Log("Enemy Hit!");
       Debug.Log(EnemyHealth);
       EnemyHealth -= DamagePerHit;
       Debug.Log(EnemyHealth);
    }

    private void DefeatEnemy() {

    }
}

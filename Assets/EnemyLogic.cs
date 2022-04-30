using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyLogic : MonoBehaviour
{
    public int EnemyHealth;
    public int DamagePerHit;
    public GameObject EnemyObject;
    public NavMeshAgent Enemy;
    public Transform Player;
    public Animator EnemyAnimator; //for changing the animation

    private Vector3 lastSeenPlayerPosition;
   
   void Update() {
       if (EnemyHealth <= 0) {
           DefeatEnemy();
       }
   }

   private void OnTriggerStay(Collider other) {
        Enemy.SetDestination(Player.position);
        EnemyAnimator.SetBool("Walking", true);
        lastSeenPlayerPosition = Player.position;
   }

   private void OnTriggerExit(Collider other) {
        Invoke("StopWalking", 9);
   }

   private void StopWalking() {
        EnemyAnimator.SetBool("Walking", false);
   }

   private void OnCollisionEnter(Collision other) {   
        if(other.gameObject.tag == "Projectile"){
           EnemyHealth -= DamagePerHit;
        }
    }

    private void DefeatEnemy() {
        SceneManager.LoadScene("Win", LoadSceneMode.Single);
    }
}

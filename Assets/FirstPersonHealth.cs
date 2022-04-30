using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonHealth : MonoBehaviour
{
    public int PlayerHealth;
    public Rigidbody PlayerObject;
    public float thrust;

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth <= 0) {
            Debug.Log("Player Lose");
            SceneManager.LoadScene("Lose", LoadSceneMode.Single);
        }
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Enemy"){
            PlayerObject.AddForce(other.relativeVelocity.x + 10 * thrust, other.relativeVelocity.y + 30 * thrust, other.relativeVelocity.z + 10 * thrust);
            PlayerHealth -= 10;
        }
    }
}

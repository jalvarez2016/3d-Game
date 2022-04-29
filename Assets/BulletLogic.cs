using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public GameObject bullet;

    private void OnCollisionEnter(Collision other) {
        Destroy(bullet);
        // instantiate a burst effect here if there's enough time
    }
}

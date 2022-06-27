using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType
{
    Circle,
    Square,
    Triangle
}
public class BulletController : MonoBehaviour {

    [SerializeField] private float speed = 30;

    private Rigidbody rig;

    private void Awake() {
        rig = GetComponent<Rigidbody>();
        Destroy(gameObject, 5);
    }

    private void Update() {
        // Timer to destroy the bullet if it doesn't hit anything
        //age -= Time.deltaTime;
        //if (age <= 0) Destroy(gameObject);

        rig.velocity = transform.forward * speed;

    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy the enemy if it matches the bullet tag
        if (other.CompareTag(tag))        
            other.GetComponent<Enemy>().Death();
        else        
            Destroy(gameObject);
    }
}

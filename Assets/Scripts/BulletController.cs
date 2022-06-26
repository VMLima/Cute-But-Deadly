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
        Debug.Log($"Hit tag: {other.tag}, name: {other.name}");
        // Destroy the enemy if it matches the bullet tag
        if (other.CompareTag(tag))        
            Destroy(other.gameObject);
        else        
            Destroy(gameObject);
    }
}

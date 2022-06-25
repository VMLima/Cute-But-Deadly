using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    private float age;
    private float speed;

    private Rigidbody rig;

    private void Awake() {
        rig = GetComponent<Rigidbody>();
    }

    private void Update() {
        // Timer to destroy the bullet if it doesn't hit anything
        age -= Time.deltaTime;
        if (age <= 0) Destroy(gameObject);

        rig.velocity = transform.forward * speed;

    }

    private void OnTriggerEnter(Collider other) {
        // Destroy the enemy if it matches the bullet tag
        if (other.CompareTag(tag)) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void SetStats(float _age, float _speed) {
        age = _age;
        speed = _speed;
    }

}

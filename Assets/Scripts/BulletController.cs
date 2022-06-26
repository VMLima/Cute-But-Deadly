using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    private float age;
    [SerializeField] private float speed = 10;

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

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Hit" + other.tag);
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

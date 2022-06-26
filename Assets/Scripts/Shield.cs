using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [SerializeField] private LayerMask enemyLayer;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        int isEnemy = (int)Mathf.Pow(2, other.gameObject.layer) & enemyLayer;
        if (isEnemy != 0)
        {
            gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }

}

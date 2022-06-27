using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    public void OnDeath()
    {
        Destroy(GetComponentInParent<Enemy>().gameObject);
    }
}

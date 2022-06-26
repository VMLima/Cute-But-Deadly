using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum PowerUpType
{
    Health,
    RapidFire,
    Shield
}
public class PowerUp : MonoBehaviour
{
    private PowerUpSpawner _powerUpSpawner;
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float powerUpDuration;
    [SerializeField] private LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        int isPlayer = (int)Mathf.Pow(2, other.gameObject.layer) & playerLayer;
        if (isPlayer != 0)
        {
            Player player = other.GetComponentInParent<Player>();
            player.UsePowerUp(powerUpType, powerUpDuration);
            _powerUpSpawner.HidePowerups();
        }
    }

    public void SetSpawner(PowerUpSpawner powerUpSpawner)
    {
        this._powerUpSpawner = powerUpSpawner;
    }

}


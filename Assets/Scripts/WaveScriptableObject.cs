﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave")]
public class WaveScriptableObject : ScriptableObject
{
    public int EnemiesInWave;
    public float SpawnInterval;
}


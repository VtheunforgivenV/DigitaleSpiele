using UnityEngine;
using System.Collections;

public class Enemy_Elite: EnemyDiffcult {

    void Awake() {
        type = DiffcultType.ELITE;
        healthMultiplier = 1.4;
        damageMultiplier = 1.2;
        expMultiplier = 2.0;
    }
    
}


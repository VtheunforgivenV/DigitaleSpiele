using UnityEngine;
using System.Collections;

public class Enemy_Normal: EnemyDiffcult {

    void Awake() {
        type = DiffcultType.NORMAL;
        healthMultiplier = 1.0;
        damageMultiplier = 1.0;
        expMultiplier = 1.0;
    }
    
}


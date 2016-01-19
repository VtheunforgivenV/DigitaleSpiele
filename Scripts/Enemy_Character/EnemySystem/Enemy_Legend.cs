using UnityEngine;
using System.Collections;

public class Enemy_Legend: EnemyDiffcult {

    void Awake() {
        type = DiffcultType.LEGEND;
        healthMultiplier = 2.0;
        damageMultiplier = 1.5;
        expMultiplier = 4.0;
    }
    
}


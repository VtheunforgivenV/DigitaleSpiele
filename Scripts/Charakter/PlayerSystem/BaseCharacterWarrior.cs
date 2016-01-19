using UnityEngine;
using System.Collections;

public class BaseCharacterWarrior : BaseCharacter {

	public BaseCharacterWarrior() {
		this.playerType = PlayerType.MELEE;
		this.characterClassName = "Krieger";
		this.characterDescription = "Der Kriefer ist stark im Nahkampf, dafür aber schwach in Fernangriffen.";
		this.maxHealth = 110.0;
		this.strength = 10.0;
        this.damageReduction = 0.5;
	}
}

using UnityEngine;
using System.Collections;

public class BaseCharacterMage : BaseCharacter {
	
	public BaseCharacterMage() {
		this.playerType = PlayerType.MAGIC;
		this.characterClassName = "Magier";
		this.characterDescription = "Der Magier kann sowohl im Nahkampf als auch im Fernkampf stark sein, je nach training seiner Fertigkeiten.";
		this.maxHealth = 75.0;
		this.strength = 20.0;
        this.damageReduction = 1;
	}
}

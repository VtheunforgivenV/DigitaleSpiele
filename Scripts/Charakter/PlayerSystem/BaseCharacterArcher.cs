using UnityEngine;
using System.Collections;

public class BaseCharacterArcher : BaseCharacter {

	public BaseCharacterArcher(){
		this.playerType = PlayerType.DISTANCE;
		this.characterClassName = "Schütze";
		this.characterDescription = "Der Bogenschütze ist schwach im Nahkampf, dafür aber stark in Fernangriffen.";
		this.maxHealth = 90.0;
		this.strength = 15.0;
        this.damageReduction = 0.75;
	}


}

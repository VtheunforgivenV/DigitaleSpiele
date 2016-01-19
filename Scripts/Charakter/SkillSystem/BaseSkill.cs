using UnityEngine;
using System.Collections;

public class BaseSkill {
	public Skill.type type;
	public string name;
	public string identifier;
	public string description;
	public string imageName;

	public int level;
	public int maxLevel;

    public Skill.passiveSkillType passiveSkillType;
	public double healthMultiplier;
	public double damageMultiplier; 
	public double damageReductionMultiplier;

	public int weaponLevel;

	public int staticHealth;
	public int staticDamage;

	public double aktiveDamageMultiplier;
}

/* Skill-List

5*0.2 Increase Health
5*0.2 Increase Damage
5*0.2 Increase Reduction

2 * Weapon Level

x * static Health
x * Static Damage

1 * Aktive Skill
*/
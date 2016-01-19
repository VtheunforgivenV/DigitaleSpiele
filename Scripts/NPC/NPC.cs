using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    public Quest quest;

	void Start () {
        int killCount = 3;
        quest = new KillQuest(KillType.Skeleton, 
                                killCount, 
                                "Hallo, ein paar Skelette haben mich vermöbelt und ich brauche dringend eure Hilfe um mich an Ihnen zu rächen. Bitte Töte ein paar Skelette für mich \n\n Töte " + killCount + " Skelette", 
                                1000, 
                                1000, 
                                null);
	}
	
}

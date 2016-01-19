using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillToolTip : MonoBehaviour {

	private GameObject info;
	private Text skillTitle;
	private Text skillDescription;

	void Start () {
		
		info = GameObject.Find("Info_Panel");

		Transform t;

		for (int i = 0; i < info.transform.childCount; i++){
			t = info.transform.GetChild(i);
			if (t.name.Equals("Title")){
				skillTitle = t.gameObject.GetComponent<Text> ();
			}else if (t.name.Equals("Description")){
				skillDescription = t.gameObject.GetComponent<Text> ();
			}
		}

		info.SetActive(false);
	}
	
	public void Activate(BaseSkill skill){
		
		info.SetActive(true);
		info.transform.position = new Vector2((Input.mousePosition.x + 200), (Input.mousePosition.y - 75));

		if(skillTitle != null){
			skillTitle.enabled = true;
			skillTitle.text = skill.name;
		}else {
			skillTitle.enabled = false;
		}

		if (skillDescription != null){
			skillDescription.enabled = true;
			skillDescription.text = skill.description;
		}else{
			skillDescription.enabled = false;
		}
	}

	public void Deactivate(){
		info.SetActive(false);
	}
}

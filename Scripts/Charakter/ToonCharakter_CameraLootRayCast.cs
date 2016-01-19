using UnityEngine;
using UnityEngine.UI;

public class ToonCharakter_CameraLootRayCast : MonoBehaviour {

    private Text openText;
    private LootInfo info;
    private ILootableObject loot;

    void Start(){
        info = GameObject.Find("LootingUI").GetComponent<LootInfo>();
        openText = GameObject.Find("LootText").GetComponent<Text>();
        openText.transform.gameObject.SetActive(false);
    }

    void Update(){
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3)){
			if (hit.transform.tag.Equals(GameConstants.chestTag)) {
                loot = hit.transform.gameObject.GetComponent<Chest>();
			} else if(hit.transform.tag.Equals(GameConstants.enemyTag)){
                if (hit.transform.gameObject.GetComponent<Enemy_DetectPlayer>().isDead) {
                    Debug.Log("Enemy is Dead - you can loot");
                    loot = hit.transform.gameObject.GetComponent<Enemy_Loot>();
                } else {
                    Debug.Log("Enemy is not Dead - you can not loot");
                    loot = null;
                }
            } else {
                loot = null;
            }

            if (loot != null) { 
                //chest = hit.transform.gameObject.GetComponent<Chest>();
                openText.transform.gameObject.SetActive(true);
                if (!loot.isLooted()){
                    if (loot.isOpened()){
                        if (Input.GetKeyDown(KeyCode.E)) {
                            //info.lootChest();
                            info.lootObject();
                        }else if (Input.GetKeyDown(KeyCode.Q)){
                            DisableUIElements(true);
                        }
                    }else{
                        if (Input.GetKeyDown(KeyCode.E)){
                            loot.setOpened(true);
                            openText.text = "Looten(E) \nNicht Looten(Q) oder weggehen";
                            info.Activate(loot);
                        }
                    }
                }else{
                    if (loot is Chest) {
                        setLootText("Kiste ist leer");
                    } else if (loot is Enemy_Loot) {
                        setLootText("Gegner hat keinen Loot mehr");
                    }
                }
            }
            else{
                DisableUIElements(false);
            }
        }
        else{
            DisableUIElements(false);
        }
    }

    void DisableUIElements(bool setActive){
        if(loot != null) {
            loot.setOpened(false);
        }
        openText.text = "Öffnen(E)";
        openText.transform.gameObject.SetActive(setActive);
        info.Deactivate();
    }

    void setLootText(string text){
        openText.text = text;
    }

    public class RayCastTarget<T> {
        T rayCastTarget;
    }
}

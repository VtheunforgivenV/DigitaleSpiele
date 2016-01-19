using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEngine.UI;

public class UIManager_BaseGame : MonoBehaviour {

	public string panel_name;

	protected GameObject info;

	// Use this for initialization
	protected void Start () {
		this.info = GameObject.Find (panel_name);

		this.info.GetComponentInParent<Canvas> ().enabled = false;

		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1.0f;
	}

	public void closeMenu() {
		this.activateMenu (false);
	}

	public void activateMenu(bool active) {

		if (active) {
			closeAllCanvas ();
		}

		this.info.GetComponentInParent<Canvas> ().enabled = active;
		Cursor.visible = active;

		if (!active) {
			Time.timeScale = 1.0f;
			Cursor.lockState = CursorLockMode.Locked;
		} else {
			Time.timeScale = 0.0f;
			Cursor.lockState = CursorLockMode.None;
		}
	}

	protected void closeAllCanvas() {

		FieldInfo[] propInfo = typeof(GameConstants.GameUIs).GetFields ();
		foreach (FieldInfo ui in propInfo) {
			closeUI(ui.GetRawConstantValue() as string);
		}
			
		Canvas inventoryUI = GameObject.Find (GameConstants.inventoryUI).gameObject.GetComponent<Canvas> ();
		if (inventoryUI.isActiveAndEnabled) {
			GameConstants.findPlayerInProject ().GetComponent<ToonCharacter_Inventory> ().activateMenu (false);
		}

		//TODO deactivate NPC UI / Loot UI

	}

	private void closeUI(string name) {
		
		Canvas ui = GameObject.Find (name).gameObject.GetComponent<Canvas> ();

		if (ui.isActiveAndEnabled) {
			ui.GetComponentInChildren<UIManager_BaseGame> ().activateMenu (false);
		}
	}

	protected void respawnPlayer() {

		GameObject playerObj = GameConstants.findPlayerInProject ();

		LoadPlayer parent = playerObj.GetComponentInParent<LoadPlayer> ();

		Destroy (playerObj);

		parent.loadPlayer ();

	}

	public void showMessageBox(string message, GameObject panel) {

		GameObject box = Instantiate (Resources.Load<GameObject> ("Prefabs/MessageBox"));
		box.transform.SetParent (panel.transform);
		box.GetComponent<UIManager_MessageBox> ().setMessage (message);
	}

	public void showMessageBox(string message) {

		GameObject box = Instantiate (Resources.Load<GameObject> ("Prefabs/MessageBox"));
		box.transform.SetParent (this.transform);
		box.GetComponent<UIManager_MessageBox> ().setMessage (message);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager_MessageBox : MonoBehaviour {

	void Start() {
		RectTransform rectTransform = GetComponent<RectTransform> ();
		rectTransform.anchoredPosition = new Vector2 (.5f, .5f);
	}

	public void setMessage(string message) {
		this.transform.FindChild ("Text").GetComponent<Text> ().text = message;
	}

	public void closeMessageBox () {
		Destroy (this.gameObject);
	}
}

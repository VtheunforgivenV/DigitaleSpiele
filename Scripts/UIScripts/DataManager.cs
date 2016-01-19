using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;

public class DataManager {

	private string baseFilePath;

	public DataManager() {
		this.baseFilePath = Application.dataPath + "/StreamingAssets";
	}

	public bool fileExists(string filename) {
		string filePath = this.baseFilePath + "/" + filename;
		return File.Exists (filePath);
	}

	public void saveToFile<T>(T obj, string filename ) {
		string filePath = this.baseFilePath + "/" + filename;
		string json = JsonMapper.ToJson (obj);
		File.WriteAllText (filePath, json);
	}

	public T readFile<T>(string filename) {
		string filePath = this.baseFilePath + "/" + filename;
		string json = File.ReadAllText (filePath);
		T obj = JsonMapper.ToObject<T> (json);
		return obj;
	}
}

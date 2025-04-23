using Newtonsoft.Json.Linq;
using System.IO;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(TankSO))]
public class TankSOEditor : Editor
{
    private TankSO _target;

    private void OnEnable()
    {
        _target = (TankSO) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(_target != null)
        {
            EditorGUILayout.BeginHorizontal();

            if(GUILayout.Button("Load") == true)
            {
                Load(_target.textAsset.text);
            }
            if (GUILayout.Button("Save") == true)
            {
                Save();
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    private void Load(string data)
    {
        if (string.IsNullOrEmpty(data))
            return;

        _target.tankModels.Clear();

        JArray jarray = JArray.Parse(data);

        foreach(var json in jarray)
        {
            TankModel tankModel = json.ToObject<TankModel>();
            _target.tankModels.Add(tankModel);
        }
    }

    private void Save()
    {
        if(_target.tankModels == null || _target.tankModels.Count == 0)
            return;

        string path = AssetDatabase.GetAssetPath(_target);

        JArray jArray = new JArray();

        foreach(var data in _target.tankModels)
        {
            JObject jObject = JObject.FromObject(data);
            jArray.Add(jObject);
        }

        string directory = Path.GetDirectoryName(path);
        string assetName = Path.GetFileNameWithoutExtension(path);
        string fileName = Path.Combine(directory, assetName + ".json");

        if(Directory.Exists(directory) == false)
            Directory.CreateDirectory(directory);

        File.WriteAllText(fileName, jArray.ToString());
        AssetDatabase.Refresh();

        Debug.Log(fileName + " saved.");
    }
}

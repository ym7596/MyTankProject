using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TankSO", menuName = "SO/TankSO", order = 1)]
public class TankSO : ScriptableObject
{
    public TextAsset textAsset;
    public List<TankModel> tankModels;
}

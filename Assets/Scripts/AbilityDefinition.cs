using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AbilityDefinition", menuName = "Scriptable Objects/AbilityDefinition")]
public class AbilityDefinition : ScriptableObject
{
    [SerializeField] List<AbilityProcessor> processors = new List<AbilityProcessor>();
}

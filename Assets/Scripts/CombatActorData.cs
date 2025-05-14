using UnityEngine;

public enum CombatZone { Vanguard, Midline, Artillery }

[CreateAssetMenu(fileName = "Combat Actor Data", menuName = "Scriptable Objects/Combat Actor Data")]
public class CombatActorData : ScriptableObject
{
    [SerializeField] public CombatZone Zone = CombatZone.Midline;

    [Header("Actor Core Statistics")]
    [SerializeField] public int Strength = 20;
    [SerializeField] public int Vitality = 20;
    [SerializeField] public int Speed = 20;
    [SerializeField] public int Aim = 20;
    [SerializeField] public int Intelligence = 20;
    [SerializeField] public int Charisma = 20;
    [SerializeField] public int Magic = 20;
}

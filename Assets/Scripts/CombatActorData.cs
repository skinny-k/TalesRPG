using UnityEngine;

public enum TeamAffiliation { None = 0,
                              Ally = 1 << 0,
                              Enemy = 1 << 1,
                              Unaffiliated = 1 << 2,
                              Any = ~0 };
public enum CombatZone { None = 0,
                         Vanguard = 1 << 0,
                         Midline = 1 << 1,
                         Artillery =1 << 2,
                         All = ~0 };

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

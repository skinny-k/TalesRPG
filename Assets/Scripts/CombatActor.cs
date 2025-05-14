using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ActorHealth))]
public class CombatActor : MonoBehaviour
{
    [SerializeField] public CombatActorData _baseActorData;
    
    public CombatZone DefaultZone { get; private set; } 
    public CombatZone CurrentZone { get; private set; }
    public ActorHealth Health { get; private set; }

    public int Strength { get; private set; }
    public int Vitality { get; private set; }
    public int Speed { get; private set; }
    public int Aim { get; private set; }
    public int Intelligence { get; private set; }
    public int Charisma { get; private set; }
    public int Magic { get; private set; }

    private bool initialized = false;

    void Awake()
    {
        Health = GetComponent<ActorHealth>();
        Health.SetActor(this);
    }

    public void InitializeStatisticsFromData()
    {
        if (initialized)
            return;
        
        Strength     = _baseActorData.Strength;
        Vitality     = _baseActorData.Vitality;
        Speed        = _baseActorData.Speed;
        Aim          = _baseActorData.Aim;
        Intelligence = _baseActorData.Intelligence;
        Charisma     = _baseActorData.Charisma;
        Magic        = _baseActorData.Magic;

        DefaultZone  = _baseActorData.Zone;
        CurrentZone  = DefaultZone;
        // SetToZone();
        
        Health.RecalculateMaxHealth();

        initialized = true;
    }

    // public void InitializeStatisticsFromSaveFile()
}

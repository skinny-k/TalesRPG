using UnityEngine;
using SkinnyUtils;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Targeter
{
    public enum TargetType { None, Self, Other, Entity, Zone };
    public enum SelectType { None, UserSelect, PassIn, Random };

    public TargetType Target;
    public SelectType Selector;
    public int NumTargets = 1;
    [EnumFlagAttribute] public CombatZone Zones;

    public List<TargetConstraint> Constraints = new List<TargetConstraint>();

    // returns the number of actors remaining in the list after constraints are applied
    public int GetValidActors(List<CombatActor> actors)
    {
        if (actors == null || actors.Count == 0)
        {
            return 0;
        }
        else
        {
            // filter the list of all available actors by each constraint
            // if a constraint ever reduces list to 0, break
            for (int i = 0; i < Constraints.Count; i++)
            {
                if (Constraints[i].FilterActors(actors) == 0)
                {
                    break;
                }
            }
            return actors.Count;
        }
    }
    
    [Serializable]
    public class TargetConstraint
    {
        public enum TC_Type { None, Affiliation, Effect, Zone };
        public TC_Type t;
        
        public TargetConstraint()
        {
            t = TC_Type.None;
        }
        
        // TODO: try implementing GetAsSpecifiedType again and make sure T has no access modifiers?
        
        // returns the number of actors remaining in the list after filtering
        public virtual int FilterActors(List<CombatActor> actors)
        {
            if (actors == null || actors.Count == 0)
            {
                return 0;
            }
            else
            {
                // check if each actor is in the filter
                // if not, remove it
                for (int i = actors.Count - 1; i >= 0; i--)
                {
                    if (!IsInFilter(actors[i]))
                    {
                        actors.RemoveAt(i);
                    }
                }
                return actors.Count;
            }
        }

        protected virtual bool IsInFilter(CombatActor actor)
        {
            return false;
        }
    }

    // To create a new Target Constraint type, follow the steps below, using existing code as reference:
    //   1. Define a new subclass of TargetConstraint.
    //      a) Ensure IsInFilter() has been overridden.
    //      b) Ensure your subclass has a public 0-argument constructor that sets t to its display name in TC_Type.
    //   2. Add the display name of your subclass to the TC_Type enumeration.
    //   4. Add a new case to the switch statement in TargeterConstraintDrawer.OnGUI() to define how your subclass will be displayed.
    //   5. (Optional) Add a new case to the switch statement in TargeterConstraintDrawer.GetPropertyHeight() to define how tall your subclass is in the editor.
    
    [Serializable]
    public class TC_Affiliation : TargetConstraint
    {
        [EnumFlagAttribute] public TeamAffiliation _affiliations;

        public TC_Affiliation()
        {
            t = TC_Type.Affiliation;
        }
        
        protected override bool IsInFilter(CombatActor actor)
        {
            return _affiliations.HasFlag(actor.Affiliation);
        }
    }
    
    [Serializable]
    public class TC_Effect : TargetConstraint
    {
        public EffectType _effect;
        public int _count = 1;
        
        public TC_Effect()
        {
            t = TC_Type.Effect;
        }

        protected override bool IsInFilter(CombatActor actor)
        {
            return actor.HasEffect(_effect) >= _count;
        }
    }
    
    [Serializable]
    public class TC_Zone : TargetConstraint
    {
        [EnumFlagAttribute] public CombatZone _zones;
        
        public TC_Zone()
        {
            t = TC_Type.Zone;
        }

        protected override bool IsInFilter(CombatActor actor)
        {
            return _zones.HasFlag(actor.CurrentZone);
        }
    }
}

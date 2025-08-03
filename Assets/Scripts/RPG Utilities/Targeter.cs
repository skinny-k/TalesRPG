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
    abstract public class TargetConstraint
    {
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

        protected abstract bool IsInFilter(CombatActor actor);
    }

    [Serializable]
    class TC_Affiliation : TargetConstraint
    {
        [SerializeField][EnumFlagAttribute] TeamAffiliation _affiliation;

        protected override bool IsInFilter(CombatActor actor)
        {
            return _affiliation.HasFlag(actor.Affiliation);
        }
    }
}

using UnityEngine;

public class ActorHealth : MonoBehaviour
{
    CombatActor Actor;

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public void SetActor(CombatActor a)
    {
        if (Actor == null)
        {
            Actor = a;
        }
        else return;
    }

    public int RecalculateMaxHealth()
    {
        MaxHealth = Actor.Vitality * 10;
        return MaxHealth;
    }

    public int ModifyHealth (int amt)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amt, 0, MaxHealth);
        return CurrentHealth;
    }
}

using System;

namespace TowerCreep.Damage
{
    /// <summary>
    /// Primary type of a damage, True damage cannot be defended against. Physical can only be countered by Physical and
    /// Magical can only countered by Magical.
    /// </summary>
    [Serializable]
    public enum DamageType
    {
        Physical,
        Magical,
        True
    }

    /// <summary>
    /// Secondary type of a damage, all DamageSources will have a secondary, but DamageSinks may not. If the subtype is
    /// not specified, the defense applies to all incoming damage of that type.
    /// </summary>
    [Serializable]
    public enum DamageSubType
    {
        None,

        // Physical Subtypes
        Slashing,
        Piercing,
        Bludgeoning,

        // Magical Subtypes
        Fire,
        Water,
        Nature,
        Electric,
        Force,
        Life,
        Death
    }
}
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public delegate bool BlockConditionDelegate(Transform source, out BlockDirectionInformation blockDirectionInformation);
}

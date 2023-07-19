using Unity.Mathematics;

namespace Events
{
    // This struct describes additional, optional, details about collision of 2 bodies
    public struct Details
    {
        internal bool IsValid;

        // If 1, then it is a vertex collision
        // If 2, then it is an edge collision
        // If 3 or more, then it is a face collision
        public int NumberOfContactPoints;

        // Estimated impulse applied
        public float EstimatedImpulse;

        // Average contact point position
        public float3 AverageContactPointPosition;

        public Details(int numContactPoints, float estimatedImpulse, float3 averageContactPosition)
        {
            IsValid = (0 < numContactPoints); // TODO: Should we add a max check?
            NumberOfContactPoints = numContactPoints;
            EstimatedImpulse = estimatedImpulse;
            AverageContactPointPosition = averageContactPosition;
        }
    }
}

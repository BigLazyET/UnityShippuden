using System.Linq;
using Unity.Collections;

namespace Events
{
    public struct StatefulSimulationEventBuffers<T> where T : unmanaged, IStatefulSimulationEvent<T>
    {
        public NativeList<T> Previous;

        public NativeList<T> Current;

        public void AllocateBuffers()
        {
            Previous = new NativeList<T>(Allocator.Persistent);
            Current = new NativeList<T>(Allocator.Persistent);
        }

        public void ReleaseBuffers()
        {
            if (Previous.IsCreated) Previous.Dispose();
            if (Current.IsCreated) Current.Dispose();
        }

        public void SwapBuffers()
        {
            (Previous, Current) = (Current, Previous);
            Current.Clear();
        }

        public void GetStatefulEvents(NativeList<T> statefulEvents, bool sortCurrent = true) => GetStatefulEvents(Previous, Current, statefulEvents, sortCurrent);

        public static void GetStatefulEvents(NativeList<T> previousEvents, NativeList<T> currentEvents, NativeList<T> statefulEvents, bool sortCurrent = true)
        {
            if (sortCurrent) currentEvents.Sort();

            statefulEvents.Clear(); // statefulEvents其实是最终的结果

            var p = 0;
            var c = 0;
            while (p < previousEvents.Count() && c < currentEvents.Count())
            {
                var previous = previousEvents[p];
                var current = currentEvents[c];

                //var r = ISimulationEventUtilities.CompareEvents(current, previous);
                var r = previous.CompareTo(current);
                if (r == 0)
                {
                    current.State = StatefulEventState.Stay;
                    statefulEvents.Add(current);
                    p++;
                    c++;
                }
                else if (r > 0)
                {
                    current.State = StatefulEventState.Exit;
                    statefulEvents.Add(current);
                    c++;
                }
                else
                {
                    previous.State = StatefulEventState.Enter;
                    statefulEvents.Add(previous);
                    p++;
                }
            }

            if (c == currentEvents.Length)
            {
                while (p < previousEvents.Length)
                {
                    var previous = previousEvents[p];
                    previous.State = StatefulEventState.Exit;
                    statefulEvents.Add(previous);
                    p++;
                }
            }

            if (p == previousEvents.Length)
            {
                while (c < currentEvents.Length)
                {
                    var current = currentEvents[c];
                    current.State = StatefulEventState.Enter;
                    statefulEvents.Add(current);
                    c++;
                }
            }
        }
    }
}

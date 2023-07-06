using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void GetStatefulEvents(NativeList<T> statefulEvents, bool sortCurrent = true);

        public static void GetStatefulEvents(NativeList<T> previousEvents, NativeList<T> currentEvents, NativeList<T> statefulEvents, bool sortCurrent = true)
        {
            if (sortCurrent) currentEvents.Sort();

            statefulEvents.Clear(); // statefulEvents其实是最终的结果


        }
    }
}

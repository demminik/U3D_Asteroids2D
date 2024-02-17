using System;

namespace Asteroids.Core.Tickables {

    /// <summary>
    /// Very basic tickables collection
    /// </summary>
    public class TickableSet : ITickable, IDisposable {

        // TODO: autoextend (not important for demo)
        private ITickable[] _tickables;
        private int _count;

        public TickableSet(int maxCapacity) {
            _tickables = new ITickable[maxCapacity];
            _count = 0;
        }

        public bool Add(ITickable item) {
            if (_count >= _tickables.Length) {
                return false;
            }

            _tickables[_count] = item;
            _count++;
            return true;
        }

        public void Dispose() {
            _tickables = null;
        }

        public void Tick(float deltaTime) {
            for (int i = 0; i < _count; i++) {
                var item = _tickables[i];
                if (item != null) {
                    item.Tick(deltaTime);
                }
            }
        }
    }
}
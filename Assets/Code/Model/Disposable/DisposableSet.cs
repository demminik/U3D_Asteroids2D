using System;
using System.Collections.Generic;

namespace Asteroids.Core.Disposables {

    /// <summary>
    /// Basic set of disposable objects
    /// </summary>
    public class DisposableSet : IDisposable {

        private HashSet<IDisposable> _disposables = new HashSet<IDisposable>();

        public void Add(IDisposable item) {
            if (item != null && !_disposables.Contains(item)) {
                _disposables.Add(item);
            }
        }

        public void Remove(IDisposable item) {
            if (item != null && _disposables.Contains(item)) {
                _disposables.Remove(item);
            }
        }

        public void Dispose() {
            foreach (var item in _disposables) {
                if (item != null) {
                    item.Dispose();
                }
            }
            _disposables.Clear();
        }
    }
}
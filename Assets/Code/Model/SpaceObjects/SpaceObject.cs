using Asteroids.Core.Disposables;
using Asteroids.Core.SpaceObjects.Modules;
using Asteroids.Core.Tickables;
using Asteroids.Core.Transforms;
using System;

namespace Asteroids.Core.SpaceObjects {

    public abstract class SpaceObject : ITransformable, ITickable, IDisposable {

        public IResilienceModule ResilienceModule { get; private set; }

        public ObjectTransform Transform { get; private set; }

        protected virtual int MaxTickablesAmount => 0;
        protected TickableSet _tickables;

        protected DisposableSet _disposables;

        public SpaceObject(ObjectTransform transform, IResilienceModule resilienceModule) {
            _disposables = new DisposableSet();

            _tickables = new TickableSet(MaxTickablesAmount);
            _disposables.Add(_tickables);

            Transform = transform;
            _disposables.Add(Transform);

            ResilienceModule = resilienceModule;
            RegisterModule(resilienceModule);
        }

        protected void RegisterModule(ISpaceObjectModule module) {
            _disposables.Add(module);
            if (module is ITickable) {
                _tickables.Add(module as ITickable);
            }
        }

        public virtual void Dispose() {
            _disposables.Dispose();
            _disposables = null;

            ResilienceModule = null;
            Transform = null;
            _tickables = null;
        }

        public virtual void Tick(float deltaTime) {
            _tickables.Tick(deltaTime);
        }
    }
}
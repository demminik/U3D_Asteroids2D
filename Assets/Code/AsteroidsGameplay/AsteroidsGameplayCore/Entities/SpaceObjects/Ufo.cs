using Asteroids2D.Gameplay.Skins;

namespace Asteroids2D.Gameplay.Entities {

    public class Ufo : SkinnableSpaceObject<Asteroids.Core.SpaceObjects.Ufo, SpaceObjectSkin> {

        protected override void OnDestroy() {
            if (Model != null) {
                Model.Transform.OnPositionChanged -= ProcessModelPositionChange;
            }

            base.OnDestroy();
        }

        protected override void InitializeModelInternal() {
            base.InitializeModelInternal();

            Model.Transform.OnPositionChanged += ProcessModelPositionChange;
        }

        private void ProcessModelPositionChange() {
            transform.position = Model.Transform.Position;
        }
    }
}
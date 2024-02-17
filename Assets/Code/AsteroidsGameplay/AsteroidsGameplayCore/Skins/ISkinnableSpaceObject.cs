namespace Asteroids2D.Gameplay.Skins {

    public interface ISkinnableSpaceObject<TSkin> where TSkin : SpaceObjectSkin {

        public void ApplySkin(TSkin skinInstance);
        public void ClearCurrentSkin();
    }
}

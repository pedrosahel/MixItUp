namespace Main
{
    using UnityEngine;

    public class ColorManager : Singleton<ColorManager>
    {
        public Material[] colors;

        public GameObject rolha;

        private new void Awake() 
        {
            base.Awake();
        }
    }
}
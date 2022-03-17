namespace Main 
{
    using UnityEngine;

    public class ClickEvents : Singleton<ClickEvents>
    {
        public delegate void OnButton();
        public static event OnButton _MovingRight;
        public static event OnButton _MovingLeft;

        public static event OnButton _PaintBlue;
        public static event OnButton _PaintRed;
        public static event OnButton _PaintYellow;
        public static event OnButton _PutTampa;

        public void MoveRight()
        {
            _MovingRight?.Invoke();
        }

        public void MoveLeft()
        {
            _MovingLeft?.Invoke();
        }

        public void PaintBlue()
        {
            _PaintBlue?.Invoke();
        }
        public void PaintRed()
        {
            _PaintRed?.Invoke();
        }
        public void PaintYellow()
        {
            _PaintYellow?.Invoke();
        }
        public void PutTampa()
        {
            _PutTampa?.Invoke();      
        }
    }
}
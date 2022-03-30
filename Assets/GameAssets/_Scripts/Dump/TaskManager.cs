 namespace Main
 {
    using UnityEngine;
    using System.Collections;
    public class TaskManager : Singleton<TaskManager>
    {
        public delegate void OnTask(string color, string frasco, bool tampa = false);

        public static event OnTask _Task;
        public static event OnTask _OnDelivery;

        private enum TaskState {OnlyPrimary, BothPrimarySecundary, OnlySecundary}
        private enum CurrentColor {blue, red, yellow, purple, green, orange}
        private enum CurrentFrasco {Frasco_01, Frasco_02, Frasco_03}
        private CurrentColor myColor;
        private CurrentFrasco myFrasco;

        private TaskState myState;

        private int randomColorIndex;
        private int randomFrasco;
        private int colorsLenght;
        private int frascoLenght;

        private bool deliveryBoxFull = false;
        
        private void Start() 
        {
            var array = System.Enum.GetValues(typeof(CurrentColor));
            var array2 = System.Enum.GetValues(typeof(CurrentFrasco));

            colorsLenght = array.Length; 
            frascoLenght = array2.Length;

            RandomTask();
        }

        private void OnEnable()
        {
            if(DeliveryBoxManager.Instance == null) return;

            DeliveryBoxManager._IsFull += Full;
            DeliveryBoxManager._IsEmpty += Empty;
        }

        private void OnDisable()
        {
            if(DeliveryBoxManager.Instance == null) return;
            
            DeliveryBoxManager._IsFull -= Full;
            DeliveryBoxManager._IsEmpty -= Empty;
        }

        public void RandomTask()
        {
            switch(myState)
            {
                case TaskState.OnlyPrimary:
                    randomColorIndex = Random.Range(0, 3);
                break;
                case TaskState.BothPrimarySecundary:
                    this.randomColorIndex = Random.Range(0, this.colorsLenght);
                break;
                case TaskState.OnlySecundary:
                    this.randomColorIndex = Random.Range(3, this.colorsLenght);
                break;
            }

            randomFrasco = Random.Range(0, this.frascoLenght);

            myColor = (CurrentColor)randomColorIndex;
            myFrasco = (CurrentFrasco)randomFrasco;

            _Task?.Invoke(myColor.ToString(), myFrasco.ToString());
        }

        public void Check(string color, string frasco, bool tampa = true)
        {
            if(deliveryBoxFull) return;

            if(!deliveryBoxFull)
            {
                _OnDelivery?.Invoke(color, frasco, tampa);

                if(tampa)
                {
                    if(color == myColor.ToString() && frasco == myFrasco.ToString())
                    {
                        if(GameManager.Instance != null) GameManager.Instance.OnSucess();
                    }
                    else
                    {
                        if(GameManager.Instance != null) GameManager.Instance.OnFailed(); 
                    }
                }
                else if (!tampa)
                {
                    if(GameManager.Instance != null) GameManager.Instance.OnFailed();
                }

                StartCoroutine(CallRandomTask());
            }
        }

        public IEnumerator CallRandomTask()
        {
            yield return new WaitForSeconds(0.5f);
            RandomTask();
        }

        public void ActivedSecundary(string timerStatus)
        {
            switch(timerStatus)
            {
                case "Half":
                    this.myState = TaskState.BothPrimarySecundary;
                break;
                case "Quarter":
                    this.myState = TaskState.OnlySecundary;
                break;
            }
        }

        public void Full()
        {
            this.deliveryBoxFull = true;
        }

        public void Empty()
        {
            this.deliveryBoxFull = false;
        }
    }
}
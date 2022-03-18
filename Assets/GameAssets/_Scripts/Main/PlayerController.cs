namespace Main
{
    using UnityEngine;

    [RequireComponent(typeof (Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public enum CurrentColor {empty, blue, red, yellow, purple, green, orange}
        public CurrentColor myColor;
        [SerializeField] private float b_speed;

        #region Color Variables
        private enum OnPosition {blue, red, yellow, tampa}
        private OnPosition myPosition;

        private bool isClosed;

        #endregion

        #region Movement Variables.
        private bool canMove = true;
        private bool isPressed = false;

        private int index = 1;
        private int currentWaypoint = 1;
        #endregion

        #region Private Components.
        private Transform[] waypoints;
        private Rigidbody p_rb;

        private Transform rolhaPos;

        private MeshRenderer mr;

        private BoxCollider box;

        #endregion

    #region Unity Functions.
    private void OnEnable() 
    {
        this.waypoints = FindObjectOfType<Waypoint>().waypoints;
        this.p_rb = GetComponent<Rigidbody>();
        this.box = GetComponent<BoxCollider>();
        this.mr = this.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>();
        this.rolhaPos = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        Reset();

        if(ClickEvents.Instance == null) return;

        ClickEvents._PaintBlue += Blue;
        ClickEvents._PaintRed += Red;
        ClickEvents._PaintYellow += Yellow;
        ClickEvents._PutTampa += Tampa;
        ClickEvents._MovingRight += Increase;
        ClickEvents._MovingLeft += Decrease;
    }

    private void OnDisable()
    {
        if(ClickEvents.Instance == null) return;

        ClickEvents._MovingRight -= Increase;
        ClickEvents._MovingLeft -= Decrease;
        ClickEvents._PaintBlue -= Blue;
        ClickEvents._PaintRed -= Red;
        ClickEvents._PaintYellow -= Yellow;
        ClickEvents._PutTampa -= Tampa;
    }

    private void Update()
    {
        Movement();
    }

    private void OnTriggerEnter(Collider other) 
    {
        switch(other.tag)
        {
            case "Descarte":
                    Descarte();
            break;
            case "Delivery":
                    Delivery();
            break;
            case "Blue":
                    if(this.myPosition == OnPosition.blue) return;
                    this.myPosition = OnPosition.blue;
                break;
            case "Red":
                    if(this.myPosition == OnPosition.red) return;
                    this.myPosition = OnPosition.red;
                break;
            case "Yellow":
                    if(this.myPosition == OnPosition.yellow) return;
                    this.myPosition = OnPosition.yellow;
                break;
            case "Tampa":
                    if(this.myPosition == OnPosition.tampa) return;
                    this.myPosition = OnPosition.tampa;
                break;
        }
    }
    #endregion

    #region Public Functions.
    public void Increase()
    {
            this.canMove = false;

            if(index + 1 >= waypoints.Length) index = waypoints.Length - 1;

            else index += 1;
    }

    public void Decrease()
    {
            this.canMove = false;

            if(index - 1 < 0) index = 0;
            else index -= 1;
    }
    #endregion.

    #region  private Functions.
    private void Movement()
    {
            if(this.waypoints == null) return;

            if(currentWaypoint != index && index >= 0)
            {
                float distance = Vector3.Distance(this.transform.position, this.waypoints[this.index].position);
                
                this.transform.position = Vector3.MoveTowards(this.transform.position,
                                                            this.waypoints[this.index].position, 
                                                            this.b_speed * Time.fixedDeltaTime);


                if(distance < 0.1f)
                {
                    this.currentWaypoint = this.index;
                    this.canMove = true;
                }
            }
    }
    private void Blue()
        {
            if(myPosition != OnPosition.blue) return;
            if(this.myColor == CurrentColor.empty)
            {
                this.mr.material = ColorManager.Instance.colors[0]; // azul
                myColor = CurrentColor.blue;
            }
            else
            {
                switch(myColor)
                {
                    case (CurrentColor.red):
                    this.mr.material = ColorManager.Instance.colors[3]; //roxo
                    myColor = CurrentColor.purple;
                    break;

                    case (CurrentColor.yellow):
                    this.mr.material = ColorManager.Instance.colors[4]; //verde
                    myColor = CurrentColor.green;
                    break;
                }
            }
        }

    private void Red()
    {
        if(myPosition != OnPosition.red) return;
        if(this.myColor == CurrentColor.empty)
        {
            this.mr.material = ColorManager.Instance.colors[1]; // vermelho
            myColor = CurrentColor.red;
        }
        else
        {
            switch(myColor)
            {
                case (CurrentColor.blue):
                this.mr.material = ColorManager.Instance.colors[3]; //roxo
                myColor = CurrentColor.purple;
                break;
                
                case (CurrentColor.yellow):
                this.mr.material = ColorManager.Instance.colors[5]; //laranja
                myColor = CurrentColor.orange;
                break;
            }
        }
    }
    private void Yellow()
    {
        if(myPosition != OnPosition.yellow) return;
        if(this.myColor == CurrentColor.empty)
        {
            this.mr.material = ColorManager.Instance.colors[2]; //amarelo
            myColor = CurrentColor.yellow;
        }
        else
        {
            switch(myColor)
            {
                case (CurrentColor.red):
                this.mr.material = ColorManager.Instance.colors[5]; //laranja
                myColor = CurrentColor.orange;
                break;
                
                case (CurrentColor.blue):
                this.mr.material = ColorManager.Instance.colors[4]; //verde
                myColor = CurrentColor.green;
                break;
            }
        }
    }
    private void Tampa()
    {
        if(myPosition != OnPosition.tampa) return;
        GameObject obj = ObjectPooling.Instance.GetObjectFromPool("Tampa");
        obj.transform.position = this.rolhaPos.position;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.SetParent(this.transform);
        obj.SetActive(true);
        isClosed = true;
    }

    private void Reset()
    {
        this.index = 1;
        this.currentWaypoint = 1;
        this.myColor = CurrentColor.empty;
        this.box.isTrigger = false;
        this.mr.material = ColorManager.Instance.colors[6]; // vazio

        for(int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).tag == "Tampa") transform.GetChild(i).gameObject.SetActive(false);
            this.isClosed = false;
        }
    }
    private void Descarte()
    {
            canMove = false;
            this.gameObject.SetActive(false);
            if(SpawnerManager.Instance != null) SpawnerManager.Instance.Spawned(false);
            if(_TaskManager.Instance != null) _TaskManager.Instance.Descarte(myColor.ToString(), this.gameObject.tag);
    }

    private void Delivery()
    {
            canMove = false;
            if(GameManager.Instance != null) GameManager.Instance.OnDelivery();
            if(SpawnerManager.Instance != null) SpawnerManager.Instance.Spawned(false);
            if(_TaskManager.Instance != null) _TaskManager.Instance.Check(myColor.ToString(), this.gameObject.tag, isClosed);
            if(DeliveryBoxManager.Instance != null) DeliveryBoxManager.Instance.ShowContainer();
            this.gameObject.SetActive(false);
    }
    #endregion
    }
}
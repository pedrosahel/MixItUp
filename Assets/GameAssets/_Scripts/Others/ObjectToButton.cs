using UnityEngine.Events;
using UnityEngine;

public class ObjectToButton : MonoBehaviour
{
    public UnityEvent unityEvent = new UnityEvent();
    
    private GameObject button;

    private Animator anim;

    private void Awake() 
    {
        this.button = this.gameObject;
        this.anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        Click();
    }
    private void Click()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                this.anim.SetTrigger("isClicked");
                unityEvent?.Invoke();
            }
        }
    }
}

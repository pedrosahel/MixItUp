using System.Collections.Generic;
using UnityEngine;

public class DeliveryBoxManager : Singleton<DeliveryBoxManager>
{

    public delegate void OnBox ();

    public static OnBox _IsFull;
    public static OnBox _IsEmpty;

    [SerializeField] private GameObject[] containers;

    private int maxContainers = 5;
    private int currentContainer = 0;
    private bool isFull = false;

    private void Start() 
    {
        this.maxContainers = containers.Length - 1;
    }

    public void ShowContainer()
    {
        if(this.isFull) return;

        this.containers[this.currentContainer].SetActive(true);
        
        if(currentContainer + 1 > this.maxContainers) 
        {
            this.currentContainer = this.maxContainers;
            this.isFull = true;
            Full();
        }
        else currentContainer += 1;
    }

    public void ResetBox()
    {
        foreach(GameObject obj in containers)
        {
            obj.SetActive(false);
            this.isFull = false;
            currentContainer = 0;

            Empty();
        }
    }

    public void Full()
    {
        _IsFull?.Invoke();
    }

    public void Empty()
    {
        _IsEmpty?.Invoke();
    }

}

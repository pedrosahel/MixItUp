using UnityEngine;

public class BombAnimation : MonoBehaviour
{
    private bool onPlay = false;
    private Animator anim;

   private void Start() 
   {
       this.anim = this.GetComponent<Animator>();
   } 
   public void Play()
   {
       this.anim.SetTrigger("Play");
   }


}

using UnityEngine;

public class RollAnimation : MonoBehaviour
{
    public Animator[] roll_Anim;

    public MeshRenderer esteiraMaterial;

    public float offset;

    public void Roll(string side)
    {
        foreach (Animator anim in roll_Anim)
        {
            anim.SetTrigger(side);
        }
    }

    public void Roll(float direction)
    {
        var currentOffset = esteiraMaterial.material.GetTextureOffset("_MainTex");
        float xOffset = Mathf.Abs(currentOffset.x);

        esteiraMaterial.material.SetTextureOffset("_MainTex", new Vector2(
                                                                         Mathf.Lerp(currentOffset.x, (xOffset + Mathf.Abs(offset)) * direction, 0.2f)
                                                                         , 0
                                                                         ));
    }
}

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

        esteiraMaterial.material.SetTextureOffset("_MainTex", Vector2.Lerp(currentOffset,
                                                                            new Vector2((xOffset + offset) * direction, 0),
                                                                            0.2f
                                                                            ));
    }
}

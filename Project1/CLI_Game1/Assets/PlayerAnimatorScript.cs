using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    //getting animator 
    [SerializeField] private Animator anim;

    private PlayerMovement player; 
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if player is here
        if (player == null) return;
        //triger jump animation, stop land animation
        if (player.jumpThisFrame)
        {
            anim.SetTrigger(JumpKey);
            anim.ResetTrigger(GroundedKey);
        }
        //triger land animation, stop jump animation
        if (player.landThisFrame)
        {
            anim.SetTrigger(GroundedKey);
            anim.ResetTrigger(JumpKey);
        }

        //Debug.Log(player.landThisFrame);

    }

    #region Animation Keys
    private static readonly int GroundedKey = Animator.StringToHash("Grounded");
    private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
    private static readonly int JumpKey = Animator.StringToHash("Jump");
    #endregion
}

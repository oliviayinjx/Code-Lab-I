using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    //getting animator 
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] _footsteps;
    [SerializeField] private ParticleSystem jumpParticles, landParticles, launchParticles;

    private Color color; 

    bool isPlaying;

    private PlayerMovement player; 
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        color = player.spriteRenderer.color;
        SetColor(jumpParticles);
        SetColor(landParticles);
        SetColor(launchParticles);
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
            isPlaying = false;
            jumpParticles.Play();
        }
        //triger land animation, stop jump animation
        if (player.landThisFrame)
        {
            anim.SetTrigger(GroundedKey);
            anim.ResetTrigger(JumpKey);
            if (!isPlaying)
            {
                isPlaying = true;
                source.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);
                landParticles.Play();
            }
        }
        else
        {
            isPlaying = false; 
        }


        //Debug.Log(isPlaying);


    }
    //change color of particle system
    void SetColor(ParticleSystem ps)
    {
        var main = ps.main;
        main.startColor = color;
    }

    #region Animation Keys
    private static readonly int GroundedKey = Animator.StringToHash("Grounded");
    private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
    private static readonly int JumpKey = Animator.StringToHash("Jump");
    #endregion
}

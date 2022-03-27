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
    bool checkColor; 

    private PlayerMovement player;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        if (!checkColor)
        {
            color = player.selectColor;
            SetColor(jumpParticles);
            SetColor(landParticles);
            SetColor(launchParticles);
            checkColor = true;
        }

        



        //check if player is here
        if (player == null) return;
        //triger jump animation, stop land animation

        if (player.jumpThisFrame)
        {
            anim.SetTrigger(JumpKey);
            anim.ResetTrigger(GroundedKey);
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

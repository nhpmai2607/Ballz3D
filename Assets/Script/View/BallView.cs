using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour {
    public Material[] materials;
    public AudioClip bounceSound;

    private AudioSource audioSource;
    private LineRenderer directionLine;

    private void Awake()
    {
        directionLine = GetComponent<LineRenderer>();
        GetComponent<MeshRenderer>().material = PlayerPrefs.HasKey("Ball") && materials.Length > PlayerPrefs.GetInt("Ball")
            ? materials[PlayerPrefs.GetInt("Ball")] : materials[0];
    }

    // Use this for initialization
    void Start () {
        audioSource = GameObject.Find("SoundSource").GetComponent<AudioSource>();
	}

    public void RenderDirectionLine(Vector3 pos0, Vector3 pos1)
    {
        directionLine.enabled = true;
        directionLine.SetPosition(0, pos0);
        directionLine.SetPosition(1, pos1);
    }

    public void Bouncing()
    {
        directionLine.enabled = false;
    }

    public void PlayEffectSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(bounceSound);
        }
    }
}

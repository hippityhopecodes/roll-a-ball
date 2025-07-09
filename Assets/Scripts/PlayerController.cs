using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public int[] winCondition;
    public AudioSource[] playerAudio;

    private Rigidbody rb;
    public LayerMask groundLayers;
    public float jumpForce = 7;
    public SphereCollider col;
    private int count; 
    private int level;

    public LayerMask GroundLayers { get => groundLayers; set => groundLayers = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent <SphereCollider>();
        count = 0;
        StartCoroutine(SetCountText()); 
        SetCountText();
        winText.text = "";
        level = SceneManager.GetActiveScene().buildIndex;
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //When entering a trigger, check for the "Pick Up" tag. If true, make the object disappear, play a sound, and update the Count Text in the UI
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            playerAudio[0].Play();
            count = count + 1;
            StartCoroutine(SetCountText());
        }
    }
    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
             col.bounds.min.y, col.bounds.center.z), col.radius * .9f, layerMask: groundLayers);
    }

    private void OnTriggerExit(Collider other)
    {
        //Restarts level when exiting a trigger named Respawn
        if (other.gameObject.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(level);
        }
    }  

    IEnumerator SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 13)
         {
            winText.text = "You Win!";
            playerAudio[1].Play();
            yield return new WaitForSeconds(3);
            LoadNextScene();
         }

    }    

    public void LoadNextScene ()
    {
        int currentSceneIndex = level;
        //Load the first scene if there is no next scene, based on winCondition array size
        if (currentSceneIndex == winCondition.Length - 1)
        {
            LoadStartScene();
            return;
        }
        //Loads the next scene in the build manager
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoadPreviouisScene()
    {
        //Load the previous scene in the build manager
        int currentSceneIndex = level;
        SceneManager.LoadScene(currentSceneIndex - 1);
    } 
     
    public void LoadStartScene()
    {
        //Load the first scene in our build manager
        SceneManager.LoadScene(0);
    }
} 





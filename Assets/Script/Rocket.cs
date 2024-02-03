using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource sound;

    [SerializeField] float rcsThrust = 175f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float addedFuel;
    [SerializeField] float fuel = 100f;
    [SerializeField] float fuelComsup;

    const string friendly = "Friendly";
    const string finish = "Finish";
    const string visible = "visible";
    const string fuelCapsule = "FuelCapsule";

    SharedData sd = SharedData.instance;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip nextLevelClip;

    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem nextLevelParticles;

    [SerializeField] TMP_Text txtFuel;
    [SerializeField] TMP_Text txtScore;

    enum State {Alive, Dead, Transcending }
    State state;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
        QualitySettings.shadows = ShadowQuality.Disable;
        setStateAlive();
        print("State is: " + state);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
            RocketMove();
    }
    private void RocketMove()
    {
        float rotateOffset = rcsThrust * Time.deltaTime;
        float mainOffset = mainThrust * Time.deltaTime;

        rigidbody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * rotateOffset);
        }
        else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward * rotateOffset);
        }
        else if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainOffset);
        }

        rigidbody.freezeRotation = false;

        if (Input.GetKey(KeyCode.Space) && fuel > 0)
        {
            rigidbody.AddRelativeForce(Vector3.up * mainOffset);
            if( state == State.Alive && !sound.isPlaying)
            {
                sound.PlayOneShot(mainEngine);
                thrustParticles.Play();
            }

            setFuel(fuel - fuelComsup * Time.deltaTime);
        }
        else
        {
            if( sound.isPlaying)
                sound.Stop();
            thrustParticles.Stop();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
            return;

        switch (collision.gameObject.tag)
        {
            case friendly:
                // Do nothing
                break;
            case finish:
                nextLevelLogic();
                break;
            case visible:
                // Hacer visible el objeto
                Renderer renderer = collision.gameObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.enabled = true; // Esto hace visible el objeto
                }
                restartLevelLogic();
                break;
            case fuelCapsule:
                addFuel(addedFuel);
                break;
            default:
                restartLevelLogic();
                break;
        }
    }
    private void nextLevelLogic()
    {
        //print("Hit Obstacle");
        calcScore();
        showScore();
        setStateTranscending();
        Invoke("loadNextLevel", 1f);
        sound.Stop();
        sound.PlayOneShot(nextLevelClip);
        thrustParticles.Stop();
        nextLevelParticles.Play();
    }
    private void restartLevelLogic()
    {
        //print("Dead");
        setStateDead();
        Invoke("loadCurrentLevel", 2f);
        sound.Stop();
        sound.PlayOneShot(deathClip);
        thrustParticles.Stop();
        deathParticles.Play();
    }
    private void loadCurrentLevel()
    {
        //SceneManager.LoadScene(0);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void loadNextLevel()
    {
        //SceneManager.LoadScene(1);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            //print("No hay más niveles");
            return;
        }
       
    }
    private void setStateAlive()
    {
        state = State.Alive;
    }
    private void setStateDead()
    {
        state = State.Dead;
    }
    private void setStateTranscending()
    {
        state = State.Transcending;
    }

    private void showScore()
    {
        int temp = sd.scores[sd.currentLevel - 1];
        txtScore.SetText($"{temp}");
    }

    private void calcScore()
    {
        sd.currentLevel += 1;
        print(sd.currentLevel);
        sd.scores[sd.currentLevel - 1] = 1000 + (int) Math.Round(fuel) * 10;
    }

    private void showFinalScore()
    {

    }

    private void addFuel(float f)
    {
        setFuel(fuel + f);
    }

    private void setFuel(float f)
    {
        fuel = f;
        if (fuel < 0)
            txtFuel.SetText("Fuel: 0%");
        else
            txtFuel.SetText("Fuel: " + (int)Math.Round(fuel) + "%");
    }
}


    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerController : MonoBehaviour
    {
        // Reference to the RandomBoxes script
        private RandomBoxes randomBoxes;
        
        //connecting score to Player
        private ScoreManager scoreManager;

        // Base speed of the player
        private float baseSpeed = 300.0f;

        // Particle system for fireworks
        public ParticleSystem fireworkParticle;

        // Z-axis boundary for player movement
        private float zBound = 8;

        // Current speed of the player
        private float speed = 20;

        // Rigidbody component of the player
        private Rigidbody playerRb;

        // Flag indicating whether the game is over
        public bool gameOver;

        // Audio clip for collision sound
        public AudioClip crashSound;

        // AudioSource component for playing sounds
        public AudioSource playerAudio;

        // Audio clip for powerup sound
        public AudioClip boneSound;

        // Particle system for player explosion
        public ParticleSystem explosionParticle;

        // Reference to RandomBoxes script through property
        public RandomBoxes RandomBoxes { get => randomBoxes; set => randomBoxes = value; }

        // Another reference to RandomBoxes script through property (Note: Both properties point to the same field)
        public RandomBoxes RandomBoxes1 { get => randomBoxes; set => randomBoxes = value; }

        public LivesManager LivesManager = new LivesManager();

        // Start is called before the first frame update
        void Start()
        {
            // Get the Rigidbody component
            playerRb = GetComponent<Rigidbody>();
        
            // Initialize speed with baseSpeed
            speed = baseSpeed;

            // Get the AudioSource component
            playerAudio = GetComponent<AudioSource>();

            // Find and get the RandomBoxes script from the "RandomBoxes" GameObject
            randomBoxes = GameObject.Find("RandomBoxes").GetComponent<RandomBoxes>();
        }

        // Update is called once per frame
        void Update()
        {
            // Check if the game is active before allowing player movement
            if (randomBoxes.isGameActive)
            {
                MovePlayer();
                ConstrainPlayerPosition();
            }
            
        }

        // Move the player based on arrow keys
        void MovePlayer()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            // Create a movement direction vector
        Vector3 moveDirection = new Vector3(horizontalInput,  0.0f, verticalInput);
    
        playerRb.AddForce(transform.TransformDirection(moveDirection) * speed);

        
        }

        // Constrain the player's position to stay within bounds
        void ConstrainPlayerPosition()
        {
            // Check and adjust the player's position on the Z-axis
            if (transform.position.z < -zBound)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
            }
            if (transform.position.z > zBound)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
            }
        }

        // Public method to get the speed
        public float GetSpeed()
        {
            return speed;
        }

        // Public method to set the speed
        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

        // Called when a collision occurs
        private void OnCollisionEnter(Collision collision)
        {
            // Check if the collided object has a specific tag
            if (collision.gameObject.CompareTag("Box1") ||
                collision.gameObject.CompareTag("Box2") ||
                collision.gameObject.CompareTag("Box3") ||
                collision.gameObject.CompareTag("Box4"))
            {
                // Log collision information
                Debug.Log("Player has collided with a box.");

                // Play fireworks particle effect
                fireworkParticle.Play();

                // Play collision sound
                playerAudio.PlayOneShot(crashSound, 1.0f);
                
                if (LivesManager.livesCount == 0)
                {
            // LivesManager.UpdateLivesText();
                Debug.Log("Calling GameOver");
                randomBoxes.GameOver();
                
                }
                // Deduct a life in the LivesManager script
                Debug.Log("should be calling Player hit");
                LivesManager.livesCount -= 1;
                Debug.Log("Lives: " + LivesManager.livesCount);
            Debug.Log(LivesManager.livesCount);
                LivesManager.UpdateLivesText();
                
            }
        }

    // Called when a trigger collider is entered
    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger collider belongs to a regular powerup
        if (other.gameObject.tag == "Powerup")
        {
            Debug.Log("Powerup collected!");
            
            Destroy(other.gameObject);
            ScoreManager.scoreCount  += 1;


            // Check if playerAudio is not null before playing the sound
            if (playerAudio != null && boneSound != null)
            {
                playerAudio.PlayOneShot(boneSound, 1.0f);
            }

            // Check if the game is still active before instantiating the explosion particle
            if (randomBoxes.isGameActive)
            {
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            }

        }

        // Check if the trigger collider belongs to a special powerup
        else if (other.gameObject.tag == "Powerup1")    
            {

            // Mark the powerup as collected to avoid processing it again
            other.isTrigger = false;

            Debug.Log("Powerup1 collected!");

            Destroy(other.gameObject);
            ScoreManager.scoreCount += 5;

        }
      }

    }
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private float xvel;
    private float yvel;
    private Vector3 _pos;
    private Boolean _inputLeft, _inputRght, _inputRollLeft, _inputRollRght;
    private Boolean _inputFire, _inputEsc;
    private float PlayableWidthBase = 100f; //Initial Playable Area. Will adjust this based on if the camera is set to zoom in or out. Basically set so we can give side borders for info.

    private Transform _transform;
    [SerializeField] private PlayerStats _playerStats;

    [SerializeField] private float accelFactor;
    [SerializeField] private int _speedPoints;

    private float _inputYaw, _inputRoll, _inputPitch, _inputForward, _inputHorizontal, _inputVertical;
    // Start is called before the first frame update

    /*
     * Cannon mount spacing and reference points: 4m offset from centre on the X axis each side. 300mm offset per outer cannon, 3 so far baked into reference model (should be configurable on the final game asset)
     *                                            0.45 -Z (-Y in Unity) Axis for all cannons.
     *                                            0.4  -Y (-Z in Unity) Axis plus positive offset (cannon length, default 1m) for projectile generation.
     *                                            Currently no central cannon designation. Considering one for final game asset once a proper game loop is instituted.
     */

    void SpawnProjectile() {
        // Projectile Spawn Mechanism - Curently only fires one type of projectile.
        // SWITCH statement will be used to classify weapon types. Additional parameters will be used depending on weapon upgrades.
        
    }
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        accelFactor = 0.15f;
        _speedPoints = 1;
        print("Test game code start");
        xvel = 0f;
        yvel = 0f;
        _pos = new Vector3(1f,1f,-1f);
        _transform = GetComponent<Transform>();
        _transform.SetPositionAndRotation( _pos, Quaternion.identity );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _speedPoints = _playerStats.shipSpeed;
        // Input declarations
        _inputLeft     = Input.GetKey(KeyCode.A);
        _inputRght     = Input.GetKey(KeyCode.D);
        _inputFire     = Input.GetKey(KeyCode.Space);
        _inputEsc      = Input.GetKey(KeyCode.Escape);
        _inputRollLeft = Input.GetKey(KeyCode.Q);
        _inputRollRght = Input.GetKey(KeyCode.E);
        _inputVertical = Input.GetAxisRaw("Vertical");
        // Input handlers
        if (_inputLeft)
        {
            _inputHorizontal = -1f;
        }
        else if (_inputRght)
        {
            _inputHorizontal = 1f;
        }
        else {
            _inputHorizontal = Input.GetAxisRaw("Horizontal");
        }

        // Because of the way the current asset is built, the values for the X position are reversed.

        if (_inputRollLeft)
        {
            _inputRoll = 1f;
        }
        else if (_inputRollRght)
        {
            _inputRoll = -1f;
        }
        else {
            _inputRoll = 0f;
        }
        // Physics Values for RigidBody application
        xvel = _inputHorizontal * (accelFactor * (_speedPoints + 5));
        yvel = _inputVertical   * (accelFactor * (_speedPoints + 5));
        // Weapons handler
        if (_inputFire) {
            // Launch a projectile and check if the interval has elapsed.
            SpawnProjectile();
        }

        // Quick quit
        if(_inputEsc) {
            UnityEditor.EditorApplication.isPlaying = false; // to quit from the Unity Editor. Remove from this gameloop and implement in the main menu logic.
            Application.Quit();
        }
        _inputRoll *= (accelFactor);

        // Physics handlers. Also incorporates boundary checks.
        if (_transform.position.x >= -PlayableWidthBase && _transform.position.x <= PlayableWidthBase)
        {
            // Within playable bounds. We don't need to take action against the player controls.
        }
        else
        {
            if (_transform.position.x < -PlayableWidthBase && xvel < 0f) {
                xvel = 0f;
            }
            if (_transform.position.x > PlayableWidthBase && xvel > 0f)
            {
                xvel = 0f;
            }
        }
        _transform.Translate(new Vector3(xvel, 0f, 0f));

    }
}

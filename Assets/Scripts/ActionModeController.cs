using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionModeController : MonoBehaviour {

    public enum ActionMode
    {
        NONE,
        RIGIDBODYMODE,
        CHARACTERCONTROLLERMODE,
        PHANTOMMODE
    };

    public GameObject Ambra;

    private ActionMode _myMode = ActionMode.NONE;
    private Rigidbody _rigidBody;
    private CapsuleCollider _capsuleCollider;
    private CharacterController _charactConroller;

    private const float RADIUS = 0.2f;
    private const float HEIGHT = 1.6f;
    private const float SKINWIDTH = 0.05f;
    private const float SLOPELIMIT = 57;
    private Vector3 CENTER = new Vector3(0, 0.8f, 0);
        
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public ActionMode getActionMode()
    {
        return _myMode;
    }

    public void changeMode(ActionMode mode)
    {
        if (mode == _myMode) return;

        _myMode = mode;
        if(mode == ActionMode.RIGIDBODYMODE)
        {
            removeCharacterControllerMode();
            setRigidBodyMode();    
        }
        else if(mode == ActionMode.CHARACTERCONTROLLERMODE)
        {
            removeRigidBodyMode();
            setCharacterControllerMode();
        }
        else if(mode == ActionMode.PHANTOMMODE)
        {
            removePhantomNoNeedComponent();
        }
    }

    void setRigidBodyMode()
    {
        _rigidBody = Ambra.AddComponent<Rigidbody>();
        _rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |RigidbodyConstraints.FreezeRotationZ;
        _rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        _capsuleCollider = Ambra.AddComponent<CapsuleCollider>();
        _capsuleCollider.material = new PhysicMaterial("Wood");
        _capsuleCollider.center = CENTER;
        _capsuleCollider.radius = RADIUS;
        _capsuleCollider.height = HEIGHT;
    }

    void removeRigidBodyMode()
    {
        Destroy(_rigidBody);
        Destroy(_capsuleCollider);
        _rigidBody = null;
        _capsuleCollider = null;
    }

    void setCharacterControllerMode()
    {
        _charactConroller = Ambra.AddComponent<CharacterController>();
        _charactConroller.slopeLimit = SLOPELIMIT;
        _charactConroller.skinWidth = SKINWIDTH;
        _charactConroller.center = CENTER;
        _charactConroller.radius = RADIUS;
        _charactConroller.height = HEIGHT;
    }

    void removeCharacterControllerMode()
    {
        
        Destroy(_capsuleCollider);
        _charactConroller = null;
    }

    void removePhantomNoNeedComponent()
    {
        Destroy(GetComponent<Teleportation>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<CharacterController>());
        Destroy(GetComponent<MoveController>());
        Destroy(GetComponent<CapsuleCollider>());
    }
}

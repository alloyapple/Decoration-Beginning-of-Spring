/// <summary>
/// <para name = "Module">ViewModeSwitch</para>
/// <para name = "Describe">
/// Control the mode of view, change mode among different views.
/// add enum view mode.Add notify change handle.</para>
/// <param name="Author"> YS </param>
/// <param name="Date"> 2014-1-1 </param>
/// </summary>

using UnityEngine;
using System;
using System.Collections.Generic;

using DecorationSystem;
using DecorationSystem.Keyboard;
using DecorationSystem.Framework;

public class ViewModeSwitch : MonoBehaviour
{

//     static ViewModeSwitch _viewModeSwith;

    #region view mode

    /// <summary>
    /// Kinds of view mode.
    /// </summary>
    public enum ViewMode
    {
        Unknown= -1,
        First_Person_Mode = 0,
        Third_Person_Mode,
        Rotation_View_Mode,
        Bird_View_Mode,
        Plane_View_Mode
    }

    /// <summary>
    /// The current mode;
    /// </summary>
    ViewMode _currentMode = ViewMode.Unknown;

    /// <summary>
    /// Gets or sets the current mode.
    /// </summary>
    /// <value>
    /// The type of value is ViewMode.
    /// </value>
    public ViewMode CurrentMode
    {
        get{ return _currentMode;}
        set
        {
            if (_currentMode != value)
            {
                _currentMode = value;                
                ChangeViewMode(_currentMode);
                OnChangeViewMode(_currentMode);
            }
        }
    }
    
    delegate void ViewModeAction();
    
    /// <summary>
    /// Selects the view mode.
    /// </summary>
    /// <param name="newViewMode">The new mode is set.</param>
    void ChangeViewMode(ViewMode newViewMode)
    {
        ViewModeAction newAction = _viewList [(int)newViewMode];
        newAction();
    }    

    /// <summary>
    /// The view list save all view mode functions.
    /// </summary>
    List<ViewModeAction> _viewList = new List<ViewModeAction>();

    /// <summary>
    /// Initializes the view list.
    /// </summary>
    void InitializeViewList()
    {
        _viewList.Add(FirstPersonView);
        _viewList.Add(ThirdPersonView);
        _viewList.Add(RotationView);
        _viewList.Add(BirdView);
        _viewList.Add(PlaneView);
    }

    #endregion

    #region notify

    /// <summary>
    /// Handle View Change.
    /// </summary>
    /// <param name="go">The gameobject which was called.</param>
    public delegate void OnViewChangeHandle(GameObject go);

    /// <summary>
    /// Occurs when [on first person view change].
    /// </summary>
    public static event OnViewChangeHandle OnFirstPersonViewChange;

    /// <summary>
    /// Occurs when [on third person view change].
    /// </summary>
    public static event OnViewChangeHandle OnThirdPersonViewChange;

    /// <summary>
    /// Occurs when [on rotation view change].
    /// </summary>
    public static event OnViewChangeHandle OnRotationViewChange;

    /// <summary>
    /// Occurs when [on bird view change].
    /// </summary>
    public static event OnViewChangeHandle OnBirdViewChange;

    /// <summary>
    /// Occurs when [on plane view change].
    /// </summary>
    public static event OnViewChangeHandle OnPlaneViewChange;

    /// <summary>
    /// The view change handle list
    /// </summary>
    static List<OnViewChangeHandle> ViewChangeHandleList = null;

    /// <summary>
    /// Initializes the ViewChangeHandleList before all function call.
    /// </summary>
    static ViewModeSwitch()
    {
        ViewChangeHandleList = new List<OnViewChangeHandle> {
            OnFirstPersonViewChange,
            OnThirdPersonViewChange,
            OnRotationViewChange,
            OnBirdViewChange,
            OnPlaneViewChange
        };
    }

    /// <summary>
    /// Call when [view mode changes].
    /// </summary>
    /// <param name="newMode">The new mode.</param>
    void OnChangeViewMode(ViewMode newMode)
    {
        OnViewChangeHandle newHandle = ViewChangeHandleList [(int)newMode];
        if (newHandle != null)
            newHandle(gameObject);
        else
            Debug.Log(newMode + " is no function.");
    }

    #endregion

    #region custom component
    /// <summary>
    /// The first camera.
    /// </summary>
    public GameObject firstCamera;
    
    /// <summary>
    /// The person worker.
    /// </summary>
    public GameObject personWorker;
    
    /// <summary>
    /// The eyes camera.
    /// </summary>
    public GameObject eyesCamera;
    
    /// <summary>
    /// The person controller.
    /// </summary>
    public GameObject personController;
    
    /// <summary>
    /// The second camera.
    /// </summary>
    public GameObject secondCamera;
    
    /// <summary>
    /// The third camera.
    /// </summary>
    public GameObject thirdCamera;

    // Some scripts used to control the character.
    private ThirdPersonController _thirdPersonController;
    private ThirdPersonCamera _thirdPersonCameraController;
    private FlyScan _flyScan;
    private CharacterMotor _characterMotor;
    private FPSInputController _fpsInputController;
    private RotationConstraint _rotationConstraint;
    private MyMouseLook _myMouseLook;

    #endregion

    #region system function
    //Initialize the state of components
    void Start()
    {
        InitializeViewList();
        InitializeCustomComponents();
    }

    #endregion

    /// <summary>
    /// Initializes the custom components.
    /// </summary>
    private void InitializeCustomComponents()
    {
        _thirdPersonController = personController.GetComponent<ThirdPersonController>();
        _thirdPersonCameraController = personController.GetComponent<ThirdPersonCamera>();
        _flyScan = personController.GetComponent<FlyScan>();
        _characterMotor = personController.GetComponent<CharacterMotor>();
        _fpsInputController = personController.GetComponent<FPSInputController>();
        _rotationConstraint = eyesCamera.GetComponent<RotationConstraint>();
        _myMouseLook = personController.GetComponent<MyMouseLook>();
    }

    #region five view mode function

    /// <summary>
    /// The first person view.
    /// </summary>
    public void FirstPersonView()
    {
        _thirdPersonController.enabled = false;
        _thirdPersonCameraController.enabled = false;
        _flyScan.enabled = false;
        _characterMotor.enabled = true;
        _fpsInputController.enabled = true;
        _rotationConstraint.enabled = false;
        _myMouseLook.enabled = true;

        secondCamera.SetActive(false);
        thirdCamera.SetActive(false);
        personWorker.SetActive(false);
        firstCamera.SetActive(false);
        eyesCamera.gameObject.SetActive(true);

        GlobalManager.CurrentCamera = eyesCamera.camera;
    }

    /// <summary>
    /// The third person view.
    /// </summary>
    public void ThirdPersonView()
    {
        firstCamera.SetActive(true);
        eyesCamera.gameObject.SetActive(false);
        personWorker.SetActive(true);
        secondCamera.SetActive(false);
        thirdCamera.SetActive(false);

        _thirdPersonController.enabled = true;
        _thirdPersonCameraController.enabled = true;
        _flyScan.enabled = false;
        _characterMotor.enabled = false;
        _fpsInputController.enabled = false;
        _rotationConstraint.enabled = false;
        _myMouseLook.enabled = false;

        GlobalManager.CurrentCamera = firstCamera.camera;
    }

    /// <summary>
    /// The Rotation view.
    /// </summary>
    public void RotationView()
    {
        firstCamera.SetActive(false);
        eyesCamera.gameObject.SetActive(false);
        personWorker.SetActive(false);
        thirdCamera.SetActive(false);
        secondCamera.SetActive(true);

        GlobalManager.CurrentCamera = secondCamera.camera;
    }

    /// <summary>
    /// The bird view.
    /// </summary>
    public void BirdView()
    {
        eyesCamera.gameObject.SetActive(true);
        personWorker.SetActive(false);
        firstCamera.SetActive(false);
        secondCamera.SetActive(false);
        thirdCamera.SetActive(false);

        _thirdPersonController.enabled = false;
        _thirdPersonCameraController.enabled = false;
        _flyScan.enabled = true;
        _characterMotor.enabled = false;
        _fpsInputController.enabled = false;
        _rotationConstraint.enabled = true;
        _myMouseLook.enabled = true;

        GlobalManager.CurrentCamera = eyesCamera.camera;
    }

    /// <summary>
    /// The plane view.
    /// </summary>
    public void PlaneView()
    {
        firstCamera.SetActive(false);
        eyesCamera.gameObject.SetActive(false);
        personWorker.SetActive(false);
        secondCamera.SetActive(false);
        thirdCamera.SetActive(true);

        GlobalManager.CurrentCamera = thirdCamera.camera;
    }

    #endregion
}
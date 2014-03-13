///<summary>
///<para name = "Module">GameController</para>
///<para name = "Describe">The main thread.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2013-12-29</para>
///</summary>
using UnityEngine;

using DecorationSystem;
using DecorationSystem.Undo;
using DecorationSystem.Framework;
using DecorationSystem.View;
using DecorationSystem.Keyboard;
using DecorationSystem.Save;
using DecorationSystem.Gestures;

/// <summary>
/// Main loop.
/// </summary>
public class GameController : MonoBehaviour
{
    #region input controller

    public InputController InputControl { get; set; }

    void InitInput()
    {
        InputControl = InputController.Instance();
    }

    #endregion

    #region history manager

    public CommandManager HistoryManager{ get; set; }

    void InitUndo()
    {
        HistoryManager = CommandManager.Instance();
    }

    #endregion

    #region view controller

    IView _currentView ;

    public IView CurrentView
    {
        get { return _currentView; }
        set
        {
            if (_currentView != null)
            {
                _currentView.Cancel();
            }
            _currentView = value;
            _currentView.Apply();
        }
    }

    void InitView()
    {
        CurrentView = new RotationView();

        RegisterHotKeyForSwitchView(KeyCode.F1, new FirstPersonView());
        RegisterHotKeyForSwitchView(KeyCode.F2, new ThirdPersonView());
        RegisterHotKeyForSwitchView(KeyCode.F3, new RotationView());
        RegisterHotKeyForSwitchView(KeyCode.F4, new BirdView());
        RegisterHotKeyForSwitchView(KeyCode.F5, new PlaneView());
    }

    void RegisterHotKeyForSwitchView(KeyCode key, IView view)
    {
        KeyboardListener.Create()
            .SetKey(key)
            .SetAction(() => CurrentView = view)
            .Register();
    }

    #endregion

    #region save manager

    public SaveGameManager SaveManager { get; set; }

    void InitSave()
    {
        SaveManager = new SaveGameManager();
    }

    public void SaveArchive()
    {
        SaveManager.SaveToFile();
    }

    public void LoadArchive()
    {
        SaveManager.LoadFromFile();
    }

    public void ClearArchive()
    {
        SaveManager.ClearSaves();
    }

    #endregion

    #region game logic

    void Start()
    {
        // Get ready for application start, initialize all components.
        InitInput();
        InitUndo();
        InitView();
        InitSave();

        // Read data from archive.
        //LoadArchive();

        // selection  = null
        GlobalManager.Selection = null;
    }

    void Update()
    {
        // Start to listen input.
        InputControl.Update();
    }

    /// <summary>
    /// Do something when application is quiting.
    /// </summary>
    void OnApplicationQuit()
    {
        // clear selection object and remove sihouette
        if (GlobalManager.Selection != null)
            TapToSelection.RemoveSihouette(GlobalManager.Selection);
        GlobalManager.Selection = null;

        // save user data to file
        //SaveArchive();
    }

    #endregion

}

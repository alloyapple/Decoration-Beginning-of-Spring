//<summary>
///<para name = "Module">RotationViewGUI</para>
///<para name = "Describe">Words</para>
///<para name = "Author">YS</para>
///<para name = "Date">2014-1-1</para>
///</summary>
/// 
using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;
using DecorationSystem.Framework;
using DecorationSystem.Gestures;
using DecorationSystem.Undo;
using DecorationSystem.View;

namespace DecorationSystem.UI
{
    public class RotationViewGUI : MonoBehaviour
    {

    #region toolbar ico ui

        /// <summary>
        /// root of ui
        /// </summary>
        public GameObject Win;

        ///////////////////////////////////////////////
        /// <summary>
        /// measureUI , adjustFurnitureUI, deleteFurniture
        /// are in same group; they are used like toggle.
        /// </summary>
        public GameObject MeasureUI;
        public GameObject MeasureNotifyUI;
        public GameObject AdjustFurnitureUI;
        public GameObject DeleteFurnitureUI;
        ///////////////////////////////////////////////

        ///////////////////////////////////////////////
        /// <summary>
        /// save, deletesave, loadsave are independent.
        /// they are like button so add click event at them. 
        /// </summary>
        public GameObject SaveUI;
        public GameObject DeleteSaveUI;
        public GameObject LoadSaveUI;
        ///////////////////////////////////////////////

        ///////////////////////////////////////////////
        public GameObject UndoUI;
        public GameObject RedoUI;
        public GameObject comUndoUI;
        public GameObject comRedoUI;
        ///////////////////////////////////////////////

        public GameObject PhotoUI;
        public GameObject PhotoOptionInput;

        ///////////////////////////////////////////////
        /// <summary>
        /// they are in same group and like a toggle.
        /// so add toggle at them
        /// </summary>
        public GameObject ThirdpersonViewUI;
        public GameObject RotationViewUI;     
        public GameObject BirdViewUI;
        ///////////////////////////////////////////////

        public GameObject ChangeMaterialUI;
        public GameObject ChangeMaterialWin;

        ///////////////////////////////////////////////

        public GameObject AddFurnitureUI;
        public GameObject AddFurnitureWin;

        ///////////////////////////////////////////////
        public GameObject DigHolesUI;

        ///////////////////////////////////////////////

    #endregion

        void Start()
        {
            /// Add event to measureUI.
            AddToggle(MeasureUI, ToggleMeasureUI);
            TweenDistance.Instance().OnFinishMeasure += NotifyMeasure;

            /// Add event to adjust furnitureUI.
            AddToggle(AdjustFurnitureUI, ToggleAdjustFurnitureUI);

            /// Add delete function.
            GlobalManager.TapSelectionChanged += CheckFurnitureBeforeDelete;
            AddClick(DeleteFurnitureUI, ClickDeleteFurnitureUI);

            /// add event to save
            AddClick(SaveUI, ToggleSaveUI);
            AddClick(DeleteSaveUI, ToggleDeleteSaveUI);
            AddClick(LoadSaveUI, ToggleLoadSaveUI);

            /// add event to undo
            AddClick(UndoUI, ToggleUndoUI);
            AddClick(RedoUI, ToggleRedoUI);
            AddClick(comUndoUI, ToggleUndoUI);
            AddClick(comRedoUI, ToggleRedoUI);
            CommandManager.Instance().CollectionChanged += ToggleUndoAndRedo;

            /// add event to snap
            AddClick(PhotoUI, ToggleSnapUI);
            var path = PhotoOptionInput.GetComponent<UIInput>();
            var views = GlobalManager.GetRoot().GetComponent<ViewModeSwitch>();
           
            /// set rotate view enable false when input is selected
            var rotation = views.secondCamera.GetComponent<RotateCamera>();
            UIEventListener.Get(PhotoOptionInput).onSelect += (go, state) => {
                if (state)
                {
                    rotation.enabled = false;
                } else
                {
                    rotation.enabled = true;
                }
            };

            /// add event to view 
            AddToggle(ThirdpersonViewUI, ToggleRoomWalkUI);
            AddToggle(RotationViewUI, ToggleDecorationUI);
            AddToggle(BirdViewUI, ToggleBirdViewUI);

            /// add event to dig holes. 
            AddToggle(DigHolesUI, ToggleDigHolesUI);

            /// add texture item current texture
            TextureItem.TextureChanged += ClickChangeMaterialUI;
            GlobalManager.TapSelectionChanged += ToggleChangeMaterialUI; 
            /// add furniture
            AddToggle(AddFurnitureUI, ToggleAddFurnitureUI);
            FurnitureItem.FurnitureChanged += ClickAddFurnitureUI;
        }

        /// <summary>
        /// Adds the toggle to toggleUI.
        /// </summary>
        /// <param name="go">The go.</param>
        /// <param name="callback">The callback.</param>
        void AddToggle(GameObject go, EventDelegate.Callback callback)
        {
            UIToggle toggle = go.GetSafeComponent<UIToggle>();
            EventDelegate.Add(toggle.onChange, callback);
        }

        /// <summary>
        /// Adds the click to button ui.
        /// </summary>
        /// <param name="go">The go.</param>
        /// <param name="callback">The callback.</param>
        void AddClick(GameObject go, UIEventListener.VoidDelegate callback)
        {
            UIEventListener.Get(go).onClick += callback;
        }

        /// <summary>
        /// Toggles the measure UI.
        /// </summary>
        public void ToggleMeasureUI()
        {
            GlobalManager.MeasureFunctionSwitch = UIToggle.current.value;
            if (!GlobalManager.MeasureFunctionSwitch) 
                TweenDistance.Instance().DestroyLine();

            if (GlobalManager.MeasureFunctionSwitch)
            {
                GlobalManager.DigHoleFunctionSwitch = false;
                TweenDistance.Instance().CreateLine();
            } else
            {
                TweenDistance.Instance().DestroyLine();
            }
        }

        public void CloseMeasureUI()
        {
            GlobalManager.MeasureFunctionSwitch = false;
        }

        /// <summary>
        /// Notifies the measure.
        /// </summary>
        public void NotifyMeasure()
        {
            //set value
            var label = MeasureNotifyUI.GetSafeComponent<UILabelChange>();
            label.SetValue(TweenDistance.Instance().GetDistance().ToString());

            //set active
            NGUITools.SetActive(MeasureNotifyUI, true);
        }

        /// <summary>
        /// Toggles the adjust furniture UI.
        /// </summary>
        public void ToggleAdjustFurnitureUI()
        {
            var state = UIToggle.current.value;
            var current = GlobalManager.FurnitureOperationSwitch;
            if (current == state)
                return;

            GlobalManager.FurnitureOperationSwitch = state;
            if (state == false && GlobalManager.Selection)
            {
                TapToSelection.RemoveSihouette(GlobalManager.Selection);
            }
        }

        public void ClickDeleteFurnitureUI(GameObject go)
        {
            if (GlobalManager.Selection)
            {
                var value = GlobalManager.Selection;
                GlobalManager.Selection = null;
                value.SetActive(false);
            }
        }

        public void CheckFurnitureBeforeDelete(GameObject go, bool state)
        {
            var ui = DeleteFurnitureUI.GetComponent<UIButtonTwoState>();
            if (go && GlobalManager.FurnitureOperationSwitch && go.CompareTag("Furniture"))
            {
                ui.CurrentState = UIButtonTwoState.State.Enable;
            } else
            {
                ui.CurrentState = UIButtonTwoState.State.Disable;
            }
        }

        /// <summary>
        /// Toggles the save UI.
        /// </summary>
        /// <param name="go">The go.</param>
        public void ToggleSaveUI(GameObject go)
        {
            var game = GetGameController();
            game.SaveArchive();
        }

        /// <summary>
        /// Toggles the delete save UI.
        /// </summary>
        /// <param name="go">The go.</param>
        public void ToggleDeleteSaveUI(GameObject go)
        {
            var game = GetGameController();
            game.ClearArchive();
            GlobalManager.ClearAllLists();
        }

        /// <summary>
        /// Toggles the load save UI.
        /// </summary>
        /// <param name="go">The go.</param>
        public void ToggleLoadSaveUI(GameObject go)
        {
            var game = GetGameController();
            game.LoadArchive();
            GlobalManager.ClearAllLists();
        }

        /// <summary>
        /// Toggles the undo UI.
        /// </summary>
        /// <param name="go">The go.</param>
        public void ToggleUndoUI(GameObject go)
        {
            var cmds = CommandManager.Instance();
            cmds.Undo();
        }

        /// <summary>
        /// Toggles the redo UI.
        /// </summary>
        /// <param name="go">The go.</param>
        public void ToggleRedoUI(GameObject go)
        {
            var cmds = CommandManager.Instance();
            cmds.Redo();
        }

        public void ToggleUndoAndRedo(object sender, EventArgs e)
        {
            var cmds = sender as CommandManager;

            var undo = UndoUI.GetComponent<UIButtonTwoState>();
            var redo = RedoUI.GetComponent<UIButtonTwoState>();
            var comUndo = comUndoUI.GetComponent<UIButtonTwoState>();
            var comRedo = comRedoUI.GetComponent<UIButtonTwoState>();

            if (cmds.CanUndo)
            {
                undo.CurrentState = UIButtonTwoState.State.Enable;
                comUndo.CurrentState = UIButtonTwoState.State.Enable;
            } else
            {
                undo.CurrentState = UIButtonTwoState.State.Disable;
                comUndo.CurrentState = UIButtonTwoState.State.Disable;
            }

            if (cmds.CanRedo)
            {
                redo.CurrentState = UIButtonTwoState.State.Enable;
                comRedo.CurrentState = UIButtonTwoState.State.Enable;
            } else
            {
                redo.CurrentState = UIButtonTwoState.State.Disable;
                comRedo.CurrentState = UIButtonTwoState.State.Disable;
            }

        }

        /// <summary>
        /// Toggles the snap UI.
        /// </summary>
        /// <param name="go">The go.</param>
        public void ToggleSnapUI(GameObject go)
        {
            var snap = new CaptureScreenShot();
            Task task = new Task(snap.GetCapture());
        }

        /// <summary>
        /// Toggles the room walk UI.
        /// </summary>
        public void ToggleRoomWalkUI()
        {
            if (UIToggle.current.value)
            {
                var game = GetGameController();
                game.CurrentView = new ThirdPersonView();
            }
        }

        /// <summary>
        /// Toggles the decoration UI.
        /// </summary>
        public void ToggleDecorationUI()
        {
            if (UIToggle.current.value)
            {
                var game = GetGameController();
                game.CurrentView = new RotationView();
            }
        }

        /// <summary>
        /// Toggles the bird view UI.
        /// </summary>
        public void ToggleBirdViewUI()
        {
            if (UIToggle.current.value)
            {
                var game = GetGameController();
                game.CurrentView = new BirdView();
            }
        }

        /// <summary>
        /// Gets the game controller.
        /// </summary>
        /// <returns></returns>
        GameController GetGameController()
        {
            return GlobalManager.GetRoot().GetSafeComponent<GameController>();
        }

        public void ClickChangeMaterialUI()
        {
            var go = GlobalManager.Selection;
            if (go == null)
                return;
            TweenTexture.Get(GlobalManager.Selection).SetTween(TextureItem.CurrentTexture).Tween();
        }

        public void ClickAddFurnitureUI()
        {
            //var go = GlobalManager.Selection;
            //if (go == null) return;
            //Instantiate(FurnitureItem.CurrentFurniture, go.transform.position, go.transform.rotation);
        }

        public void ToggleAddFurnitureUI()
        {              
            GlobalManager.AddFurnitureFunctionSwitch = UIToggle.current.value;

            //if(GlobalManager.AddFurnitureFunctionSwitch) GlobalManager.Selection = null;
        }

        public void CloseAddFurnitureUI()
        {
            GlobalManager.AddFurnitureFunctionSwitch = false;
        }

        public void ToggleChangeMaterialUI(GameObject go, bool state)
        {
            var ui = ChangeMaterialUI.GetComponent<UIButtonThreeState>();
            if (state && GlobalManager.FurnitureOperationSwitch && go.CompareTag("Furniture"))
            {
                ui.CurrentState = UIButtonThreeState.State.Activate;
            } else
            {
                ui.CurrentState = UIButtonThreeState.State.Disable;
            }
        }

        /// <summary>
        /// Execute the dig hole funtion.
        /// </summary>
        public void ToggleDigHolesUI()
        {
            GlobalManager.DigHoleFunctionSwitch = UIToggle.current.value;
            if (GlobalManager.DigHoleFunctionSwitch)
            {
                GlobalManager.FurnitureOperationSwitch = false;
                GlobalManager.MeasureFunctionSwitch = false;
                GameObject.Find("Room01_1").AddComponent<DigWall2>();
            } else
            {
                Destroy(GameObject.Find("Room01_1").GetComponent<DigWall2>());
                GlobalManager.Selection = null;
            }
        }

    }
}
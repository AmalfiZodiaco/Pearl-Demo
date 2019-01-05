using System.Collections.Generic;
using UnityEngine;
using it.amalfi.Pearl.events;
using System;
using it.amalfi.Pearl.game;

namespace it.amalfi.Pearl.UI
{
    /// <summary>
    /// The abstract class of the UI.This class must be the parent of every UI of the menu or pause.
    /// </summary>
    public abstract class UIMenuManager : LogicalManager
    {
        #region Inspector Fields
        /// <summary>
        /// The first activated element of the UI
        /// </summary>
        [SerializeField]
        protected GameObject firstUIObjectEnable;
        /// <summary>
        /// The UI is the pre-game one or the one during the game
        /// </summary>
        [SerializeField]
        private StateUI stateUI;
        #endregion

        #region Protected Fields
        protected bool isOpenUI = false;
        protected VisibilityPanelComponent visibilityComponent;
        protected SelectionPanelComponent selectionComponent;
        #endregion

        #region Private Fields
        private enum StateUI { Menu, Pause }
        #endregion

        #region Unity CallBacks
        // Use this for initialization
        private void Start()
        {
            visibilityComponent = GetLogicalComponent<VisibilityPanelComponent>();
            selectionComponent = GetLogicalComponent<SelectionPanelComponent>();
            InitOpenMenu();
        }
        #endregion

        #region Init Methods
        private void InitOpenMenu()
        {
            if (stateUI == StateUI.Menu)
            {
                isOpenUI = true;
                DoChangePanel(firstUIObjectEnable);
            }
        }

        protected override void CreateComponents()
        {
            listComponents = new Dictionary<Type, LogicalComponent>
            {
                { typeof(VisibilityPanelComponent), new VisibilityPanelComponent() },
                { typeof(SelectionPanelComponent), new SelectionPanelComponent() },
            };
        }
        #endregion

        #region Interface Methods

        #region UI interface Methods
        /// <summary>
        /// Calls The new Game
        /// </summary>
        public void NewGame()
        {
            gameObject.SetActive(false);
            EventsManager.GetIstance<GameManager>().NewLevel(SceneEnum.Level);
        }

        /// <summary>
        ///  This method must be called whenever you want to change the active button(and therefore often also panel)
        /// </summary>
        public void ChangeButton(GameObject obj)
        {
            if (visibilityComponent.ObeyIsSamePanel(obj))
                DoChangeButton(obj);
            else
                DoChangePanel(obj);
        }

        /// <summary>
        /// Calls The quit game
        /// </summary>
        public void Quit()
        {
            #if UNITY_STANDALONE
                Application.Quit();
            #endif

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        #endregion

        #region Add/Remove Methods in Events
        protected override void SubscribeEvents()
        {
            EventsManager.AddMethod<bool>(EventAction.CallPause, ReceivePause);
            EventsManager.AddMethod(EventAction.GetInputEntryMenu, ReceiveInputOpenCloseMenu);
            EventsManager.AddMethod(EventAction.GetInputReturnUI, ReceiveInputReturn);
        }

        protected override void RemoveEvents()
        {
            EventsManager.RemoveMethod<bool>(EventAction.CallPause, ReceivePause);
            EventsManager.RemoveMethod(EventAction.GetInputEntryMenu, ReceiveInputOpenCloseMenu);
            EventsManager.RemoveMethod(EventAction.GetInputReturnUI, ReceiveInputReturn);
        }
        #endregion

        #region Receive Methods
        private void ReceivePause(bool pause)
        {
            this.isOpenUI = pause;
            if (!pause)
                DoCloseMenu();
            else
                DoChangePanel(firstUIObjectEnable);
        }

        private void ReceiveInputOpenCloseMenu()
        {
            DoOpenCloseMenu();
        }

        private void ReceiveInputReturn()
        {
            DoReturn();
        }
        #endregion

        #region Send Methods
        private void SendCallPause()
        {
            EventsManager.CallEvent(EventAction.CallPause, !this.isOpenUI);
        }
        #endregion

        #endregion

        #region Logical Methods
        protected void DoChangePanel(GameObject obj)
        {
            visibilityComponent.ObeyShow(obj);
            selectionComponent.ObeyChangeSelectNext(obj);
        }

        protected void DoCloseMenu()
        {
            visibilityComponent.ObeyOpenOrCloseAllPanels(false, transform);
            selectionComponent.ObeyReset();
        }

        protected void DoChangeButton(GameObject obj)
        {
            selectionComponent.ObeyChangeSelectNext(obj);
        }

        protected void DoOpenCloseMenu()
        {
            if (stateUI == StateUI.Pause)
                SendCallPause();
        }

        protected void DoReturn()
        {
            if (isOpenUI)
            {
                if (!selectionComponent.IsOpenPage)
                {
                    visibilityComponent.ObeyShow(GetLogicalComponent<SelectionPanelComponent>().LastSelectedButton);
                    selectionComponent.ObeyChangeInPreSelect();
                }
                else if (stateUI == StateUI.Pause)
                    SendCallPause();
            }
        }
        #endregion
    }
}

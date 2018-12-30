using System.Collections.Generic;
using UnityEngine;
using it.amalfi.Pearl.audio;
using it.amalfi.Pearl.events;
using System;
using it.amalfi.Pearl.input;
using it.amalfi.Pearl.game;

namespace it.amalfi.Pearl.UI
{
    public abstract class UIMenuManager : LogicalManager
    {
        #region Inspector Fields
        [SerializeField]
        protected GameObject firstUIObjectEnable;
        [SerializeField]
        private StateUI stateUI;
        #endregion

        #region Protected Fields
        protected bool isOpenUI = false;
        #endregion

        #region Private Fields
        private enum StateUI { Menu, Pause }
        #endregion

        #region Unity CallBacks
        // Use this for initialization
        private void Start()
        {
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
        public void NewGame()
        {
            gameObject.SetActive(false);
            GameManager.NewLevel(SceneEnum.Level);
        }

        public void ChangeButton(GameObject obj)
        {
            if (GetLogicalComponent<VisibilityPanelComponent>().ObeyIsSamePanel(obj))
                DoChangeButton(obj);
            else
                DoChangePanel(obj);
        }

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
        protected override void SubscribEvents()
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
            GetLogicalComponent<VisibilityPanelComponent>().ObeyShow(obj, transform);
            GetLogicalComponent<SelectionPanelComponent>().ObeyChangeSelectNext(obj);
        }

        protected void DoCloseMenu()
        {
            GetLogicalComponent<VisibilityPanelComponent>().ObeyOpenOrCloseAllPanels(false, transform);
            GetLogicalComponent<SelectionPanelComponent>().ObeyReset();
        }

        protected void DoChangeButton(GameObject obj)
        {
            GetLogicalComponent<SelectionPanelComponent>().ObeyChangeSelectNext(obj);
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
                if (!GetLogicalComponent<SelectionPanelComponent>().IsOpenPage)
                {
                    GetLogicalComponent<VisibilityPanelComponent>().ObeyShow(GetLogicalComponent<SelectionPanelComponent>().LastSelectedButton, transform);
                    GetLogicalComponent<SelectionPanelComponent>().ObeyChangeInPreSelect();
                }
                else if (stateUI == StateUI.Pause)
                    SendCallPause();
            }
        }
        #endregion
    }
}

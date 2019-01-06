using UnityEngine;
using Pearl.events;

namespace Pearl.Demo.camera
{
    public class CameraManager : LogicalSimpleManager
    {
        #region Add/Remove Methods in Events
        protected override void SubscribeEvents()
        {
            EventsManager.AddMethod<Transform>(EventAction.CreatePlayer, CreateCamera);
        }

        protected override void RemoveEvents()
        {
            EventsManager.RemoveMethod<Transform>(EventAction.CreatePlayer, CreateCamera);
        }
        #endregion

        #region Private Methods
        private void CreateCamera(Transform transformPlayer)
        {
            transform.position = new Vector3(transformPlayer.position.x, transformPlayer.position.y, transform.position.z);
            transform.parent = transformPlayer;
        }
        #endregion
    }
}

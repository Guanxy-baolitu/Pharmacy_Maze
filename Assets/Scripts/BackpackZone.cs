namespace VRTK.Examples
{
    using UnityEngine;

    public class BackpackZone : VRTK_SnapDropZone
    {
        public GameObject UIcontroller;

        public GameObject RightHandController;

        protected override void OnTriggerStay(Collider collider){
            base.OnTriggerStay(collider);
            //Do sanity check to see if there should be a snappable object
            if (!isSnapped && ValidSnapObject(collider.gameObject, true))
            {
                AddCurrentValidSnapObject(collider.gameObject);
            }

            //if the current colliding object is the valid snappable object then we can snap
            if (IsObjectHovering(collider.gameObject))
            {
                //If it isn't snapped then force the highlighter back on
                if (!isSnapped)
                {
                    ToggleHighlight(collider, true);
                }

                //Attempt to snap the object
                if (RightHandController.GetComponent<VRTK.VRTK_InteractGrab>().GetGrabbedObject() != null)
                {
                    SnapObject(collider);
                }
            }
        }

        protected override void SnapObject(Collider collider){
            base.SnapObject(collider);
            Managers.Backpack.AddItem(collider.gameObject.name);
            collider.transform.parent = UIcontroller.transform;//Controller只是起到中转作用，在用包裹调用该道具的瞬间会抓在手上
            collider.gameObject.SetActive(false);
        }
    }
}
namespace VRTK.Examples
{
    using UnityEngine;

    public class A_ChnPainting : VRTK_InteractableObject
    {

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            transform.position = new Vector3(transform.position.x, transform.position.y+2.5f, transform.position.z);
            //spinSpeed = 360f;
        }

        public override void StopUsing(VRTK_InteractUse usingObject)
        {
            base.StopUsing(usingObject);
            //spinSpeed = 0f;
            transform.position = new Vector3(transform.position.x, transform.position.y-2.5f, transform.position.z);
        }

        protected void Start()
        {
            //rotator = transform.Find("Capsule");
        }

        protected override void Update()
        {
            base.Update();
            //rotator.transform.Rotate(new Vector3(spinSpeed * Time.deltaTime, 0f, 0f));
        }
    }
}
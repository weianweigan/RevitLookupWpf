using GalaSoft.MvvmLight.Messaging;

namespace RevitLookup.PropertySys.BaseProperty.ReferenceType
{
    //public delegate void NaviRequest(object obj);

    public class ObjectProperty<TObject> : PropertyBase<TObject>
        where TObject : class
    {
        public ObjectProperty(string name) : base(name)
        {
            IsClass = true;
        }

        /// <summary>
        /// Use Navi to Snoop Revit Object
        /// </summary>
        /// <param name="result"></param>
        protected void NaviRvtObj(object result)
        {
            if (result == null)
            {
                return;
            }

            //发生消息到 LookupWindowViewModel
            Messenger.Default.Send<RvtObjectMessage>(new RvtObjectMessage(result));
        }
    }
}

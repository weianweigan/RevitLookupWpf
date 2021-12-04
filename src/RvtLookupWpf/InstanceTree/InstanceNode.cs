using Autodesk.Revit.DB;
using GalaSoft.MvvmLight;
using RvtLookupWpf.PropertySys;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvtLookupWpf
{
    public class InstanceNode<TRvtObject> : InstanceNode
    {
        public InstanceNode(TRvtObject rvtObjcet)
        {
            RvtObject = rvtObjcet;
            Name = rvtObjcet?.GetType().Name;
        }

        public TRvtObject RvtObject { get; set; }

        protected override void Snoop()
        {
            if (PropertyList == null && RvtObject != null)
            {
                PropertyList = RvtObject.GetProperties();
            }
        }
    }

    public class ElementIdInstanceNode : InstanceNode<Element>
    {
        public ElementIdInstanceNode(Element rvtObjcet) : base(rvtObjcet)
        {
        }
    }

    public abstract class InstanceNode : ObservableObject
    {
        #region Fields
        private PropertyList _properties;
        private bool _isSelected;
        #endregion

        #region Ctor
        public static InstanceNode Create<TRvtObjcet>(TRvtObjcet obj)
        {
            if (obj == null)
            {
                return null;
            }

            var typeName = obj.GetType().Name;
            var node = default(InstanceNode); 
            switch (typeName)
            {
                case "Document":
                    node = new DocumentInstanceNode(obj as Document);
                    break;                    
                default:
                    node = new InstanceNode<object>(obj);
                    break;
            }
            return node;
        }

        #endregion

        #region Properties
        public string Name { get; protected set; }

        public bool IsSelected
        {
            get => _isSelected; set
            {
                Set(ref _isSelected, value);
                Snoop();
            }
        }

        public bool IsExpanded { get; set; }

        public ObservableCollection<InstanceNode> Children { get; set; }

        public PropertyList PropertyList { get => _properties; set => Set(ref _properties, value); }
        #endregion

        protected abstract void Snoop();

        #region Public Methods

        public IEnumerable<InstanceNode> RecruChild()
        {
            if (Children == null)
            {
                yield break;
            }

            foreach (var child in Children)
            {
                yield return child;

                foreach (var childChild in child.RecruChild())
                {
                    yield return childChild;
                }
            }
        }
        #endregion
    }
}

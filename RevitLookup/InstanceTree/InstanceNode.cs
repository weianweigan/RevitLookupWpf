using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight;
using RevitLookupWpf.Extension;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.PropertySys;
using ArgumentNullException = System.ArgumentNullException;

namespace RevitLookupWpf.InstanceTree
{
    public class InstanceNode<TRvtObject> : InstanceNode
    {   

        public InstanceNode(TRvtObject rvtObject)
        {
            RvtObject = rvtObject;
            Name = rvtObject?.GetType().Name;
        }

        public TRvtObject RvtObject { get; set; }

        public override void Snoop()
        {
            if (PropertyList == null && RvtObject != null)
            {
                
                PropertyList = RvtObject.GetProperties();
            }
        }
    }



    public class IEnumerableInstanceNode : InstanceNode<IEnumerable>
    {
        private readonly IEnumerable _rvtObject;
        public IEnumerableInstanceNode(IEnumerable rvtObject) : base(rvtObject)
        {
            _rvtObject = rvtObject;
            Name = rvtObject?.GetType().Name;
            GetChild();
        }

        private void GetChild()
        {
            if (Children == null)
            {
                Children = new ObservableCollection<InstanceNode>();
            }
            foreach (var item in _rvtObject)
            {
                InstanceNode node;
                switch (item)
                {
                    case Element element:
                        node = new ElementInstanceNode(element,false);
                        Children.Add(node);
                        break;
                    case FamilyType familyType:
                        node = new FamilyTypeInstanceNode(familyType);
                        Children.Add(node);
                        break;
                    case PlanTopology planTopology:
                        node = new PlanTopologyInstanceNode(planTopology);
                        Children.Add(node);
                        break;
                    case PlanCircuit planTopology:
                        node = new PlanCircuitInstanceNode(planTopology);
                        Children.Add(node);
                        break;
                    case WorksetId worksetId:
                        node = new WorksetIdInstanceNode(worksetId).ToWorksetInstanceNode();
                        Children.Add(node);
                        break;
                    case ElementId elementId:
                        node = new ElementIdInstanceNode(elementId).ToElementInstanceNode(false);
                        Children.Add(node);
                        break;
                    case InstanceBinding instanceBinding:
                        node = new InstanceBindingInstanceNode(instanceBinding);
                        Children.Add(node);
                        break;
                    case Parameter parameter:
                        node = new ParameterInstanceNode(parameter);
                        Children.Add(node);
                        break;
                    case FamilyParameter familyParameter:
                        node = new FamilyParameterInstanceNode(familyParameter);
                        Children.Add(node);
                        break;
                    case Connector connector:
                        node = new ConnectorInstanceNode(connector);
                        Children.Add(node);
                        break;
                    case Category category:
                        node = new CategoryInstanceNode(category);
                        Children.Add(node);
                        break;
                    case DefinitionGroup definitionGroup:
                        node = new DefinitionGroupInstanceNode(definitionGroup);
                        Children.Add(node);
                        break;
                    case FailureMessage failureMessage:
                        node = new FailureMessageInstanceNode(failureMessage);
                        Children.Add(node);
                        break;
                    case FailureDefinitionAccessor failureDefinitionAccessor:
                        node = new FailureDefinitionAccessorInstanceNode(failureDefinitionAccessor);
                        Children.Add(node);
                        break;
                    case ExternalDefinition externalDefinition:
                        node = new ExternalDefinitionInstanceNode(externalDefinition);
                        Children.Add(node);
                        break;
                    case City city:
                        node = new CityInstanceNode(city);
                        Children.Add(node);
                        break;
                    case RibbonPanel ribbonPanel:
                        node = new RibbonPanelInstanceNode(ribbonPanel);
                        Children.Add(node);
                        break;
                    case Document document:
                        node = new DocumentInstanceNode(document);
                        Children.Add(node);
                        break;
                    case PaperSize paperSize:
                        node = new PaperSizeInstanceNode(paperSize);
                        Children.Add(node);
                        break;
                    case GeometryObject geometryObject:
                        node = new GeometryObjectInstanceNode(geometryObject);
                        Children.Add(node);
                        break;
                    case XYZ xyz:
                        node = new XYZInstanceNode(xyz);
                        Children.Add(node);
                        break;
                    case EdgeArray edgeArray:
                        node = new IEnumerableInstanceNode(edgeArray);
                        node.IsExpanded = true;
                        Children.Add(node);
                        break;
                    default:
                        node = new InstanceNode<object>(item);
                        Children.Add(node);
                        break;
                }

            }
            if (Children.Any()) Children = Children.OrderBy(x => x.Name).ToObservableCollection();
        }

    }

    public abstract class InstanceNode : ObservableObject
    {
        #region Fields
        private PropertyList _properties;
        private bool _isSelected;
        #endregion

        #region Ctor
        public static InstanceNode Create<TRvtObject>(TRvtObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            InstanceNode node;
            if (obj is IEnumerable enumble)
            {
                node = new IEnumerableInstanceNode(enumble);
            }
            else
            {
                switch (obj)
                {
                    case Element element:
                        node = new ElementInstanceNode(element,true);
                        break;
                    case WorksetId worksetId:
                        node = new WorksetIdInstanceNode(worksetId).ToWorksetInstanceNode();
                        break;
                    case ElementId elementId:
                        node = new ElementIdInstanceNode(elementId).ToElementInstanceNode(true);
                        break;
                    case Document doc:
                        node = new DocumentInstanceNode(doc);
                        break;
                    default:
                        node = new InstanceNode<object>(obj);
                        break;
                }
            }

            if (node == null) throw new ArgumentNullException(nameof(obj));
            return node;
        }

        #endregion

        #region Properties
        public string Name { get; protected set; }

        public bool IsSelected
        {
            get => _isSelected; set
            {
                Snoop();
                Set(ref _isSelected, value);
            }
        }

        public bool IsExpanded { get; set; }

        public ObservableCollection<InstanceNode> Children { get; set; }

        public PropertyList PropertyList { get => _properties; set => Set(ref _properties, value); }
        #endregion

        public abstract void Snoop();

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

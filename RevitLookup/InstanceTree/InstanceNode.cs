using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight;
using RevitLookupWpf.Extension;
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
                PropertyList = RvtObject.GetProperties(this);
            }
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
                    case FamilyType familyType:
                        node = new FamilyTypeInstanceNode(familyType);
                        break;
                    case PlanTopology planTopology:
                        node = new PlanTopologyInstanceNode(planTopology);
                        break;
                    case PlanCircuit planTopology:
                        node = new PlanCircuitInstanceNode(planTopology);
                        break;
                    case InstanceBinding instanceBinding:
                        node = new InstanceBindingInstanceNode(instanceBinding);
                        break;
                    case Parameter parameter:
                        node = new ParameterInstanceNode(parameter);
                        break;
                    case FamilyParameter familyParameter:
                        node = new FamilyParameterInstanceNode(familyParameter);
                        break;
                    case Connector connector:
                        node = new ConnectorInstanceNode(connector);
                        break;
                    case Category category:
                        node = new CategoryInstanceNode(category);
                        break;
                    case DefinitionGroup definitionGroup:
                        node = new DefinitionGroupInstanceNode(definitionGroup);
                        break;
                    case FailureMessage failureMessage:
                        node = new FailureMessageInstanceNode(failureMessage);
                        break;
                    case FailureDefinitionAccessor failureDefinitionAccessor:
                        node = new FailureDefinitionAccessorInstanceNode(failureDefinitionAccessor);
                        break;
                    case ExternalDefinition externalDefinition:
                        node = new ExternalDefinitionInstanceNode(externalDefinition);
                        break;
                    case City city:
                        node = new CityInstanceNode(city);
                        break;
                    case RibbonPanel ribbonPanel:
                        node = new RibbonPanelInstanceNode(ribbonPanel);
                        break;
                    case PaperSize paperSize:
                        node = new PaperSizeInstanceNode(paperSize);
                        break;
                    case GeometryObject geometryObject:
                        node = new GeometryObjectInstanceNode(geometryObject);
                        break;
                    case XYZ xyz:
                        node = new XYZInstanceNode(xyz);
                        break;
                    case BoundingBoxXYZ boundsXYZ:
                        node = new BoundingBoxXYZInstanceNode(boundsXYZ);
                        break;
                    case EdgeArray edgeArray:
                        node = new EdgeArrayInstanceNode(edgeArray);
                        node.IsExpanded = true;
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
        public string Name { get; protected internal set; }
        public int Index { get; set; }
        public bool IsSelected
        {
            get => _isSelected; set
            {
                Snoop();
                Set(ref _isSelected, value);
            }
        }

        public bool IsExpanded { get; set; }

        public virtual bool IsGeometryObject => false;

        public ObservableCollection<InstanceNode> Children { get; set; }

        public InstanceNode Parent { get; }

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

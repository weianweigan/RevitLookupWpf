/*
 * Created By WeiGan 2021.9.10
 * Wrapper of MethodInfo
 */

using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight.Command;
using RevitLookupWpf.Extension;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;
using RevitLookupWpf.View;

namespace RevitLookupWpf.PropertySys.BaseProperty.MethodType
{
    public class MethodProperty : ObjectProperty<MethodInfo>
    {
        #region Fields

        private RelayCommand _selectedCommand;
        private object _methodValue;
        private readonly object _parent;

        #endregion

        #region Ctor
        public MethodProperty(string name, MethodInfo value, object parent) : base(name, value.GetFullName())
        {
            IsMethod = true;
            Value = value;
            _parent = parent;

            var parameters = value.GetParameters();
            SolvedValue = TrySolveValue(name, value, parameters);
            if (!SolvedValue)
                CanExecute = GetCanExexute(parameters);
        }

        #endregion

        #region Properties
        public object MethodValue { get => _methodValue; set => Set(ref _methodValue ,value); }

        /// <summary>
        /// User Click this object to Snoop
        /// </summary>
        //public event NaviRequest OnNaviRequest;

        public bool CanExecute { get; set; }

        /// <summary>
        /// Invoked the method and get a value;
        /// </summary>
        public bool SolvedValue { get; set; }

        public ICommand SelectedCommand
        {
            get
            {
                if (_selectedCommand == null)
                {
                    _selectedCommand = new RelayCommand(Selected);
                }

                return _selectedCommand;
            }
        }

        #endregion

        #region Public Methods

        public string AggregateParameters(ParameterInfo[] parameterInfos)
        {
            if (parameterInfos?.Any() != true)
            {
                return string.Empty;
            }

            return parameterInfos.Length == 1 ?
                $"{parameterInfos[0].ParameterType.Name} {parameterInfos[0].Name}" :
                parameterInfos
                    .Select(p => $"{p.ParameterType.Name} {p.Name}")
                    .Aggregate((p1, p2) => $"{p1},{p2}");
        }

        public bool GetCanExexute(ParameterInfo[] parameterInfos)
        {
            if (parameterInfos == null || parameterInfos.Length == 0)
            {
                return true;
            }

            //检查是否都是基本类型
            foreach (var parameterInfo in parameterInfos)
            {
                if (parameterInfo.ParameterType.IsClass
                    && parameterInfo.ParameterType.FullName != "System.String")
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Solve Value without parameters
        /// </summary>
        public bool NoParameterSolve()
        {
            bool resultFlag = false;
            if (!CanExecute || SolvedValue)
            {
                return resultFlag;
            }

            //直接执行
            try
            {
                var result = Value.Invoke(_parent, null);
                if (result != null)
                {
                    MethodValue = result;
                    var type = result.GetType();
                    if (!type.IsValueTypeOrString())
                        resultFlag = true;
                }
                else
                {
                    MethodValue = "<Null>";
                    resultFlag = false;
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", $"Exception When Call {MethodValue}：{ex.Message}");
            }
            return resultFlag;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Try to solve value if it's return type is value type and it has no parameter
        /// </summary>
        private bool TrySolveValue(string name, MethodInfo value, ParameterInfo[] parameters)
        {
            bool solvedValue;

            ToolTip = $"{value.ReturnType?.Name} {name}({AggregateParameters(parameters)})";
            solvedValue = false;

            if (parameters?.Any() != true && !IsExcept(value))
            {
                if (value.ReturnType.IsValueTypeOrString())
                {
                    //Solve Value
                    MethodValue = value.Invoke(_parent, null)?.ToString() ?? "<Null>";
                    solvedValue = true;
                }
                else
                {
                    var tempValue = value.Invoke(_parent, null);
                    if (tempValue == null)
                    {
                        MethodValue = "<Null>";
                        solvedValue = true;
                    }
                    else
                    {
                        if (tempValue is ElementId id) solvedValue = ResolveElementId(id);
                        else if (tempValue is XYZ xyz) solvedValue = ResolveXYZValue(xyz);
                        else if (tempValue is BoundingBoxXYZ bbxyz) solvedValue = ResolvebbXYZValue(bbxyz);
                        else if (tempValue is BoundingBoxUV bbuv) solvedValue = ResolvebbUVValue(bbuv);
                        else MethodValue = ToolTip;
                    }
                }
            }
            else
            {
                MethodValue = ToolTip;
            }

            return solvedValue;
        }

        /// <summary>
        /// Except some method don't want invoke
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        bool IsExcept(MethodInfo methodInfo)
        {
            if (methodInfo.Name.Equals("Save", StringComparison.OrdinalIgnoreCase)) return true;
            if (methodInfo.Name.Equals("Regenerate", StringComparison.OrdinalIgnoreCase)) return true;
            if (methodInfo.Name.Equals("ConvertTemporaryHideIsolateToPermanent", StringComparison.OrdinalIgnoreCase))
                return true;
            if (methodInfo.Name.Equals("SaveAs", StringComparison.OrdinalIgnoreCase)) return true;
            if (methodInfo.Name.Equals("UpdateAllOpenViews", StringComparison.OrdinalIgnoreCase)) return true;
            if (methodInfo.Name.Equals("DeactivateAllModes", StringComparison.OrdinalIgnoreCase)) return true;
            if (methodInfo.Name.Equals("RemoveCustomization", StringComparison.OrdinalIgnoreCase)) return true;
            if (methodInfo.Name.Equals("RefreshActiveView", StringComparison.OrdinalIgnoreCase)) return true;
            if (methodInfo.Name.Equals("Maximize3DExtents", StringComparison.OrdinalIgnoreCase)) return true;
            if (methodInfo.Name.Equals("OpenSharedParameterFile", StringComparison.OrdinalIgnoreCase)) return true;
            if (methodInfo.Name.Equals("PurgeReleasedAPIObjects", StringComparison.OrdinalIgnoreCase)) return true;
            if (methodInfo.Name.Equals("UpdateRenderAppearanceLibrary", StringComparison.OrdinalIgnoreCase))
                return true;
            if (methodInfo.Name.Equals("SaveCloudModel", StringComparison.OrdinalIgnoreCase)) return true;
            if (methodInfo.Name.Equals("Print", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("SubmitPrint", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("Set", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("Revert", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("ToggleToIsometric", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("ToggleToPerspective", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("EnableRevealHiddenMode", StringComparison.InvariantCultureIgnoreCase))
                return true;
            if (methodInfo.Name.Equals("HideActiveWorkPlane", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("ShowActiveWorkPlane", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("PickIMXFileToImport", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("Unlock", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("Clear", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("Sort", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("Reverse", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("TrimExcess", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("ZoomSheetSize", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("ZoomToFit", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("Launch", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("ResetLinks", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("ResetManualAdjustment", StringComparison.InvariantCultureIgnoreCase))
                return true;
            if (methodInfo.Name.Equals("GetAnalyticalModel", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("Activate", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("FitToModel", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("ResetPartShape", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("Reset", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("MakeUnbound", StringComparison.InvariantCultureIgnoreCase)) return true;
            if (methodInfo.Name.Equals("DissociateFromGlobalParameter ", StringComparison.InvariantCultureIgnoreCase))
                return true;
            return false;
        }

        private void Selected()
        {
            if (!CanExecute || SolvedValue)
            {
                return;
            }

            var parameters = Value.GetParameters();

            if (parameters == null || parameters.Length == 0)
            {
                //直接执行
                try
                {
                    var result = Value.Invoke(_parent, null);
                    if (result != null)
                    {
                        VisitResult(result);
                    }
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Error", $"Exception When Call {MethodValue}：{ex.Message}");
                }
            }
        }

        bool ResolveElementId(ElementId id)
        {
            var element = SnoopingContext.Instance.CommandData.Application.ActiveUIDocument.Document
                .GetElement(id);
            if (element == null)
            {
                MethodValue = "<Null>";
                return true;
            }
            MethodValue = element;
            return false;
        }

        bool ResolveXYZValue(XYZ xyz)
        {
            MethodValue = xyz.ToString();
            return false;
        }

        bool ResolvebbXYZValue(BoundingBoxXYZ bbXyz)
        {
            XYZ minPoint = bbXyz.Min;
            XYZ maxPoint = bbXyz.Max;
            XYZ centerPoint = (minPoint + maxPoint) * 0.5;
            MethodValue = $"Min:{minPoint}\nMax:{maxPoint}\nCenter:{centerPoint}";
            return false;
        }
        bool ResolvebbUVValue(BoundingBoxUV bbuv)
        {
            UV minPoint = bbuv.Min;
            UV maxPoint = bbuv.Max;
            UV centerPoint = (minPoint + maxPoint) * 0.5;
            MethodValue = $"Min:{minPoint}\nMax:{maxPoint}\nCenter:{centerPoint}";
            return false;
        }

        private void VisitResult(object result)
        {
            //对值类型和引用类型分别处理
            var type = result.GetType();
            if (!type.IsValueTypeOrString())
            {
                if (SnoopOption.WindowOrNavi)
                {
                    var window = new LookupWindow();
                    window.SetRvtInstance<object>(result);
                    window.Show();
                }
                else
                {
                    NaviRvtObj(result);
                }
            }
            else
            {
                MethodValue = result;
                //TaskDialog.Show("Success", result.ToString());
            }
        }

        #endregion
    }
}
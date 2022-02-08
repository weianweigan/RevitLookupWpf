/*
 * Created By WeiGan 2021.9.10
 * Wrapper of MethodInfo
 */

using System.Reflection;
using System.Windows.Input;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight.CommandWpf;
using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;
using RevitLookupWpf.View;

namespace RevitLookupWpf.PropertySys.BaseProperty.MethodType
{
    public class MethodProperty : ObjectProperty<MethodInfo>
    {
        #region Fields
        private RelayCommand _selectedCommand;
        private readonly object _parent;
        #endregion

        #region Ctor
        public MethodProperty(string name, MethodInfo value,object parent) : base(name)
        {
            IsMethod = true;
            Value = value;
            _parent = parent;

            var parameters = value.GetParameters();
            MethodValue = $"{value.ReturnType?.Name} {name}({AggregateParameters(parameters)})";

            CanExecute = GetCanExexute(parameters);
        }
        #endregion

        #region Properties
        public string MethodValue { get; set; }

        /// <summary>
        /// User Click this object to Snoop
        /// </summary>
        //public event NaviRequest OnNaviRequest;

        public bool CanExecute { get; set; }

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
                if(parameterInfo.ParameterType.IsClass
                    && parameterInfo.ParameterType.FullName != "System.String")
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Private Methods
        private void Selected()
        {
            if (!CanExecute)
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

        private void VisitResult(object result)
        {
            //对值类型和引用类型分别处理
            var type = result.GetType();
            if (type.IsClass && type.FullName != "System.String")
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
                TaskDialog.Show("Success", result.ToString());
            }
        }
        #endregion
    }
}

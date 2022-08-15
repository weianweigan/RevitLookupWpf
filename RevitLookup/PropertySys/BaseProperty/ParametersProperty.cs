using Autodesk.Revit.UI;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.ParameterSys;
using RevitLookupWpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitLookupWpf.PropertySys.BaseProperty
{
    /// <summary>
    /// A property which has getter index or set parameter and A method which has some value-type parameters
    /// </summary>
    public abstract class ParametersProperty : PropertyBase<object>
    {
        protected readonly object _parent;
        private RelayCommand _selectedCommand;
        private bool _solvedValue;
        private RelayCommand _applyCommand;
        private string _valueType;
        private bool _hasError;

        public ParametersProperty(string name,string fullName ,object parent) : base(name,fullName)
        {
            _parent = parent;
        }

        public string ValueType { get => _valueType; set => SetProperty(ref _valueType, value); }

        public bool IsReturnValueType { get; set; }

        public bool ReturnVoid { get; set; }

        public string ReturnTypeName { get; set; }

        public bool SolvedValue { get => _solvedValue; set => SetProperty(ref _solvedValue, value); }

        public bool HasError { get => _hasError; set => SetProperty(ref _hasError ,value); }

        public List<ParameterBase> Parameters { get; protected set; }

        public RelayCommand ApplyCommand { get => _applyCommand ??= new RelayCommand(ApplyClick); }

        public ICommand SelectedCommand => _selectedCommand ??= new RelayCommand(Selected);

        protected abstract object Invoke();

        protected void DetermainReturnInfo(Type returnType)
        {
            if (returnType == null)
            {
                ReturnVoid = true;
                return;
            }

            IsReturnValueType = returnType.IsValueTypeOrString();

            ReturnTypeName = returnType.Name;
        }

        private void ApplyClick()
        {
            //Check Parameters
            if (Parameters?.Any() == false)
            {
                TaskDialog.Show("Error", "No Parameter,Cannot Invoke");
                return;
            }

            foreach (var parameter in Parameters)
            {
                if (!parameter.IsLegal())
                {
                    return;
                }

                if (parameter.GetValue() == null)
                {
                    var res = TaskDialog.Show("Warning", $"Parameter{parameter.Name} is null,Continue?", TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No);

                    if (res == TaskDialogResult.No)
                    {
                        return;
                    }
                }
            }

            try
            {
                Value = Invoke();
                if (Value == null)
                {
                    ValueType = ReturnVoid ? "<Void>" : "<Null>";
                    SolvedValue = true;
                }
                else
                {
                    if (IsReturnValueType)
                    {
                        ValueType = Value.ToString();
                        SolvedValue = true;
                    }
                    else
                    {
                        ValueType = ReturnTypeName;
                        SolvedValue = true;
                    }
                }
                HasError = false;
            }
            catch (Exception ex)
            {
                ValueType = ex.Message;
                ToolTip = ex.ToString();
                HasError = true;
            }

        }

        private void Selected()
        {
            if (Value == null || IsReturnValueType)
            {
                return;
            }

            if (SnoopOption.WindowOrNavi)
            {
                var lookupWindow = new LookupWindow();
                lookupWindow.SetRvtInstance(Value);
                lookupWindow.ShowDialog();
            }
            else
            {
                //使用面包屑导航
                //发生消息到 LookupWindowViewModel
                StrongReferenceMessenger.Default.Send<RvtObjectMessage>(new RvtObjectMessage(Value));
            }
        }
    }
}

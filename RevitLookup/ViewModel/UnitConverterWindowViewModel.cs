using System.Text;
using Autodesk.Revit.DB;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;

namespace RevitLookupWpf.ViewModel
{
    public class UnitConverterWindowViewModel : ViewModelBase
    {
        private string _targetData;
        private string _converterData;
        private Exception _ex;
        private RelayCommand _closeCommand;

        public UnitConverterWindowViewModel(string converterData)
        {
            ConverterData = converterData;

#if R19 || R20

#else
            UnitTypes = GetUnitTypesFromUnitTypeId();
#endif
        }

        public string ConverterData
        {
            get => _converterData; set
            {
                _converterData = value;
                Update();
            }
        }

#if R19 || R20
        private DisplayUnitType _selectedUnitType = DisplayUnitType.DUT_DECIMAL_FEET;
        private DisplayUnitType _selectedTargetUnitType =  DisplayUnitType.DUT_METERS;

        public List<DisplayUnitType> UnitTypes { get; set; } =
            Enum.GetValues(typeof(DisplayUnitType))
            .OfType<DisplayUnitType>()
            .ToList();

        public DisplayUnitType SelectedUnitType
        {
            get => _selectedUnitType; set
            {
                _selectedUnitType = value;
                Update();
            }
        }

        public DisplayUnitType SelectedTargetUnitType
        {
            get => _selectedTargetUnitType; set
            {
                _selectedTargetUnitType = value;
                Update();
            }
        }
#else
        private string _selectedUnitType = nameof(UnitTypeId.Feet);
        private string _selectedTargetUnitType = nameof(UnitTypeId.Meters);

        public List<string> UnitTypes { get; set; }

        public string SelectedUnitType
        {
            get => _selectedUnitType; set
            {
                _selectedUnitType = value;
                Update();
            }
        }

        public string SelectedTargetUnitType
        {
            get => _selectedTargetUnitType; set
            {
                _selectedTargetUnitType = value;
                Update();
            }
        }

        private List<string> GetUnitTypesFromUnitTypeId()
        {
            var type = typeof(UnitTypeId);
            var properties = type.GetProperties();

            var staticProperties = properties
                .Select(p => p.Name)
                .ToList();

            return staticProperties;
        }
#endif

        public string TargetData { get => _targetData; set => Set(ref _targetData, value); }

        public RelayCommand CloseCommand { get => _closeCommand ?? (_closeCommand = new RelayCommand(CloseAction)); }

        public Action CloseAction { get; set; }

        /// <summary>
        /// Unit Conveter Exception
        /// </summary>
        public Exception Ex { get => _ex; set => Set(ref _ex ,value); }

        private void Update()
        {
            if (string.IsNullOrEmpty(_converterData))
            {
                TargetData = string.Empty;
                return;
            }

            if (SelectedUnitType == SelectedTargetUnitType)
            {
                TargetData = ConverterData;
                return;
            }

            var regex = new Regex("([1-9]\\d*\\.?\\d*)|(0\\.\\d*[1-9])");

            var matchs = regex.Matches(ConverterData);
            if (matchs?.Count <= 0)
            {
                TargetData = string.Empty;
            }

            try
            {
                var strBuilder = new StringBuilder();
                int endIndex = 0;
                foreach (Match match in matchs)
                {
                    strBuilder.Append(ConverterData.Substring(endIndex, match.Index-endIndex));
                    endIndex = match.Index + match.Length;

                    var value = double.Parse(match.Value);
#if R19 || R20
                var result = UnitUtils.Convert(value, SelectedUnitType, SelectedTargetUnitType);
#else
                    var type = typeof(UnitTypeId);
                    var properties = type.GetProperties();

                    var realSelectedUnitType = properties
                        .FirstOrDefault(p => p.Name == SelectedUnitType)
                        .GetValue(null) as ForgeTypeId;
                    var realSelectedTargetUnitType = properties
                        .FirstOrDefault(p => p.Name == SelectedTargetUnitType)
                        .GetValue(null) as ForgeTypeId;

                    var result = UnitUtils.Convert(value, realSelectedUnitType, realSelectedTargetUnitType);
#endif
                    strBuilder.Append(result);
                }
                strBuilder.Append(ConverterData.Substring(endIndex, ConverterData.Length - endIndex));

                TargetData = strBuilder.ToString();
                Ex = null;
            }
            catch (Exception ex)
            {
                Ex = ex;
            }
        }
    }
}

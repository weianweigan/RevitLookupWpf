using System.Text;
using Autodesk.Revit.DB;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Text.RegularExpressions;
using System.Reflection;

namespace RevitLookupWpf.ViewModel
{
    public class UnitConverterWindowViewModel : ViewModelBase
    {
        private string _targetData;
        private string _converterData;
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
        private DisplayUnitType _selectedUnitType;
        private DisplayUnitType _selectedTargetUnitType;

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
        private ForgeTypeId _selectedUnitType = UnitTypeId.Inches;
        private ForgeTypeId _selectedTargetUnitType = UnitTypeId.Meters;

        public List<ForgeTypeId> UnitTypes { get; set; }

        public ForgeTypeId SelectedUnitType
        {
            get => _selectedUnitType; set
            {
                _selectedUnitType = value;
                Update();
            }
        }

        public ForgeTypeId SelectedTargetUnitType
        {
            get => _selectedTargetUnitType; set
            {
                _selectedTargetUnitType = value;
                Update();
            }
        }

        private List<ForgeTypeId> GetUnitTypesFromUnitTypeId()
        {
            var type = typeof(UnitTypeId);
            var properties = type.GetProperties();

            var staticProperties = properties
                .Select(p => p.GetValue(null))
                .Cast<ForgeTypeId>()
                .Where(p => p != null)
                .ToList();

            return staticProperties;
        }
#endif

        public string TargetData { get => _targetData; set => Set(ref _targetData, value); }

        public RelayCommand CloseCommand { get => _closeCommand ?? (_closeCommand = new RelayCommand(CloseAction)); }

        public Action CloseAction { get; set; }

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

            var matchs = Regex.Matches(ConverterData, "^(-?d+)(.d+)?$");
            if (matchs?.Count <= 0)
            {
                TargetData = string.Empty;
            }

            var strBuilder = new StringBuilder();
            int endIndex = 0;
            foreach (Match match in matchs)
            {
                strBuilder.Append(ConverterData.Substring(endIndex, match.Index));
                endIndex = match.Index + match.Length;

                var value = double.Parse(match.Value);
                var result = UnitUtils.Convert(value, SelectedUnitType, SelectedTargetUnitType);
                strBuilder.Append(result);
            }
            strBuilder.Append(ConverterData.Substring(endIndex, ConverterData.Length - endIndex));

            TargetData = strBuilder.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.View;

namespace RevitLookupWpf.ViewModel
{
    public enum DocType
    {
        DocCurrent,
        DocLinked
    }
    public class SnoopSearchViewModel : ViewModelBase
    {
        public SearchWindow SearchWindow { get; set; }
        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                Set(ref _value, value);
                RaisePropertyChanged(nameof(Value));
            }
        }

        public Array Docs => Enum.GetValues(typeof(DocType));

        public DocType DocSelected { get; set; }

        private RelayCommand _snoopSearchCommand;
        public RelayCommand SnoopSearchCommand => _snoopSearchCommand ?? new RelayCommand(SnoopSearchClick);
        public ExternalCommandData Data { get; set; }
        public SnoopSearchViewModel(ExternalCommandData data)
        {
            Data = data;
        }
        void SnoopSearchClick()
        {
            try
            {
                if (string.IsNullOrEmpty(Value))
                {
                    MessageBox.Show("Please Input ElementId or Guid", Resource.AppName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                Element element = null;
                switch (DocSelected)
                {
                    case DocType.DocCurrent:
                        Document doc = Data.Application.ActiveUIDocument.Document;
                        element = TryGetElement(doc);
                        break;
                    case DocType.DocLinked:
                        foreach (Document document in Data.Application.Application.Documents)
                        {
                            element = TryGetElement(document);
                            if(element!=null) break;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (element == null)
                {
                    ShowWarning();
                    return;
                }
                SearchWindow.Close();
                var lookupWindow = new LookupWindow(Data);
                lookupWindow.SetRvtInstance(element);
                lookupWindow.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        Element TryGetElement(Document doc)
        {
            Element element = doc.GetElement(Value);
            if (element == null)
            {
                bool flag = int.TryParse(Value, out int result);
                if (flag) element = doc.GetElement(new ElementId(result));
                if (element == null)
                {
                    return null;
                }
            }
            return element;
        }

        void ShowWarning()
        {
            MessageBox.Show("ElementId or Guid not exist in project", Resource.AppName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}

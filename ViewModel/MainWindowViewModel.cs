using CodeManager.Commands;
using EditableTextBox_Demo.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/*
 * Code I used to test the control:
*       tb.replace("</Code>", "</Mike>");
        tb.Insert(200, "<This is a test>");
        tb.Insert("<Code ", "<Booger>");
        List<FindCollection> list = tb.FindAllRegex(@"%\w*%*");

        List<int> ints = tb.FindAll("ode");

        // Using an IEnumerable to find all "ode" strings
        int isit = 0;
        var id = tb.FindNext("ode");
        foreach (var num in id)
            isit = num;

        // Replaces all occurances of string returned from Regex expression
        List<FindCollection> list = tb.FindAllRegex(@"%\w*%*");
        foreach (FindCollection fc in list)
            tb.Replace(fc, "...Testing...".Substring(0, fc.Length));

        // Find and select example
        int idx = tb.Find("</Code>");
        if (idx >= 0)
            tb.SelectText(idx, 7);
 */

namespace EditableTextBox_Demo.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string QueryString { get; set; }
        public Dictionary<string, string> ReplaceList { get; set; }
        private string ErrorText = "Please enter a valid query in the QueryTextBox";

        private string _snippetText;
        public string SnippetText
        {
            get { return _snippetText; }
            set => SetProperty(ref _snippetText, value);
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            set => SetProperty(ref _result, value);
        }

        public RelayCommand ToolBarClickCommand { get; set; }

        public MainWindowViewModel(Window win) : base(win)
        {
            SnippetText = Model.DataModel.GetData();

            ToolBarClickCommand = new RelayCommand(
                (p) => ExecuteToolBarClickCommand(p),
                (p) => { return true; }
            );
        }

        private void ExecuteToolBarClickCommand(object param)
        {
            EditableTextBox tb = (ParentWindow as MainWindow).tbSnippet;
            List<int> ints = null;
            StringBuilder sb = new StringBuilder();
            List<FindCollection> list = null;

            sb.Clear();
            switch (param.ToString())
            {
                case "find":
                    Find(tb);
                    break;
                case "findall":
                    FindAll(tb, ints, sb);
                    break;
                case "demo_findall":
                    Dempo_FindAll(tb, sb);
                    break;
                case "demo_findandreplace":
                    Demo_FindAndReplace(tb, sb);
                    break;
                case "regex":
                    FindAllRegex(tb, sb);
                    break;
            }
        }

        private void Find(EditableTextBox tb)
        {
            if (string.IsNullOrEmpty(QueryString))
                MessageBox.Show(ErrorText, "Invalid entry", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                int idx = tb.Find(QueryString, true);
                if (idx >= 0)
                {
                    Result = string.Format("Index: {0}", idx);
                    tb.SelectText(idx, QueryString.Length);
                } 
            }
        }

        private void FindAll(EditableTextBox tb, List<int> ints, StringBuilder sb)
        {
            if (string.IsNullOrEmpty(QueryString))
                MessageBox.Show(ErrorText, "Invalid entry", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                ints = tb.FindAll(QueryString);
                if (ints.Count > 0)
                {
                    foreach (int i in ints)
                        sb.Append("Index: " + i + Environment.NewLine);

                    Result = sb.ToString();
                }
            }
        }

        private void FindAllRegex(EditableTextBox tb, StringBuilder sb)
        {
            List<FindCollection> list;
            string str;
            string rstr;

            if (string.IsNullOrEmpty(QueryString))
            {
                MessageBox.Show(ErrorText, "Invalid entry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                list = tb.FindAllRegex(QueryString);
                foreach (FindCollection fc in list)
                {
                    str = fc.FoundStr.Substring(1, fc.FoundStr.Length - 2);
                    sb.Append("Index: " + fc.Index);
                    sb.Append(", String: " + fc.FoundStr);
                    sb.Append(", Length: " + fc.Length);
                    sb.Append(Environment.NewLine);
                }
                Result = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Regex: " + ex.Message, "Invalid entry", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void Dempo_FindAll(EditableTextBox tb, StringBuilder sb)
        {
            List<FindCollection> list;
            ReplaceList = Model.DataModel.GetDataReplacements();
            list = tb.FindAllRegex(@"%\w*%*");
            foreach (FindCollection fc in list)
            {
                sb.Append("Index: " + fc.Index);
                sb.Append(", String: " + fc.FoundStr);
                sb.Append(", Length: " + fc.Length);
                sb.Append(Environment.NewLine);
            }
            Result = sb.ToString();
        }

        private void Demo_FindAndReplace(EditableTextBox tb, StringBuilder sb)
        {
            List<FindCollection> list;
            string str;
            string rstr;

            ReplaceList = Model.DataModel.GetDataReplacements();
            list = tb.FindAllRegex(@"%\w*%*");
            foreach (FindCollection fc in list)
            {
                str = fc.FoundStr.Substring(1, fc.FoundStr.Length - 2);
                rstr = ReplaceList[str];
                tb.Replace(fc.FoundStr, rstr);
                sb.Append("Index: " + fc.Index);
                sb.Append(", String: " + fc.FoundStr);
                sb.Append(", Length: " + fc.Length);
                sb.Append(Environment.NewLine);
                sb.Append("Replacement String: " + rstr);
                sb.Append(Environment.NewLine);
            }
            Result = sb.ToString();
        }
    }
}

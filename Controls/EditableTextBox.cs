using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace EditableTextBox_Demo.Controls
{
    public class EditableTextBox : TextBox
    {
        public EditableTextBox()
        {

        }

        public void SelectText(int startIndex, int length)
        {
            this.Focus();
            this.SelectionStart = startIndex;
            this.SelectionLength = length;
        }

        public int Find(string str, bool ignoreCase = false)
        {
            if (ignoreCase)
                return this.Text.IndexOf(str, StringComparison.OrdinalIgnoreCase);

            return this.Text.IndexOf(str);
        }

        public int Find(char chr, int index)
        {
            return this.Text.IndexOf(chr, index);
        }

        public List<int> FindAll(string str)
        {
            int idx = 0;
            List<int> strList = new List<int>();

            while (idx >=0)
            {
                idx = this.Text.IndexOf(str, ++idx);
                if (idx >=0)
                    strList.Add(idx);
            }

            return strList;
        }

        public IEnumerable<int> FindNext(string str)
        {
            int idx = 0;

            while (idx >= 0)
            {
                idx = this.Text.IndexOf(str, ++idx);
                if (idx >= 0)
                    yield return idx;
            }
        }

        public List<FindCollection> FindAllRegex(string regexExpression)
        {
            List<FindCollection> strList = new List<FindCollection>();

            Regex regex = new Regex(regexExpression);
            MatchCollection match = regex.Matches(this.Text);

            if (match.Count > 0)
            {
                for (int i = 0; i < match.Count; i++)
                {
                    strList.Add(new FindCollection(match[i].Index, 
                        match[i].Captures[0].Length,
                        match[i].Captures[0].Value));
                }
            }

            return strList;
        }

        // Replace
        public void Replace(string src, string replaceStr)
        {
            int index = Find(src);
            if (index >= 0)
            {
                this.Text = this.Text.Replace(src, replaceStr);
            }
        }

        public void Replace(FindCollection fc, string replaceStr)
        {
            if (fc.Index >= 0)
            {
                RemoveText(fc.Index, fc.Length);
                Insert(fc.Index, replaceStr);
            }
        }

        // Insert
        public void Insert(int pos, string str)
        {
            this.Text = this.Text.Insert(pos, str);
        }

        public void Insert(string src, string addStr)
        {
            int idx = Find(src);
            if (idx >= 0)
            {
                this.Text = this.Text.Insert(idx + src.Length, addStr);
            }
        }

        public void RemoveText(int index, int len)
        {
            this.Text = this.Text.Remove(index, len);
        }
    }

    public class FindCollection
    {
        public int Index { get; set; }
        public int Length { get; set; }
        public string FoundStr { get; set; }

        public FindCollection(int idx,int len, string fnd)
        {
            Index = idx;
            FoundStr = fnd;
            Length = len;
        }
    }
}

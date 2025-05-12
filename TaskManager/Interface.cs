using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace TaskManager;

static class Interface
{
    public static Dictionary<Record, Button> CreateRecords(List <Record> recordsDb)
    {
        Dictionary<Record, Button> recordsDictionary = new ();
        for (int i = 0; i < recordsDb.Count; ++i)
        {
            Button record = new Button();
            record.Content = recordsDb[i].Name;
            record.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
            record.Margin = new System.Windows.Thickness(1);
            recordsDictionary.Add(recordsDb[i], record);
        }
        return recordsDictionary;
    }

    public static void ShowRecordInfo(Button button, Popup popup, TextBlock block, Dictionary<Record, Button> records)
    {
        popup.IsOpen = true;
        var listRecords = records.ToDictionary(pair => pair.Key.Name.ToString(), pair => pair.Key);
        var info = listRecords[button.Content.ToString()];
        block.Text = $"{info.Text}\nСрок выполнения: до {info.Date.ToShortDateString()}\nПриоритет задачи: {info.Priority}";
        block.Foreground = info.Date < DateTime.Now ? Brushes.Red : Brushes.Black;
    }

    public static Button AddTask(StackPanel panel, string name, string text, string priority, DateTime date)
    {
        Button record = new Button();
        record.Content = name;
        record.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
        record.Margin = new System.Windows.Thickness(1);
        panel.Children.Add(record);
        return record;
    }

    public static Dictionary<Record, Button> SortByPriority(Dictionary<Record, Button> records)
    {
        var sortedDictionary = records.OrderBy(pair => pair.Key.Priority).ToDictionary();
        return sortedDictionary;
    }
}

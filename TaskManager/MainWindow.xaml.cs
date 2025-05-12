using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TaskManager;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Dictionary<Record, Button> records;

    private ObservableCollection<Button> buttons;

    public MainWindow()
    {
        InitializeComponent();
        var listRecords = DataBase.GetAllTasksDb();
        this.records = Interface.CreateRecords(listRecords);
        this.buttons = new ObservableCollection<Button>(records.Values);
        PlaceToPanel();
        AddEvent();
    }

    private void PlaceToPanel()
    {
        MainPanel.Children.Clear();
        foreach (var button in buttons)
        {
            MainPanel.Children.Add(button);
        }
    }

    private void AddEvent()
    {
        foreach (var button in buttons)
        {
            button.Click += GetRecordInfo;
        }
    }

    private void GetRecordInfo(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            Interface.ShowRecordInfo(button, InfoPopup, TextPopup, records);
        }
    }

    private void ChangeForm(Visibility mainPanel, Visibility tasksection, Visibility add)
    {
        MainPanel.Visibility = mainPanel;
        CreateTaskSection.Visibility = tasksection;
        Add.Visibility = add;
    }

    private void OpenAddTask(object sender, RoutedEventArgs e)
    {
        ChangeForm(Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed);
    }

    private void CancelAddTask(object sender, RoutedEventArgs e)
    {
        ChangeForm(Visibility.Visible, Visibility.Collapsed, Visibility.Visible);
        ClearForm();
    }

    private void CreateTask(object sender, RoutedEventArgs e)
    {
        var priority = string.Empty;
        if (HighPriority.IsChecked == true)
        {
            priority = "1.high";
        }
        if (MediumPriority.IsChecked == true)
        {
            priority = "2.medium";
        }
        if (LowPriority.IsChecked == true)
        {
            priority = "3.low";
        }
        DataBase.AddTaskDb(TaskName.Text, TaskContent.Text, priority, DateTime.Now);
        var newButton = Interface.AddTask(MainPanel, TaskName.Text, TaskContent.Text, priority, DateTime.Now);
        newButton.Click += GetRecordInfo;
        records.Add(new Record(TaskName.Text, TaskContent.Text, priority, DateTime.Now), newButton);
        buttons.Add(newButton);
        ClearForm();
    }

    private void ClearForm()
    {
        TaskContent.Text = string.Empty;
        TaskName.Text = string.Empty;
        TaskDate.Text = string.Empty;
        LowPriority.IsChecked = false;
        MediumPriority.IsChecked = false;
        HighPriority.IsChecked = false;
    }

    private void SortByPriority(object sender, RoutedEventArgs e)
    {
        this.records = Interface.SortByPriority(this.records);
        this.buttons = new ObservableCollection<Button>(records.Values);
        PlaceToPanel();
    }
}

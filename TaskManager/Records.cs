using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager;

class Record
{
    public string Name { get; private set; }

    public string Text { get; private set; }

    public string Priority { get; private set; }

    public DateTime Date { get; private set; }

    public Record(string name, string text, string priority, DateTime date)
    {
        this.Name = name;
        this.Text = text;
        this.Priority = priority;
        this.Date = date;
    }
}

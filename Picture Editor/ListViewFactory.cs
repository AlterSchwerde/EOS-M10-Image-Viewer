using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Picture_Editor
{
    class ListViewFactory
    {
        public static void SetListView()
        {
            ListView Listview1 = Form3.Listview1;

            Listview1.Items.Add(new ListViewItem(new string[] {"245", 
                "0x0245",
                "0x0245",
                "0x0245",
                "0x0245"}));
        }


        public static void AddItem(ListView listview, string id, string gg, string tagname, string value, string type)
        {
            listview.Items.Add(new ListViewItem(new string[]
                            {
                                id,
                                gg,
                                tagname,
                                value,
                                type}));
        }
                            
    }
}

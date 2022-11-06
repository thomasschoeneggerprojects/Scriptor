using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TsSolution.WpfCommon.Controls
{
    /// <summary>
    /// Interaction logic for UserControlList.xaml
    /// </summary>
    public partial class UserControlList : UserControl
    {
        public UserControlList()
        {
            InitializeComponent();
            lstBox.SelectionChanged += new SelectionChangedEventHandler((o, e) => OnSelectedItemChanged(o, e));
        }

        #region Items

        public static readonly DependencyProperty SetListItemsProperty =
           DependencyProperty.Register("ListItems", typeof(List<UserControl>), typeof(UserControlList), new
              PropertyMetadata(new List<UserControl>(), new PropertyChangedCallback(OnItemsChanged)));

        public List<UserControl> ListItems
        {
            get
            {
                return (List<UserControl>)GetValue(SetListItemsProperty);
            }
            set
            {
                SetValue(SetListItemsProperty, value);
            }
        }

        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControlList? userControlList = d as UserControlList;
            userControlList?.OnItemsChanged(e);
        }

        private List<UserControl> _allItems = new List<UserControl>();

        private void OnItemsChanged(DependencyPropertyChangedEventArgs e)
        {
            _allItems = (List<UserControl>)e.NewValue;

            ReFillListBox(_allItems);
        }

        public static readonly DependencyProperty SetSelectedItemProperty =
           DependencyProperty.Register("SelectedItem", typeof(UserControl), typeof(UserControlList), new
              PropertyMetadata(null));

        public UserControl SelectedItem
        {
            get
            {
                return (UserControl)GetValue(SetSelectedItemProperty);
            }
            set
            {
                SetValue(SetSelectedItemProperty, value);
            }
        }

        private void OnSelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            if (e.AddedItems.Count > 0)
            {
                if (e.AddedItems[0] is UserControl pfVar)
                {
                    SelectedItem = pfVar;
                }
            }

            if (SelectedItem != null)
            {
                listBox.SelectedItem = SelectedItem;
            }
        }

        #endregion Items

        private void ReFillListBox(List<UserControl> items)
        {
            if (items != null)
            {
                lstBox.Items.Clear();
                foreach (var item in items)
                {
                    lstBox.Items.Add(item);
                }
            }
        }
    }
}
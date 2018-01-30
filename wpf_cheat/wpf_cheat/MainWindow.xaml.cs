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

using System.ComponentModel;
using System.Collections.ObjectModel;

namespace wpf_cheat
{
   /// <summary>
   /// Testing Data-binding in WPF
   /// Data-binding = to view/interact with data
   /// elements bound to data in the form of CLR objects and XML
   /// Specifically: establish connection between app UI and program logic
   /// </summary>
   public partial class MainWindow : Window
   {

      private ObservableCollection<User> users = new ObservableCollection<User>();

      public MainWindow()
      {
         InitializeComponent();
         // DataContext is default Binding source of bindings. This is what bindings will get data from
         this.DataContext = this; // the MainWindow:Window object (self) is the datacontext

         // Add some useres by default to users list
         users.Add(new User() { Name = "Ji" });
         users.Add(new User() { Name = "Raj" });

         // populate the listbox with users we added
         lbUsers.ItemsSource = users;
         
      }
      
      private void btnUpdateSource_Click(object sender, RoutedEventArgs e)
      {
         BindingExpression binding = txtWindowTitle.GetBindingExpression(TextBox.TextProperty);
         binding.UpdateSource();
      }

      private void btnAddUser_Click(object sender, RoutedEventArgs e)
      {
         users.Add(new User() { Name = "New User" });
      }

      private void btnDelUser_Click(object sender, RoutedEventArgs e)
      {

         if (lbUsers.SelectedItem != null)
         {
            User selectUser = lbUsers.SelectedItem as User;
            users.Remove(selectUser);
         }
      }

      private void btnChangeUser_Click(object sender, RoutedEventArgs e)
      {
         if(lbUsers.SelectedItem != null)
         {
            // cast item as user obj
            (lbUsers.SelectedItem as User).Name = "Random Name";
         }
      }


      
   }

   public class User: INotifyPropertyChanged
   {

      private string name;
      public string Name
      {
         get { return this.name; }
         set
         {
            if(this.name != value)
            {
               this.name = value;
               this.NotifyPropertyChanged("Name");
            }
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
      public void NotifyPropertyChanged(string propName)
      {
         if (this.PropertyChanged != null)
         {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
         }
      }
   }

}


using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfCore.Data;
using WpfCore.Models;

namespace WpfCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int id;
        private bool _IsEnabled1;
        private bool _IsEnabled2;
        private bool _IsEnabled3;
        private bool _IsEnabled4;
       

        public MainWindow()
        {
            InitializeComponent();

            //using (var db = new AppDbContext())
            //{
            //    var studentData = db.Students.ToList();
            //    StudentList.Items.Add(studentData);
            //}
            btnEdit.IsEnabled = false;
           
            PopulateStudentData();

            

        }

        public bool IsEnabled1  
        {
            get => _IsEnabled1;
            set 
            {
                _IsEnabled1 = value;
                NotifyPropertyChanged();
            }         
        }

        public bool IsEnabled2
        {
            get => _IsEnabled2;
            set
            {
                _IsEnabled2 = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsEnabled3
        {
            get => _IsEnabled3;
            set
            {
                _IsEnabled3 = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsEnabled4
        {
            get => _IsEnabled4;
            set
            {
                _IsEnabled4 = value;
                NotifyPropertyChanged();
            }
        }

        


private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(strName.Text))
            {
                MessageBox.Show("Please Insert Name");
                strName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(strAddress.Text))
            {
                MessageBox.Show("Please Insert Address");
                strAddress.Focus();
                return;
            }


            using (var db = new AppDbContext())
            {


                db.Students.Add(new Student
                {
                    Name = strName.Text,
                    Address = strAddress.Text,
                });

                //var student = new Student 
                //{
                //    Name = studentName,
                //    Address = studentAddress,
                //};

                //db.Students.Add(student);
                db.SaveChanges();

                strAddress.Text = string.Empty;
                strName.Text = string.Empty;

                StudentList.Items.Clear();
                PopulateStudentData();

                MessageBox.Show("The Data Was Created!");


            }



        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
          
            using (var db = new AppDbContext())
            {

                if (id == 0)
                {
                    MessageBox.Show("Please, Select a Data form ListView to Edit!");
                    return;
                }

                var editStudent = db.Students.FirstOrDefault(e => e.Id == id);

                editStudent.Name = strName.Text;
                editStudent.Address = strAddress.Text;

                db.Students.Update(editStudent);

                db.SaveChanges();

                strAddress.Text = string.Empty;
                strName.Text = string.Empty;    
                
                StudentList.Items.Clear();
                PopulateStudentData();
                MessageBox.Show("The Data Was Edited!");

                btnEdit.IsEnabled = false;
            }
        }

        private void PopulateStudentData()
        {
            using (var db = new AppDbContext())
            {
                var studentData = db.Students.ToList();
                foreach (var student in studentData)
                {
                    StudentList.Items.Add(student);

                }
            }
        }

        private void StudentList_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (ListView)sender;
            var student = (Student)item.SelectedItem;
            id = student.Id;

            if (id != 0)
            {
                btnEdit.IsEnabled = true;
            }
         

            //MessageBox.Show($"Student Name: {student.Name} and his Address is {student.Address}");


            if (deleteCheckBox.IsChecked == true)
            {      

              
                using (var db = new AppDbContext())
                {
                    var result = MessageBox.Show("Are you want Delete this Record?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        var findStudent = db.Students.Find(id);
                        db.Students.Remove(findStudent);
                        db.SaveChanges();
                        StudentList.Items.Clear();
                        PopulateStudentData();
                        MessageBox.Show("The Data Was Deleted!");
                    }



                }
            }
            else if (editCheckBox.IsChecked == true)
            {

                deleteCheckBox.IsEnabled = false;
                 id = student.Id;            
                using (var db = new AppDbContext())
                {
                    var result = db.Students.Find(id);
                    strName.Text = result.Name;
                    strAddress.Text = result.Address;
                                                          
                }
            }
            else
            {
                MessageBox.Show($"Student Name: {student.Name} and his Address is: {student.Address}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void deleteCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (deleteCheckBox.IsChecked == true)
            {
                editCheckBox.IsEnabled = false;
                //btnEdit.IsEnabled = false;
                btnSave.IsEnabled = false;
                
            }

            if (deleteCheckBox.IsChecked ==false)
            {
                editCheckBox.IsEnabled = true;
                btnSave.IsEnabled = true;
            }


        }

      

        private void editCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (editCheckBox.IsChecked == true)
            {
                MessageBox.Show("Please, Select a Data form ListView to Edit!");

                deleteCheckBox.IsEnabled = false;
                //btnEdit.IsEnabled = true;
                btnSave.IsEnabled = false;
            }
            else
            {
                deleteCheckBox.IsEnabled = true;
                btnSave.IsEnabled = true;
                btnEdit.IsEnabled = false;
            }
        }
    }
}

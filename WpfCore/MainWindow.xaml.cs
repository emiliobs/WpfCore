using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfCore.Data;
using WpfCore.Models;

namespace WpfCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //using (var db = new AppDbContext())
            //{
            //    var studentData = db.Students.ToList();
            //    StudentList.Items.Add(studentData);
            //}

            PopulateStudentData();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var studentName = strName.Text;
            var studentAddress = strAddress.Text;

            using (var db = new AppDbContext())
            {
                db.Students.Add(new Student
                {
                    Name = studentName,
                    Address = studentAddress,
                });

                //var student = new Student 
                //{
                //    Name = studentName,
                //    Address = studentAddress,
                //};

                //db.Students.Add(student);
                db.SaveChanges();
                StudentList.Items.Clear();
                PopulateStudentData();
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

            //MessageBox.Show($"Student Name: {student.Name} and his Address is {student.Address}");


            if (deleteCheckBox.IsChecked == true)
            {
                var studentId = student.Id;
                using (var db = new AppDbContext())
                {
                    var result = MessageBox.Show("Are you want Delete this Record?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        var findStudent = db.Students.Find(studentId);
                        db.Students.Remove(findStudent);
                        db.SaveChanges();
                        StudentList.Items.Clear();
                        PopulateStudentData();
                    }



                }
            }
            else
            {
                MessageBox.Show($"Student Name: {student.Name} and his Address is: {student.Address}");
            }
        }
    }
}
